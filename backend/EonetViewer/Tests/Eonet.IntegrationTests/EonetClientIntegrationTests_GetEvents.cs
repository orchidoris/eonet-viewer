using Eonet.IntegrationTests.Extensions;
using FluentAssertions;
using System.Globalization;
using static Eonet.IntegrationTests.Helpers.EonetClientHelper;
using static Eonet.IntegrationTests.Settings;

namespace Eonet.IntegrationTests;

[TestClass]
public sealed class EonetClientIntegrationTests_GetEvents
{
    private readonly IEonetClient _client = GetRealEonetClient();

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    public async Task DeserializesResponse_PointGeometry_Success()
    {
        var date = Date("2024-07-28");
        var apiResponse = await _client.GetEvents(new() { Categories = ["wildfires"], Status = EventStatusFilter.Closed, Start = date, End = date });
        Assert.AreEqual(
            "https://eonet.gsfc.nasa.gov/api/v3/events?category=wildfires&status=closed&start=2024-07-28&end=2024-07-28",
            apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

        var response = apiResponse.Content;
        Assert.IsNotNull(response);
        Assert.AreEqual(1, response.Events.Count);
        response.Should().BeEquivalentTo(
            new EventsResponse(
                Title: "EONET Events",
                Description: "Natural events from EONET.",
                Url: "https://eonet.gsfc.nasa.gov/api/v3/events",
                Events: [new(
                    Id: "EONET_9111",
                    Title: "Beaver Creek Wildfire, Campbell, Wyoming",
                    Description: "22 miles SW of Gillette",
                    Url: "https://eonet.gsfc.nasa.gov/api/v3/events/EONET_9111",
                    ClosedDate: DateTimeOffset.Parse("2024-07-28T00:00:00Z"),
                    Categories: [new("wildfires", "Wildfires")],
                    Sources: [new("IRWIN", "https://irwin.doi.gov/observer/")],
                    Geometry: [new EventPointGeometry(
                        Date: DateTimeOffset.Parse("2024-07-28T20:53:00Z"),
                        Coordinates: new(44.082883, -105.902417),
                        MagnitudeValue: 1075.00,
                        MagnitudeUnit: "acres"
                    )]
                )]));
    }

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    public async Task DeserializesResponse_PolygonGeometry_Success()
    {
        var date = Date("2015-08-12");
        var apiResponse = await _client.GetEvents(new() { Categories = ["manmade"], Status = EventStatusFilter.Closed, Start = date, End = date });
        Assert.AreEqual(
            "https://eonet.gsfc.nasa.gov/api/v3/events?category=manmade&status=closed&start=2015-08-12&end=2015-08-12",
            apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

        var response = apiResponse.Content;
        Assert.IsNotNull(response);
        Assert.AreEqual(1, response.Events.Count);
        response.Should().BeEquivalentTo(
            new EventsResponse(
                Title: "EONET Events",
                Description: "Natural events from EONET.",
                Url: "https://eonet.gsfc.nasa.gov/api/v3/events",
                Events: [new(
                    Id: "EONET_103",
                    Title: "Explosion at Tianjin, China",
                    Description: "Fires associated with a massive explosion in Tianjin, China, sent dark smoke drifting east and southeast.",
                    Url: "https://eonet.gsfc.nasa.gov/api/v3/events/EONET_103",
                    ClosedDate: DateTimeOffset.Parse("2015-08-13T00:00:00Z"),
                    Categories: [new("dustHaze", "Dust and Haze"), new("manmade", "Manmade")],
                    Sources: [new("EO", "http://earthobservatory.nasa.gov/NaturalHazards/view.php?id=86410")],
                    Geometry: [new EventPolygonGeometry(
                        Date: DateTimeOffset.Parse("2015-08-12T00:00:00Z"),
                        Coordinates: [new([
                            [117.099609375, 38.0171001312755],
                            [117.099609375, 39.78083314629773],
                            [121.795654296875, 39.78083314629773],
                            [121.795654296875, 38.0171001312755],
                            [117.099609375, 38.0171001312755]
                        ])]
                    )]
                )]));
    }

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    [DataRow(10)]
    [DataRow(42)]
    public async Task LimitsEventsNumber_Success(int limit)
    {
        var apiResponse = await _client.GetEvents(new() { Limit = limit, End = Date("2024-12-31") });
        Assert.AreEqual(
            $"https://eonet.gsfc.nasa.gov/api/v3/events?limit={limit}&end=2024-12-31",
            apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());
        
        var response = apiResponse.Content;
        Assert.IsTrue(response.Events.Count <= limit);
    }


    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    [DataRow(2)]
    [DataRow(8)]
    [DataRow(20)]
    public async Task LimitsDaysInThePast_Success(int daysPrior)
    {
        var apiResponse = await _client.GetEvents(new() { Days = daysPrior });
        Assert.AreEqual(
            $"https://eonet.gsfc.nasa.gov/api/v3/events?days={daysPrior}",
            apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

        var minDate = apiResponse.Content?.Events.MinMax(g => g.Date);
        Assert.IsTrue(
            minDate > DateTimeOffset.UtcNow.Date.AddDays(-daysPrior),
            $"Failed for daysPrior={daysPrior}");
    }

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    public async Task FiltersBySource_Success()
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
            apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

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
    [Timeout(IntegrationTestTimeout)]
    public async Task FiltersByCategory_Success()
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
            apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

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
    [Timeout(IntegrationTestTimeout)]
    [DataRow(null, "", true, false)]
    [DataRow(EventStatusFilter.Open, "", true, false)]
    [DataRow(EventStatusFilter.Closed, "status=closed&", false, true)]
    [DataRow(EventStatusFilter.All, "status=all&", true, true)]
    public async Task FiltersByStatus_Success(EventStatusFilter? status, string expectedPath, bool hasOpen, bool hasClosed)
    {
        var apiResponse = status == null
            ? await _client.GetEvents(new() { Days = 15 })
            : await _client.GetEvents(new() { Status = status.Value, Days = 15 });

        Assert.AreEqual(
            $"https://eonet.gsfc.nasa.gov/api/v3/events?{expectedPath}days=15",
            apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

        var events = apiResponse.Content?.Events;
        Assert.IsNotNull(events);
        Assert.AreEqual(hasOpen, events.Any(e => e.ClosedDate == null));
        Assert.AreEqual(hasClosed, events.Any(e => e.ClosedDate != null));
    }

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    public async Task FiltersByStartEnd_Success()
    {
        var apiResponse = await _client.GetEvents(new()
            { Status = EventStatusFilter.All, Start = Date("2024-08-31"), End = Date("2024-09-01") });

        Assert.AreEqual(
            "https://eonet.gsfc.nasa.gov/api/v3/events?status=all&start=2024-08-31&end=2024-09-01",
            apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

        var events = apiResponse.Content?.Events;
        Assert.IsNotNull(events);
        Assert.AreEqual(44, events.Count);
        Assert.IsTrue(DateTimeOffset.Parse("2024-08-31Z") <= events.MinMax(g => g.Date));
        Assert.IsTrue(events.MaxMin(g => g.Date) <= DateTimeOffset.Parse("2024-09-02Z"));
    }

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    public async Task FiltersByMagnitude_Success()
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
            apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

        var events = apiResponse.Content?.Events;
        Assert.IsNotNull(events);
        Assert.AreEqual(7, events.Count);
        Assert.IsTrue(events.All(e => e.Geometry.Any(g => g.MagnitudeUnit == "acres")));
        Assert.IsTrue(3000 <= events.MinMax(g => g.MagnitudeValue!.Value));
        Assert.IsTrue(events.MaxMin(g => g.MagnitudeValue!.Value) <= 7000);
    }

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    public async Task FiltersByBoundingBox_Success()
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
            apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

        var events = apiResponse.Content?.Events;
        Assert.IsNotNull(events);
        Assert.AreEqual(45, events.Count);
        foreach (var e in events)
        {
            Assert.IsTrue(
                e.Geometry.Any(g => {
                var coordinates = (g as EventPointGeometry)?.Coordinates;

                return coordinates == null
                    || (bbox.MinLongitude <= coordinates.Longitude && coordinates.Longitude <= bbox.MaxLongitude)
                    || (bbox.MinLatitude <= coordinates.Latitude && coordinates.Longitude <= bbox.MaxLatitude);
                }),
                $"Event {e.Id} point is missing or is outside of the bounding box.");
        }
    }

    private static DateOnly Date(string date) => DateOnly.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
}
