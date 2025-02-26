namespace Eonet;

/// <summary>
/// Response containing natural events from the EONET API.
/// </summary>
/// <param name="Title">Title of the response, typically "EONET Events".</param>
/// <param name="Description">Description of the response, typically "Natural events from EONET".</param>
/// <param name="Link">The full link to the EONET API endpoint for this specific resource.</param>
/// <param name="Events">A list of natural events.</param>
public record EventsResponse(
    string Title,
    string Description,
    string Link,
    IList<Event> Events
);
