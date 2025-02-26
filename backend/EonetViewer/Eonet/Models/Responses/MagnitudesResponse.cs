namespace Eonet;

/// <summary>
/// Represents the response containing a list of event magnitudes in the EONET system.
/// </summary>
/// <param name="Title">The title of the response.</param>
/// <param name="Description">The description of the response.</param>
/// <param name="Link">The link to the response.</param>
/// <param name="Magnitudes">The list of magnitudes in the response.</param>
public record MagnitudesResponse(
    string Title,
    string Description,
    string Link,
    IList<Magnitude> Magnitudes
);
