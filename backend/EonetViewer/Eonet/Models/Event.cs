using System.Text.Json.Serialization;

namespace Eonet;

/// <summary>
/// Event from the EONET API.
/// </summary>
/// <param name="Id">Unique event id.</param>
/// <param name="Title">Title of the event.</param>
/// <param name="Description">Optional description of the event, only a sentence or two.</param>
/// <param name="Url">Full link to the API endpoint for this specific event.</param>
/// <param name="ClosedDate">End date/time available for closed event, null if open.</param>
/// <param name="Categories">One or more categories assigned to the event.</param>
/// <param name="Sources">One or more sources that refer to more information about the event.</param>
/// <param name="Geometry">
/// One or more event geometries are the pairing of a specific date/time with a location.
/// The date/time will most likely be 00:00Z unless the source provided a particular time.
/// The geometry will be a GeoJSON object of either type “Point” or “Polygon”.
/// </param>
public record Event(
    string Id,
    string Title,
    string? Description,
    [property: JsonPropertyName("link")]
    string Url,
    [property: JsonPropertyName("closed")]
    DateTimeOffset? ClosedDate,
    IReadOnlyList<EventCategory> Categories,
    IReadOnlyList<EventSource> Sources,
    IReadOnlyList<EventGeometry> Geometry
);

/// <summary>
/// Mapping from an event to a category.
/// </summary>
/// <param name="Id">Unique category id.</param>
/// <param name="Title">Title of the category.</param>
public record EventCategory(
    string Id,
    string Title
);

/// <summary>
/// Mapping from an event to an original source and a url in the source.
/// </summary>
/// <param name="Id">Unique source type id.</param>
/// <param name="Url">Url of the event in the original source.</param>
public record EventSource(
    string Id,
    [property: JsonPropertyName("url")]
    string EventSourceUrl
);
