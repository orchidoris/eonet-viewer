namespace Eonet;

/// <summary>
/// Represents a category in the system.
/// </summary>
public record Category
{
    /// <summary> Unique id for this category. </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary> The title of the category. </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary> Longer description of the category, addressing the scope. </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary> Full link to the API endpoint for this specific category. </summary>
    public string Link { get; init; } = string.Empty;

    /// <summary> Service endpoint that points to the Layers API endpoint filtered to return only layers from this category. </summary>
    public string Layers { get; init; } = string.Empty;
}

