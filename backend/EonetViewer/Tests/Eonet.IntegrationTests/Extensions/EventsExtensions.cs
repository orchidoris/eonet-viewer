namespace Eonet.IntegrationTests.Extensions;

internal static class EventsExtensions
{
    public static T MinMax<T>(this IEnumerable<Event> events, Func<EventGeometry, T> getValue)
        where T : IComparable<T>
    {
        return events.Min(e => e.Geometry.Max(getValue))!;
    }

    public static T MaxMin<T>(this IEnumerable<Event> events, Func<EventGeometry, T> getValue)
        where T : IComparable<T>
    {
        return events.Max(e => e.Geometry.Min(getValue))!;
    }
}
