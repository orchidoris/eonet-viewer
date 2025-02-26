namespace Eonet;

/// <summary>
/// Represents the response containing the list of all available event sources in the EONET system.
/// </summary>
public record SourcesResponse
{
    /// <summary>
    /// The title of the response, typically "EONET Event Sources".
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Description of the response, typically "List of all the available event sources in the EONET system".
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// The full link to the API endpoint for this specific resource.
    /// </summary>
    public string Link { get; init; } = string.Empty;

    /// <summary>
    /// A list of available event sources in the EONET system.
    /// </summary>
    public IList<Source> Sources { get; init; } = [];
}


