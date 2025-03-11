using Eonet.Tests.Extensions;
using FluentAssertions;
using Moq;

namespace Eonet.Tests;

[TestClass]
public class EonetClientTests
{
    private readonly Mock<IEonetClient> _eonetClient = new() { CallBase = true };

    [TestMethod]
    public async Task GetContext_ComposesMultipleEndpointsResponses()
    {
        // Arrange
        _eonetClient.Setup(m => m.GetCategories(It.IsAny<CancellationToken>())).ReturnsApiResponse(new([
            new("landslides", "Landslides"),
            new("volcanoes", "Volcanoes"),
            new("snow", "Snow")]));

        var layer1 = new Layer("layer-1", "layer-url-1", "layer-type-1", [new("image/png", "250m")]);
        var layer2 = new Layer("layer-2", "layer-url-2", "layer-type-2", [new("image/jpeg")]);
        var layer3 = new Layer("layer-3", "layer-url-3", "layer-type-3", [new(null, "2km")]);

        _eonetClient.Setup(m => m.GetLayers("landslides", It.IsAny<CancellationToken>())).ReturnsApiResponse(new([new([layer1, layer2, layer3])]));
        _eonetClient.Setup(m => m.GetLayers("volcanoes", It.IsAny<CancellationToken>())).ReturnsApiResponse(new([new([layer1, layer2])]));
        _eonetClient.Setup(m => m.GetLayers("snow", It.IsAny<CancellationToken>())).ReturnsApiResponse(new([new([layer1])]));

        _eonetClient.Setup(m => m.GetSources(It.IsAny<CancellationToken>())).ReturnsApiResponse(new([
            new("source-1", "Source 1", "source-url-1", "source-event-1"),
            new("source-2", "Source 2", "source-url-2", "source-event-2")
        ]));

        _eonetClient.Setup(m => m.GetMagnitudes(It.IsAny<CancellationToken>())).ReturnsApiResponse(new([
            new("mag-1", "Magnitude 1", "mag-unit-1", "mag-desc-1", "mag-url-1"),
            new("mag-2", "Magnitude 2", "mag-unit-2", "mag-desc-2", "mag-url-2")
        ]));

        // Act
        var contextApiResponse = await _eonetClient.Object.GetContext();

        // Assert
        Assert.IsNotNull(contextApiResponse);
        Assert.IsTrue(contextApiResponse.IsSuccessful);
        contextApiResponse.Content.Should().BeEquivalentTo(new ContextResponse(
            Categories: [
                new("landslides", "Landslides", Layers: ["layer-1", "layer-2", "layer-3"]),
                new("volcanoes", "Volcanoes", Layers: ["layer-1", "layer-2"]),
                new("snow", "Snow", Layers: ["layer-1"])
            ],
            Layers: [ 
                new("layer-1", "layer-url-1", "layer-type-1", [new("image/png", "250m")]),
                new("layer-2", "layer-url-2", "layer-type-2", [new("image/jpeg")]),
                new("layer-3", "layer-url-3", "layer-type-3", [new(null, "2km")]),
            ],
            Sources: [
                new("source-1", "Source 1", "source-url-1", "source-event-1"),
                new("source-2", "Source 2", "source-url-2", "source-event-2"),
            ],
            Magnitudes: [
                new("mag-1", "Magnitude 1", "mag-unit-1", "mag-desc-1", "mag-url-1"),
                new("mag-2", "Magnitude 2", "mag-unit-2", "mag-desc-2", "mag-url-2"),
            ]));
    }
}
