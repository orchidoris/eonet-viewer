﻿namespace Eonet;

internal static class EventsFiltersExtensions
{
    public static RawEventsQuery? ToRawQuery(this EventsFilters? query) =>
        query == null
        ? null
        : new RawEventsQuery(
            source: query.Sources?.Any() == true ? query.Sources : null,
            category: query.Categories?.Any() == true ? query.Categories : null,
            status: query.Status == EventStatusFilter.Open ? null : query.Status.ToString().ToLower(),
            limit: query.Limit,
            days: query.DaysPrior,
            start: query.Start?.ToString("yyyy-MM-dd"),
            end: query.End?.ToString("yyyy-MM-dd"),
            magID: query.Magnitude?.Id,
            magMin: query.Magnitude?.Min,
            magMax: query.Magnitude?.Max,
            bbox: query.BoundingBox?.ToRawQuery());

    private static IReadOnlyList<double> ToRawQuery(this BoundingBox bbox) =>
        [bbox.MinLongitude, bbox.MaxLatitude, bbox.MaxLongitude, bbox.MinLatitude];
}
