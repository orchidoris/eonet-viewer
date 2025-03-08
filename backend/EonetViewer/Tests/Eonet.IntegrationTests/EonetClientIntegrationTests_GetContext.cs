using FluentAssertions;
using static Eonet.IntegrationTests.Helpers.EonetClientHelper;
using static Eonet.IntegrationTests.Settings;

namespace Eonet.IntegrationTests;

[TestClass]
[DoNotParallelize] // a method tested here involves a lot of parallelization already
public class EonetClientIntegrationTests_GetContext
{
    const int MinCategoriesCount = 10;
    const int MinSourcesCount = 10;
    const int MinMagnitudesCount = 10;
    const int MinLayersCount = 10;

    private readonly IEonetClient _client = GetRealEonetClient();

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    public async Task GetContext_Success()
    {
        var contextApiResponse = await _client.GetContext();
        Assert.IsTrue(contextApiResponse.IsSuccessful);
        
        var context = contextApiResponse.Content;
        Assert.IsNotNull(context);
        var (categories, sources, magnitudes, layers) = context;

        Assert.IsTrue(categories.Count >= MinCategoriesCount, $"Expected at least {MinCategoriesCount} categories, but {categories.Count} encountered.");
        categories.FirstOrDefault(c => c.Id == KnownCategoryId.Earthquakes).Should().BeEquivalentTo(new CategoryWithLayers(
            Id: KnownCategoryId.Earthquakes,
            Title: "Earthquakes",
            Description: "Related to all manner of shaking and displacement. Certain aftermath of earthquakes may also be found under landslides and floods.",
            Url: "https://eonet.gsfc.nasa.gov/api/v3/categories/earthquakes",
            LayersUrl: "https://eonet.gsfc.nasa.gov/api/v3/layers/earthquakes",
            Layers: ["MODIS_Aqua_Data_No_Data", "MODIS_Terra_Data_No_Data"]
        ));
        
        Assert.IsTrue(sources.Count >= MinSourcesCount, $"Expected at least {MinSourcesCount} sources, but {sources.Count} encountered.");
        sources.FirstOrDefault(c => c.Id == KnownSourceId.AVO).Should().BeEquivalentTo(new Source(
            Id: KnownSourceId.AVO,
            Title: "Alaska Volcano Observatory",
            SourceUrl: "https://www.avo.alaska.edu/",
            EventsUrl: "https://eonet.gsfc.nasa.gov/api/v3/events?source=AVO"
        ));

        Assert.IsTrue(magnitudes.Count >= MinMagnitudesCount, $"Expected at least {MinMagnitudesCount} magnitudes, but {magnitudes.Count} encountered.");
        magnitudes.FirstOrDefault(c => c.Id == "ac").Should().BeEquivalentTo(new Magnitude(
            Id: "ac",
            Title: "Acres",
            Unit: "acres",
            Description: "An area of land area equal to 4,840 square yards (0.405 hectare).",
            EventsUrl: "https://eonet.gsfc.nasa.gov/api/v3/events?magID=ac"
        ));

        Assert.IsTrue(layers.Count >= MinLayersCount, $"Expected at least {MinLayersCount} layers, but {layers.Count} encountered.");
        var layer = layers.FirstOrDefault(c => c.Id == KnownLayerId.AIRS_Dust_Score_Ocean_Day);
        layer.Should().BeEquivalentTo(new LayerWithCategories(
            Id: "AIRS_Dust_Score_Ocean_Day",
            ServiceUrl: "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best/wmts.cgi",
            ServiceTypeId: "WMTS_1_0_0",
            Parameters: [new("image/png", "2km")],
            Categories: ["dustHaze", "wildfires"]
        ));
    }
}
