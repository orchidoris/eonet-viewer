namespace Eonet;

/// <summary>
/// Response containing natural events from the EONET API according to the request filters.
/// </summary>
/// <param name="Events">Events in the EONET system matching request filters.</param>
/// <param name="Title">The title of the response, typically "EONET Events".</param>
/// <param name="Description">Description of the response, typically "Natural events from EONET".</param>
/// <param name="Url">Full url to the EONET events endpoint.</param>
public record EventsResponse(
    IReadOnlyList<Event> Events,
    string Title = "",
    string Description = "",
    string Url = "")
    : Response(Title, Description, Url);
