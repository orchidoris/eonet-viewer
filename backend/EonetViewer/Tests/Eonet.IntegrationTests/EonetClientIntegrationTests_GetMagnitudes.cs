using Eonet.IntegrationTests.Extensions;
using FluentAssertions;
using static Eonet.IntegrationTests.Helpers.EonetClientHelper;
using static Eonet.IntegrationTests.Settings;

namespace Eonet.IntegrationTests;

[TestClass]
public class EonetClientIntegrationTests_GetMagnitudes
{
    private readonly IEonetClient _client = GetRealEonetClient();

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    public async Task DeserializesResponse_Success()
    {
        var apiResponse = await _client.GetMagnitudes();
        Assert.AreEqual("https://eonet.gsfc.nasa.gov/api/v3/magnitudes", apiResponse.GetUrl());
        Assert.IsTrue(apiResponse.IsSuccessful, apiResponse.GetErrorMessage());

        var response = apiResponse.Content;
        Assert.IsNotNull(response);

        response.Should().BeEquivalentTo(
            new MagnitudesResponse(
                Title: "EONET Event Magnitudes",
                Description: "List of all the available event magnitudes in the EONET system",
                Url: "https://eonet.gsfc.nasa.gov/api/v3/magnitudes",
                Magnitudes: []),
            opt => opt.Excluding(m => m.Magnitudes));

        var magnitudes = response.Magnitudes.ToDictionary(c => c.Id);
        Assert.IsTrue(magnitudes.Count >= 11);
        magnitudes["ha"].Should().BeEquivalentTo(new Magnitude(
            Id: "ha",
            Title: "Hectares",
            Unit: "hectare",
            Description: "A hectare is approximately equal to 2.471 acres.",
            EventsUrl: "https://eonet.gsfc.nasa.gov/api/v3/events?magID=ha"));

        magnitudes["sq_NM"].Should().BeEquivalentTo(new Magnitude(
            Id: "sq_NM",
            Title: "Area (Nautical Miles)",
            Unit: "NM^2",
            Description: "Nautical miles squared used to approximate the area of icebergs.",
            EventsUrl: "https://eonet.gsfc.nasa.gov/api/v3/events?magID=sq_NM"));
    }
}
