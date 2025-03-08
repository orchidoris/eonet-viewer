using System.Text.Json.Serialization;

namespace Eonet;

/// <summary>
/// Represents a magnitude in the EONET system.
/// </summary>
/// <param name="Id">The unique identifier of the magnitude.</param>
/// <param name="Title">The full name of the magnitude.</param>
/// <param name="Unit">The unit of the magnitude.</param>
/// <param name="Description">The description of the magnitude.</param>
/// <param name="EventsUrl">Full url to events filtered by this magnitude.</param>
public record Magnitude(
    string Id,
    [property: JsonPropertyName("name")]
    string Title,
    string Unit,
    string Description,
    [property: JsonPropertyName("link")]
    string EventsUrl
);
