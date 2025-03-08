using System.Text.Json.Serialization;

namespace Eonet;

/// <summary>
/// Represents a layer in the system.
/// </summary>
/// <param name="Name">The name of the layer as specified by the source web service found at serviceUrl.</param>
/// <param name="ServiceUrl">The base URL of the web service.</param>
/// <param name="ServiceTypeId">
/// A string to indicate the type (WMS, WMTS, etc.) 
/// and version (1.0.0, 1.3.0, etc.) of the web service found at serviceUrl.
/// </param>
/// <param name="Parameters">
/// Zero or more URL parameters that are pertinent to the construction 
/// of a properly-formatted request to the web service found at serviceUrl.
/// </param>
public record Layer(
    [property: JsonPropertyName("name")]
    string Id,
    string ServiceUrl,
    string ServiceTypeId,
    IReadOnlyList<LayerParameters> Parameters);

/// <summary>
/// Represents a layer in the system with mapping to categories available for it.
/// </summary>
/// <param name="Name">The name of the layer as specified by the source web service found at serviceUrl.</param>
/// <param name="ServiceUrl">The base URL of the web service.</param>
/// <param name="ServiceTypeId">
/// A string to indicate the type (WMS, WMTS, etc.) 
/// and version (1.0.0, 1.3.0, etc.) of the web service found at serviceUrl.
/// </param>
/// <param name="Parameters">
/// Zero or more URL parameters that are pertinent to the construction 
/// of a properly-formatted request to the web service found at serviceUrl.
/// </param>
/// <param name="Categories">Ids of all categories available for this layer.</param>
public record LayerWithCategories(
    string Id,
    string ServiceUrl,
    string ServiceTypeId,
    IReadOnlyList<LayerParameters> Parameters,
    IReadOnlyList<string> Categories)
    : Layer(Id, ServiceUrl, ServiceTypeId, Parameters)
{
    public LayerWithCategories(Layer layer, IReadOnlyList<string> categories)
        : this(layer.Id, layer.ServiceUrl, layer.ServiceTypeId, layer.Parameters, categories)
    { }
}

/// <summary>
/// Parameters of a layer
/// </summary>
/// <param name="Format"></param>
/// <param name="TileMatrixSet"></param>
public record LayerParameters(
    [property: JsonPropertyName("FORMAT")]
    string? Format = null,
    [property: JsonPropertyName("TILEMATRIXSET")]
    string? TileMatrixSet = null);
