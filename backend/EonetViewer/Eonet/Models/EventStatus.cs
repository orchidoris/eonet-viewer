namespace Eonet.Models;

/// <summary>Status filter based on events having closed date.</summary>
public enum EventStatus
{
    /// <summary>Value matching events having no closed date.</summary>
    Open,

    /// <summary>Value matching events having closed date.</summary>
    Closed,

    /// <summary>Value matching all events, both open and closed ones.</summary>
    All
}
