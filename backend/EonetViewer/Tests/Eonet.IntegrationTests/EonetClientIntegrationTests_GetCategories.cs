using Eonet.IntegrationTests.Extensions;
using FluentAssertions;
using static Eonet.IntegrationTests.Helpers.EonetClientHelper;

namespace Eonet.IntegrationTests;

[TestClass]
public class EonetClientIntegrationTests_GetCategories
{
    private readonly IEonetClient _client = GetRealEonetClient();

    [TestMethod]
    [Timeout(8000)]
    public async Task DeserializeResponse_Success()
    {
        var categoriesApiResponse = await _client.GetCategories();
        Assert.AreEqual("https://eonet.gsfc.nasa.gov/api/v3/categories", categoriesApiResponse.GetUrl());
        Assert.IsTrue(categoriesApiResponse.IsSuccessful, categoriesApiResponse.GetErrorMessage());

        var categoriesResponse = categoriesApiResponse.Content;
        Assert.IsNotNull(categoriesResponse);
        
        categoriesResponse.Should().BeEquivalentTo(
            new CategoriesResponse(
                Title: "EONET Event Categories",
                Description: "List of all the available event categories in the EONET system",
                Url: "https://eonet.gsfc.nasa.gov/api/v3/categories",
                Categories: []),
            opt => opt.Excluding(m => m.Categories));

        var categories = categoriesResponse.Categories.ToDictionary(c => c.Id);
        Assert.IsTrue(categories.Count >= 13);
        categories["drought"].Should().BeEquivalentTo(new Category(
            Id: "drought",
            Title: "Drought",
            Description: "Long lasting absence of precipitation affecting agriculture and livestock, and the overall availability of food and water.",
            Url: "https://eonet.gsfc.nasa.gov/api/v3/categories/drought",
            LayersUrl: "https://eonet.gsfc.nasa.gov/api/v3/layers/drought"));

        categories["volcanoes"].Should().BeEquivalentTo(new Category(
            Id: "volcanoes",
            Title: "Volcanoes",
            Description: "Related to both the physical effects of an eruption (rock, ash, lava) and the atmospheric (ash and gas plumes).",
            Url: "https://eonet.gsfc.nasa.gov/api/v3/categories/volcanoes",
            LayersUrl: "https://eonet.gsfc.nasa.gov/api/v3/layers/volcanoes"));
    }
}
