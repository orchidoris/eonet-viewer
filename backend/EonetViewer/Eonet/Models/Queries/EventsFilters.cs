namespace Eonet;

/// <summary>
/// Represents filters for querying events from the EONET API.
/// </summary>
/// <param name="Sources">Filter by event sources, none - means all sources.</param>
/// <param name="Categories">Filter by event categories, none - means all categories.</param>
/// <param name="Status">Filter based on events having a closed date.</param>
/// <param name="Limit">Limits the number of events returned.</param>
/// <param name="DaysPrior">Limit the number of the days prior (including today) from which events will be returned.</param>
/// <param name="Start">Min date of events.</param>
/// <param name="End">Max date of events.</param>
/// <param name="Magnitude">A ceiling, floor, or range of magnitude values for the events.</param>
/// <param name="BoundingBox">A bounding box for event coordinates to fall into.</param>
public record EventsFilters(
    IReadOnlyList<string>? Sources = null,
    IReadOnlyList<string>? Categories = null,
    EventStatusFilter Status = default,
    int? Limit = null,
    int? DaysPrior = null,
    DateOnly? Start = null,
    DateOnly? End = null,
    MagnitudeFilter? Magnitude = null,
    BoundingBox? BoundingBox = null
);

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = 
    "Refit serializes this model as-is into query parameters for case-sensitive EONET API. " +
    "In the meanwile, the public API model follows .NET naming conventions.")]
internal record RawEventsQuery(
    IReadOnlyList<string>? source,
    IReadOnlyList<string>? category,
    string? status,
    int? limit,
    int? days,
    string? start,
    string? end,
    string? magID,
    double? magMin,
    double? magMax,
    IReadOnlyList<double>? bbox
);

/// <summary>Status filter based on events having closed date.</summary>
public enum EventStatusFilter
{
    /// <summary>Value matching events having no closed date.</summary>
    Open,

    /// <summary>Value matching events having closed date.</summary>
    Closed,

    /// <summary>Value matching all events, regardless of having a closed date or not.</summary>
    All
}

/// <summary>
/// Ceiling, floor, or range of magnitude values for the events to fall between (inclusive).
/// </summary>
/// <param name="Id">Unique id of magnitude unit.</param>
/// <param name="Min">Optional min magnitude value.</param>
/// <param name="Max">Optional max magnitude value.</param>
public record MagnitudeFilter(string Id, double? Min = null, double? Max = null);

/// <summary>
/// Bounding box to filter events by coordinates.
/// </summary>
/// <param name="MinLongitude">Minimum longitude (left boundary).</param>
/// <param name="MaxLatitude">Maximum latitude (top boundary).</param>
/// <param name="MaxLongitude">Maximum longitude (right boundary).</param>
/// <param name="MinLatitude">Minimum latitude (bottom boundary).</param>
public record BoundingBox(double MinLongitude, double MaxLatitude, double MaxLongitude, double MinLatitude);
