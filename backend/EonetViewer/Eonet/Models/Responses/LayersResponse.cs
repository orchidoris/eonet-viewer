namespace Eonet;

/// <summary>
/// Response containing a list of web service layers in the EONET system.
/// </summary>
/// <param name="Categories">Event layers in the EONET system matching request filters.</param>
/// <param name="Title">Title of the response, typically "EONET Web Service Layers".</param>
/// <param name="Description">Description of the response, typically "List of web service layers in the EONET system".</param>
/// <param name="Url">Full url to the EONET layers endpoint.</param>
public record LayersResponse(
    IReadOnlyList<CategoryLayers> Categories,
    string Title = "",
    string Description = "",
    string Url = "")
    : Response(Title, Description, Url);

/// <summary>
/// Represents a category of layers within the EONET system.
/// </summary>
/// <param name="Id">Id of a category.</param>
/// <param name="Title">Title of a category</param>
/// <param name="Layers">List of layers available for a category.</param>
public record CategoryLayers(
    IReadOnlyList<Layer> Layers,
    int Id = default,
    string Title = "");
