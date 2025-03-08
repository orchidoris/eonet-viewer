namespace Eonet;

/// <summary>
/// Response containing the list of available event categories in the EONET system.
/// </summary>
/// <param name="Categories">All available event categories in the EONET system.</param>
/// <param name="Title">Title of the response, typically "EONET Event Categories".</param>
/// <param name="Description">Description of the response, typically "List of all the available event categories in the EONET system".</param>
/// <param name="Url">Full url to the EONET categories endpoint..</param>
public record CategoriesResponse(
    IReadOnlyList<Category> Categories,
    string Title = "",
    string Description = "",
    string Url = "")
    : Response(Title, Description, Url);
