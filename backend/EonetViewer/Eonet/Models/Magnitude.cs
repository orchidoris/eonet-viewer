namespace Eonet;

/// <summary>
/// Represents a magnitude in the EONET system.
/// </summary>
/// <param name="Id">The unique identifier of the magnitude.</param>
/// <param name="Name">The full name of the magnitude.</param>
/// <param name="Unit">The unit of the magnitude.</param>
/// <param name="Description">The description of the magnitude.</param>
/// <param name="Link">The link to the magnitude details.</param>
public record Magnitude(
    string Id,
    string Name,
    string Unit,
    string Description,
    string Link
);
