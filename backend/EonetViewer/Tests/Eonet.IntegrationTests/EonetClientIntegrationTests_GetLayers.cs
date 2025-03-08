using Eonet.IntegrationTests.Extensions;
using FluentAssertions;
using System.Collections.Immutable;
using static Eonet.IntegrationTests.Helpers.EonetClientHelper;
using static Eonet.IntegrationTests.Settings;

namespace Eonet.IntegrationTests;

[TestClass]
public class EonetClientIntegrationTests_GetLayers
{
    private readonly IEonetClient _client = GetRealEonetClient();

    private static readonly ImmutableDictionary<string, int?> CategoryExpectedLayerCountMap =
        new Dictionary<string, int?>() {
            { KnownCategoryId.Drought, 20 },
            { KnownCategoryId.DustHaze, 20 },
            { KnownCategoryId.Earthquakes, 2 },
            { KnownCategoryId.Floods, 21 },
            { KnownCategoryId.Landslides, 5 },
            { KnownCategoryId.Manmade, 9 },
            { KnownCategoryId.SeaLakeIce, 22 },
            { KnownCategoryId.SevereStorms, 24 },
            { KnownCategoryId.Snow, 12 },
            { KnownCategoryId.TempExtremes, 10 },
            { KnownCategoryId.Volcanoes, 22 },
            { KnownCategoryId.WaterColor, 7 },
            { KnownCategoryId.Wildfires, 27 },
        }.ToImmutableDictionary();

    private static IEnumerable<object[]> GetActualCategoryIds() =>
        GetRealEonetClient().GetCategories().Result.Content?
            .Categories.Select(c => new object[] { c.Id }) ?? [];

    [TestMethod]
    [Timeout(IntegrationTestTimeout * 4)]
    [DynamicData(nameof(GetActualCategoryIds), DynamicDataSourceType.Method)]
    public async Task FiltersByCategory_Success(string categoryId)
    {
        var layersApiResponse = await _client.GetLayers(categoryId);
        var layersResponse = layersApiResponse.Content;
        Assert.IsNotNull(layersResponse);
        Assert.AreEqual(1, layersResponse.Categories.Count);

        var layers = layersResponse.Categories.Single().Layers;
        Assert.IsNotNull(layers);
        CollectionAssert.AllItemsAreUnique(layers.Select(l => l.Id).ToList());

        CategoryExpectedLayerCountMap.TryGetValue(categoryId, out int? expectedCount);
        Assert.IsNotNull(expectedCount, $"Expected layers count is not provided for category '{categoryId}'.");
        Assert.IsTrue(layers.Count >= expectedCount,
            $"Expected at least {expectedCount} layers for '{categoryId}' category, but {layers.Count} encountered.");
    }

    [TestMethod]
    [Timeout(IntegrationTestTimeout)]
    public async Task DeserializesResponse_Success()
    {
        var categoryId = KnownCategoryId.Volcanoes;
        var layersApiResponse = await _client.GetLayers(categoryId);
        Assert.AreEqual("https://eonet.gsfc.nasa.gov/api/v3/layers/volcanoes", layersApiResponse.GetUrl());
        Assert.IsTrue(layersApiResponse.IsSuccessful, layersApiResponse.GetErrorMessage());

        var response = layersApiResponse.Content;
        Assert.IsNotNull(response);

        response.Should().BeEquivalentTo(
            new LayersResponse(
                Title: "EONET Web Service Layers",
                Description: "List of web service layers in the EONET system",
                Url: "https://eonet.gsfc.nasa.gov/api/v3/layers/volcanoes",
                Categories: []),
            opt => opt.Excluding(m => m.Categories));

        var layers = response.Categories[0].Layers.ToDictionary(c => c.Id);
        Assert.IsTrue(layers.Count >= CategoryExpectedLayerCountMap[categoryId]);
        layers["AIRS_CO_Total_Column_Day"].Should().BeEquivalentTo(new Layer(
            Id: "AIRS_CO_Total_Column_Day",
            ServiceUrl: "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best/wmts.cgi",
            ServiceTypeId: "WMTS_1_0_0",
            Parameters: [new(KnownLayerFormat.ImagePng, KnownTileMatrixSet._2km)]));

        layers["ndh-volcano-hazard-frequency-distribution"].Should().BeEquivalentTo(new Layer(
            Id: "ndh-volcano-hazard-frequency-distribution",
            ServiceUrl: "https://sedac.ciesin.columbia.edu/geoserver/ows",
            ServiceTypeId: "WMS_1_1_1",
            Parameters: [new(KnownLayerFormat.ImagePng)]));
    }
}
