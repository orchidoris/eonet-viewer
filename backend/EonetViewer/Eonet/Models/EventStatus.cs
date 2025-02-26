namespace Eonet.Models;

/// <summary>
/// Events that have ended are assigned a closed date, and the existence of that date will allow 
/// you to filter for only-open or only-closed events. Omitting the status parameter will return 
/// only the currently open events (default). Using “all” will list open and closed values.
/// </summary>
public enum EventStatus
{
    Open,
    Closed,
    All
}
