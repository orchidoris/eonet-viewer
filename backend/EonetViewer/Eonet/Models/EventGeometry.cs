using GeoJSON.Text.Geometry;
using System.Text.Json.Serialization;

namespace Eonet;

/// <summary>
/// Geometry of an event.
/// </summary>
/// <param name="Date">Date and time of the event.</param>
/// <param name="Type">Type of geometry, "Point" or "Polygon".</param>
/// <param name="MagnitudeUnit">Unit of the magnitude value.</param>
/// <param name="MagnitudeValue">Magnitude value of the event.</param>
public abstract record EventGeometry(
    DateTimeOffset Date,
    GeometryType Type,
    string? MagnitudeUnit,
    double? MagnitudeValue
);

/// <summary>
/// Point geometry of an event.
/// </summary>
/// <param name="Date">Date and time of the event.</param>
/// <param name="Type">Type of geometry, always Point for this type.</param>
/// <param name="Coordinates">Coordinates of the event.</param>
/// <param name="MagnitudeUnit">Unit of the magnitude value.</param>
/// <param name="MagnitudeValue">Magnitude value of the event.</param>
public record EventPointGeometry(
    DateTimeOffset Date,
    Position Coordinates,
    GeometryType Type = GeometryType.Point,
    string? MagnitudeUnit = null,
    double? MagnitudeValue = null
) : EventGeometry(
    Date,
    Type == GeometryType.Point
        ? GeometryType.Point
        : throw new ArgumentException($"{GeometryType.Point} type expected, but encountered {Type}"),
    MagnitudeUnit,
    MagnitudeValue);

/// <summary>
/// Polygon geometry of an event.
/// </summary>
/// <param name="Date">Date and time of the event.</param>
/// <param name="Type">Type of geometry, always Polygon for this type.</param>
/// <param name="Coordinates">Polygon coordinates of the event.</param>
/// <param name="MagnitudeUnit">Unit of the magnitude value.</param>
/// <param name="MagnitudeValue">Magnitude value of the event.</param>
public record EventPolygonGeometry(
    DateTimeOffset Date,
    IReadOnlyList<LineString> Coordinates,
    GeometryType Type = GeometryType.Polygon,
    
    string? MagnitudeUnit = null,
    double? MagnitudeValue = null
) : EventGeometry(
    Date,
    Type == GeometryType.Polygon 
        ? GeometryType.Polygon
        : throw new ArgumentException($"{GeometryType.Polygon} type expected, but encountered {Type}"),
    MagnitudeUnit,
    MagnitudeValue)
{
}

/// <summary>
/// Type of event geometry.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GeometryType
{
    Point,
    Polygon,
}
