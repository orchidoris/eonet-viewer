namespace Eonet;

/// <summary>
/// Represents the response containing a list of event magnitudes in the EONET system.
/// </summary>
/// <param name="Magnitudes">All available event magnitudes in the EONET system.</param>
/// <param name="Title">Title of the response, typically "EONET Event Magnitudes".</param>
/// <param name="Description">Description of the response, typically "List of all the available event magnitudes in the EONET system".</param>
/// <param name="Url">Full url to the EONET magnitudes endpoint.</param>
public record MagnitudesResponse(
    IReadOnlyList<Magnitude> Magnitudes,
    string Title = "",
    string Description = "",
    string Url = "")
    : Response(Title, Description, Url);
