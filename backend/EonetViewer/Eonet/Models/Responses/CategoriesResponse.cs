namespace Eonet;

/// <summary>
/// Represents the response containing the list of all available event categories in the EONET system.
/// </summary>
public record CategoriesResponse
{
    /// <summary>
    /// The title of the response, typically "EONET Event Categories".
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Description of the response, typically "List of all the available event categories in the EONET system".
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// The full link to the API endpoint for this specific resource.
    /// </summary>
    public string Link { get; init; } = string.Empty;

    /// <summary>
    /// All available event categories in the EONET system.
    /// </summary>
    public IList<Category> Categories { get; init; } = [];
}
