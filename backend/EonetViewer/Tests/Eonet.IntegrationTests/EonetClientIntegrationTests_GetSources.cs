using Eonet.IntegrationTests.Extensions;
using FluentAssertions;
using static Eonet.IntegrationTests.Helpers.EonetClientHelper;
using static Eonet.IntegrationTests.Settings;

namespace Eonet.IntegrationTests;

[TestClass]
public class EonetClientIntegrationTests_GetSources
{
    private readonly IEonetClient _client = GetRealEonetClient();

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    public async Task DeserializesResponse_Success()
    {
        var apiResponse = await _client.GetSources();
        Assert.AreEqual("https://eonet.gsfc.nasa.gov/api/v3/sources", apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

        var response = apiResponse.Content;
        Assert.IsNotNull(response);

        response.Should().BeEquivalentTo(
            new SourcesResponse(
                Title: "EONET Event Sources",
                Description: "List of all the available event sources in the EONET system",
                Url: "https://eonet.gsfc.nasa.gov/api/v3/sources",
                Sources: []),
            opt => opt.Excluding(s => s.Sources));

        var sources = response.Sources.ToDictionary(c => c.Id);
        Assert.IsTrue(sources.Count >= 33);
        sources["BYU_ICE"].Should().BeEquivalentTo(new Source(
            Id: "BYU_ICE",
            Title: "Brigham Young University Antarctic Iceberg Tracking Database",
            SourceUrl: "http://www.scp.byu.edu/data/iceberg/database1.html",
            EventsUrl: "https://eonet.gsfc.nasa.gov/api/v3/events?source=BYU_ICE"));

        sources["UNISYS"].Should().BeEquivalentTo(new Source(
            Id: "UNISYS",
            Title: "Unisys Weather",
            SourceUrl: "http://weather.unisys.com/hurricane/",
            EventsUrl: "https://eonet.gsfc.nasa.gov/api/v3/events?source=UNISYS"));
    }
}
