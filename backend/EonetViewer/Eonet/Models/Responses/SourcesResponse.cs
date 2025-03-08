namespace Eonet;

/// <summary>
/// Represents the response containing the list of all available event sources in the EONET system.
/// </summary>
/// <param name="Sources">All available event sources in the EONET system.</param>
/// <param name="Title">Title of the response, typically "EONET Event Sources".</param>
/// <param name="Description">Description of the response, typically "List of all the available event sources in the EONET system".</param>
/// <param name="Url">Full url to the EONET sources endpoint.</param>
public record SourcesResponse(
    IReadOnlyList<Source> Sources,
    string Title = "",
    string Description = "",
    string Url = "")
    : Response(Title, Description, Url);
