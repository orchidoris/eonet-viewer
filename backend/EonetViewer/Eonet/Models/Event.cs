using GeoJSON.Text.Geometry;
using System.Text.Json.Serialization;

namespace Eonet;

/// <summary>
/// Event from the EONET API.
/// </summary>
/// <param name="Id">Unique event id.</param>
/// <param name="Title">Title of the event.</param>
/// <param name="Description">Optional longer description of the event. Most likely only a sentence or two.</param>
/// <param name="Link">Full link to the API endpoint for this specific event.</param>
/// <param name="ClosedDate">Event is deemed “closed” when it has ended. The closed field will include a date/time when the event has ended. Depending upon the nature of the event, the closed value may or may not accurately represent the absolute ending of the event. If the event is open, this will show “null”.</param>
/// <param name="Categories">One or more categories assigned to the event.</param>
/// <param name="EventSource">One or more sources that refer to more information about the event.</param>
/// <param name="Geometry">One or more event geometries are the pairing of a specific date/time with a location. The date/time will most likely be 00:00Z unless the source provided a particular time. The geometry will be a GeoJSON object of either type “Point” or “Polygon”.</param>
public record Event(
    string Id,
    string Title,
    string? Description,
    string Link,
    [property: JsonPropertyName("closed")]
    DateTimeOffset? ClosedDate,
    IList<EventCategory> Categories,
    IList<EventSource> Sources,
    IList<EventGeometry> Geometry
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
/// Mapping from an event to the specific event url in the original source.
/// </summary>
/// <param name="Id">Unique source type id.</param>
/// <param name="Url">Url of this specific event in the original source.</param>
public record EventSource(
    string Id,
    string Url
);

/// <summary>
/// Geometry of an event.
/// </summary>
/// <param name="Date">Date and time of the event.</param>
/// <param name="Type">Type of geometry, usually "Point" but "Polygon" is possible.</param>
/// <param name="Coordinates">Coordinates of the event.</param>
/// <param name="MagnitudeUnit">Unit of the magnitude value.</param>
/// <param name="MagnitudeValue">Magnitude value of the event.</param>
public record EventGeometry(
    DateTimeOffset Date,
    string Type,
    Position Coordinates,
    string? MagnitudeUnit,
    double? MagnitudeValue
);
