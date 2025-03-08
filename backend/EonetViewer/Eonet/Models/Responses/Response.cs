using System.Text.Json.Serialization;

namespace Eonet;

/// <summary>
/// Represents the response containing the list of all available event sources in the EONET system.
/// </summary>
/// <param name="Title">Title of the response.</param>
/// <param name="Description">Description of the response.</param>
/// <param name="Url">Full url to this resource in the EONET system.</param>
public record Response(
    string Title,
    string Description,
    [property: JsonPropertyName("link")]
    string Url);
