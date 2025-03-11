using System.Text.Json.Serialization;

namespace Eonet;

/// <summary>
/// Represents a source in the system.
/// </summary>
/// <param name="Source">Unique source id.</param>
/// <param name="Title">Source title.</param>
/// <param name="ExternalHomepageUrl">Homepage URL for the source.</param>
/// <param name="EventsUrl">Full url to events filtered by this source.</param>
public record Source(
    string Id,
    string Title,
    [property: JsonPropertyName("source")]
    string ExternalHomepageUrl,
    [property: JsonPropertyName("link")]
    string EventsUrl);
