using System.Text.Json.Serialization;

namespace Eonet;

/// <summary>
/// Represents a source in the system.
/// </summary>
public record Source
{
    /// <summary> Unique id for this type. </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary> The title of this source. </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary> The homepage URL for the source. </summary>
    [JsonPropertyName("source")]
    public string SourceUrl { get; init; } = string.Empty;

    /// <summary> Full link to the API endpoint for this specific source. </summary>
    public string Link { get; init; } = string.Empty;
}
