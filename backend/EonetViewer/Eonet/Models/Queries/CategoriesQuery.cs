using Refit;

namespace Eonet;

/// <summary>
/// Represents the query parameters for filtering events by category in the EONET API.
/// </summary>
public record CategoriesQuery
{
    /// <summary>
    /// Filter the events by the Category ID.
    /// </summary>
    public string? CategoryId { get; init; } = null;

    /// <summary>
    /// Filter the events by the Source. Multiple sources can be included, separated by commas, operating as a boolean OR.
    /// </summary>
    public string? Source { get; init; } = null;

    /// <summary>
    /// Filter events by their status: "open", "closed", or "all". If omitted, only open events are returned.
    /// </summary>
    public string? Status { get; init; } = null;

    /// <summary>
    /// Limit the number of events returned by the query.
    /// </summary>
    [AliasAs("limit")]
    public int? Limit { get; init; } = null;

    /// <summary>
    /// Limit the number of prior days (including today) for which events will be returned.
    /// </summary>
    public int? Days { get; init; } = null;

    /// <summary>
    /// Start date for filtering events in the format YYYY-MM-DD.
    /// </summary>
    public string? Start { get; init; } = null;

    /// <summary>
    /// End date for filtering events in the format YYYY-MM-DD.
    /// </summary>
    public string? End { get; init; } = null;
}
