using System.Text.Json.Serialization;

namespace Eonet;

/// <summary>
/// Represents a category in the system.
/// </summary>
/// <param name="Id">Unique id for this category.</param>
/// <param name="Title">Title of the category.</param>
/// <param name="Description">Description of the category; addressing the scope.</param>
/// <param name="Url">Full url to this category.</param>
/// <param name="LayersUrl">Full url to layers filtered by this category.</param>
public record Category(
    string Id,
    string Title,
    string Description = "",
    [property: JsonPropertyName("link")]
    string Url = "",
    [property: JsonPropertyName("layers")]
    string LayersUrl = "");

/// <summary>
/// Represents a category in the system with mapping to layers available for it.
/// </summary>
/// <param name="Id">Unique id for this category.</param>
/// <param name="Title">Title of the category.</param>
/// <param name="Description">Description of the category; addressing the scope.</param>
/// <param name="Url">Full url to this category.</param>
/// <param name="LayersUrl">Full url to layers filtered by this category.</param>
/// <param name="Layers">Ids of all layers available for this category.</param>
public record CategoryWithLayers(
    string Id,
    string Title,
    string Description = "",
    string Url = "",
    string LayersUrl = "",
    IReadOnlyList<string> Layers = default!)
    : Category(Id, Title, Description, Url, LayersUrl);
