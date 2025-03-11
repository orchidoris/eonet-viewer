namespace Eonet;

public record ContextResponse(
    IReadOnlyList<CategoryWithLayers> Categories,
    IReadOnlyList<Source> Sources,
    IReadOnlyList<Magnitude> Magnitudes,
    IReadOnlyList<Layer> Layers);
