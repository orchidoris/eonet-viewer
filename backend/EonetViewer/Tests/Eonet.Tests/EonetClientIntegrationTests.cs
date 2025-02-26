using Eonet.Tests.Extensions;
using FluentAssertions;
using GeoJSON.Text.Geometry;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Eonet.Tests;

[TestClass]
public sealed class EonetClientIntegrationTests
{
    private readonly TestServer _server;
    private readonly IEonetClient _client;

    public EonetClientIntegrationTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _server = new TestServer(new WebHostBuilder()
            .Configure(app => { })
            .ConfigureServices(services => services.AddEonetClients(configuration)));

        _client = _server.Services.GetRequiredService<IEonetClient>();
    }

    [TestMethod]
    [Timeout(8000)]
    public async Task GetEvents_DeserializeEvent()
    {
        var date = Date("2024-07-28");
        var apiResponse = await _client.GetEvents(new() { Categories = ["wildfires"], Status = EventStatusFilter.Closed, Start = date, End = date });
        Assert.AreEqual(
            "https://eonet.gsfc.nasa.gov/api/v3/events?category=wildfires&status=closed&start=2024-07-28&end=2024-07-28",
            apiResponse.GetUriString());
        Assert.IsTrue(apiResponse.IsSuccessful);

        var response = apiResponse.Content;
        Assert.IsNotNull(response);
        Assert.AreEqual(1, response.Events.Count);
        response.Should().BeEquivalentTo(new EventsResponse(
            Title: "EONET Events",
            Description: "Natural events from EONET.",
            Link: "https://eonet.gsfc.nasa.gov/api/v3/events",
            Events: [new(
                Id: "EONET_9111",
                Title: "Beaver Creek Wildfire, Campbell, Wyoming",
                Description: "22 miles SW of Gillette",
                Link: "https://eonet.gsfc.nasa.gov/api/v3/events/EONET_9111",
                ClosedDate: DateTimeOffset.Parse("2024-07-28T00:00:00Z"),
                Categories: [new("wildfires", "Wildfires")],
                Sources: [new("IRWIN", "https://irwin.doi.gov/observer/")],
                Geometry: [new(
                    Date: DateTimeOffset.Parse("2024-07-28T20:53:00Z"),
                    Type: "Point",
                    Coordinates: new Position(44.082883, -105.902417),
                    MagnitudeValue: 1075.00,
                    MagnitudeUnit: "acres"
                )]
            )]
        ));
    }

    [TestMethod]
    [Timeout(8000)]
    [DataRow(10)]
    [DataRow(42)]
    public async Task GetEvents_Limit(int limit)
    {
        var apiResponse = await _client.GetEvents(new() { Limit = limit, End = Date("2024-12-31") });
        Assert.AreEqual(
            $"https://eonet.gsfc.nasa.gov/api/v3/events?limit={limit}&end=2024-12-31",
            apiResponse.GetUriString());
        Assert.IsTrue(apiResponse.IsSuccessful);
        
        var response = apiResponse.Content;
        Assert.IsTrue(response.Events.Count <= limit);
    }


    [TestMethod]
    [Timeout(8000)]
    [DataRow(2)]
    [DataRow(8)]
    [DataRow(20)]
    public async Task GetEvents_Days(int daysPrior)
    {
        var apiResponse = await _client.GetEvents(new() { Days = daysPrior });
        Assert.AreEqual(
            $"https://eonet.gsfc.nasa.gov/api/v3/events?days={daysPrior}",
            apiResponse.GetUriString());
        Assert.IsTrue(apiResponse.IsSuccessful);

        var minDate = apiResponse.Content?.Events.Min(static e => e.Geometry.Max(static g => g.Date));
        Assert.IsTrue(
            minDate > DateTimeOffset.UtcNow.Date.AddDays(-daysPrior),
            $"Failed for daysPrior={daysPrior}");
    }

    [TestMethod]
    [Timeout(8000)]
    public async Task GetEvents_SourceFilter()
    {
        string[] filterSources = ["BYU_ICE", "SIVolcano", "Earthdata", "NATICE"];
        var apiResponse = await _client.GetEvents(new()
        {
            Sources = filterSources,
            Status = EventStatusFilter.All,
            Start = Date("2024-01-01"),
            End = Date("2024-03-31")
        });

        Assert.AreEqual(
            "https://eonet.gsfc.nasa.gov/api/v3/events?source=BYU_ICE,SIVolcano,Earthdata,NATICE&status=all&start=2024-01-01&end=2024-03-31",
            apiResponse.GetUriString());
        Assert.IsTrue(apiResponse.IsSuccessful);

        var events = apiResponse.Content?.Events;
        Assert.IsNotNull(events);
        Assert.AreEqual(42, events.Count);

        foreach (var e in events)
            Assert.IsTrue(e.Sources.Any(s => filterSources.Contains(s.Id)), $"Event {e.Id} has no relevant sources");

        var sourceCountMap = events
            .SelectMany(e => e.Sources.Select(s => new { SourceId = s.Id, EventId = e.Id }))
            .GroupBy(se => se.SourceId, se => se.EventId)
            .Where(kv => filterSources.Contains(kv.Key))
            .ToDictionary(kv => kv.Key, kv => kv.Count());

        sourceCountMap.Should().BeEquivalentTo(new Dictionary<string, int> {
            { "BYU_ICE", 3 }, { "SIVolcano", 6 }, { "Earthdata", 1 }, { "NATICE", 36 }
        });
    }

    [TestMethod]
    [Timeout(8000)]
    public async Task GetEvents_CategoryFilter()
    {
        string[] filterCategories = ["volcanoes", "seaLakeIce"];
        var apiResponse = await _client.GetEvents(new()
        {
            Categories = filterCategories,
            Status = EventStatusFilter.All,
            Start = Date("2024-01-01"),
            End = Date("2024-02-29")
        });

        Assert.AreEqual(
            "https://eonet.gsfc.nasa.gov/api/v3/events?category=volcanoes,seaLakeIce&status=all&start=2024-01-01&end=2024-02-29",
            apiResponse.GetUriString());
        Assert.IsTrue(apiResponse.IsSuccessful);

        var events = apiResponse.Content?.Events;
        Assert.IsNotNull(events);
        Assert.AreEqual(36, events.Count);

        foreach (var e in events)
            Assert.IsTrue(e.Categories.Any(s => filterCategories.Contains(s.Id)), $"Event {e.Id} has no relevant categories");

        var categoryCountMap = events
            ?.SelectMany(e => e.Categories.Select(s => new { CategoryId = s.Id, EventId = e.Id }))
            .GroupBy(se => se.CategoryId, se => se.EventId)
            .Where(kv => filterCategories.Contains(kv.Key))
            .ToDictionary(kv => kv.Key, kv => kv.Count());

        categoryCountMap.Should().BeEquivalentTo(new Dictionary<string, int> {
            { "volcanoes", 2 }, { "seaLakeIce", 34 }
        });
    }

    [TestMethod]
    [Timeout(8000)]
    [DataRow(null, "", true, false)]
    [DataRow(EventStatusFilter.Open, "", true, false)]
    [DataRow(EventStatusFilter.Closed, "status=closed&", false, true)]
    [DataRow(EventStatusFilter.All, "status=all&", true, true)]
    public async Task GetEvents_StatusFilter(EventStatusFilter? status, string expectedPath, bool hasOpen, bool hasClosed)
    {
        var apiResponse = status == null
            ? await _client.GetEvents(new() { Days = 15 })
            : await _client.GetEvents(new() { Status = status.Value, Days = 15 });

        Assert.AreEqual(
            $"https://eonet.gsfc.nasa.gov/api/v3/events?{expectedPath}days=15",
            apiResponse.GetUriString());
        Assert.IsTrue(apiResponse.IsSuccessful);

        var events = apiResponse.Content?.Events;
        Assert.IsNotNull(events);
        Assert.AreEqual(hasOpen, events.Any(e => e.ClosedDate == null));
        Assert.AreEqual(hasClosed, events.Any(e => e.ClosedDate != null));
    }

    [TestMethod]
    [Timeout(8000)]
    public async Task GetEvents_StartEndFilter()
    {
        var apiResponse = await _client.GetEvents(new()
            { Status = EventStatusFilter.All, Start = Date("2024-08-31"), End = Date("2024-09-01") });

        Assert.AreEqual(
            "https://eonet.gsfc.nasa.gov/api/v3/events?status=all&start=2024-08-31&end=2024-09-01",
            apiResponse.GetUriString());
        Assert.IsTrue(apiResponse.IsSuccessful);

        var events = apiResponse.Content?.Events;
        Assert.IsNotNull(events);
        Assert.AreEqual(44, events.Count);
        Assert.IsTrue(DateTimeOffset.Parse("2024-08-31Z") <= events.Min(static e => e.Geometry.Max(static g => g.Date)));
        Assert.IsTrue(events.Max(static e => e.Geometry.Min(static g => g.Date)) <= DateTimeOffset.Parse("2024-09-02Z"));
    }

    [TestMethod]
    [Timeout(8000)]
    public async Task GetEvents_MagnitudeFilter()
    {
        var apiResponse = await _client.GetEvents(new()
        {
            Status = EventStatusFilter.All,
            Start = Date("2024-03-01"),
            End = Date("2024-03-31"),
            Magnitude = new("ac", 3000, 7000),
        });

        Assert.AreEqual(
            "https://eonet.gsfc.nasa.gov/api/v3/events?status=all&start=2024-03-01&end=2024-03-31&magID=ac&magMin=3000&magMax=7000",
            apiResponse.GetUriString());
        Assert.IsTrue(apiResponse.IsSuccessful);

        var events = apiResponse.Content?.Events;
        Assert.IsNotNull(events);
        Assert.AreEqual(7, events.Count);
        Assert.IsTrue(events.All(e => e.Geometry.Any(g => g.MagnitudeUnit == "acres")));
        Assert.IsTrue(3000 <= events.Min(static e => e.Geometry.Max(static g => g.MagnitudeValue)));
        Assert.IsTrue(events.Max(static e => e.Geometry.Min(static g => g.MagnitudeValue)) <= 7000);
    }

    [TestMethod]
    [Timeout(8000)]
    public async Task GetEvents_BoundingBoxFilter()
    {
        // USA bounding box
        var bbox = new BoundingBox(-125.0011, 49.5904, -66.9326, 24.9493);
        var apiResponse = await _client.GetEvents(new()
        {
            Status = EventStatusFilter.All,
            Start = Date("2024-03-01"),
            End = Date("2024-03-31"),
            BoundingBox = bbox, 
        });

        Assert.AreEqual(
            "https://eonet.gsfc.nasa.gov/api/v3/events?status=all&start=2024-03-01&end=2024-03-31&bbox=-125.0011,49.5904,-66.9326,24.9493",
            apiResponse.GetUriString());
        Assert.IsTrue(apiResponse.IsSuccessful);

        var events = apiResponse.Content?.Events;
        Assert.IsNotNull(events);
        Assert.AreEqual(45, events.Count);
        foreach (var e in events)
        {
            Assert.IsTrue(
                e.Geometry.Any(g => bbox.MinLongitude <= g.Coordinates.Longitude && g.Coordinates.Longitude <= bbox.MaxLongitude),
                $"Longitude is outside of the bounding box for event {e.Id}");

            Assert.IsTrue(
                e.Geometry.Any(g => bbox.MinLatitude <= g.Coordinates.Latitude && g.Coordinates.Longitude <= bbox.MaxLatitude),
                $"Latitude is outside of the bounding box for event {e.Id}");
        }
    }

    private static DateOnly Date(string date) => DateOnly.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
}
