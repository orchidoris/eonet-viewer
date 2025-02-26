namespace Eonet;

/// <summary>
/// Represents the response containing a list of web service layers in the EONET system.
/// </summary>
public record LayersResponse
{
    /// <summary>
    /// The title of the response, typically "EONET Web Service Layers".
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// A description of the response, typically "List of web service layers in the EONET system".
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// The full link to the API endpoint for this specific resource.
    /// </summary>
    public string Link { get; init; } = string.Empty;

    /// <summary>
    /// A list of categories, each containing layers related to EONET events.
    /// </summary>
    public IList<LayerCategory> Categories { get; init; } = [];
}

/// <summary>
/// Represents a category of layers within the EONET system.
/// </summary>
public record LayerCategory
{
    /// <summary>
    /// A list of layers related to a particular category.
    /// </summary>
    public IList<Layer> Layers { get; init; } = [];
}

