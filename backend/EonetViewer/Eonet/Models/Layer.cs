namespace Eonet;

/// <summary>
/// Represents a layer in the system.
/// </summary>
public record Layer
{
    /// <summary>
    /// The name of the layer as specified by the source web service found at serviceUrl.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary> The base URL of the web service. </summary>
    public string ServiceUrl { get; init; } = string.Empty;

    /// <summary>
    /// A string to indicate the type (WMS, WMTS, etc.) 
    /// and version (1.0.0, 1.3.0, etc.) of the web service found at serviceUrl.
    /// </summary>
    public string ServiceTypeId { get; init; } = string.Empty;

    /// <summary>
    /// Zero or more URL parameters that are pertinent to the construction
    /// of a properly-formatted request to the web service found at serviceUrl.
    /// </summary>
    public IList<string> Parameters { get; init; } = [];
}
