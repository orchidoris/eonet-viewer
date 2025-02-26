using Refit;

namespace Eonet;

/// <summary>
/// Represents the EONET API client for fetching events.
/// </summary>
public interface IEonetClient
{
    [Get("/events/{eventId}")]
    Task<ApiResponse<EventsResponse>> GetEvent(string eventId);

    [Get("/events")]
    [QueryUriFormat(UriFormat.Unescaped)]
    internal Task<ApiResponse<EventsResponse>> GetEventsRaw([Query] RawEventsQuery? query);

    /// <summary>
    /// Retrieves a list of events from the EONET API based on the provided filters.
    /// </summary>
    /// <param name="filter">Filters applied to the event request.</param>
    /// <returns>A list of events from the EONET API.</returns>
    Task<ApiResponse<EventsResponse>> GetEvents(EventsQuery? query = null) => GetEventsRaw(query.ToRawQuery());

    /// <summary>
    /// Retrieves the list of acceptable sources from which the event was first curated,
    /// although there can be multiple sources per event.
    /// Source(s) can be used to filter the output of the Events API.
    /// </summary>
    /// <returns>A list of available sources.</returns>
    [Get("/sources")]
    Task<ApiResponse<SourcesResponse>> GetSources();

    /// <summary>
    /// Retrieves the list of available magnitudes for events.
    /// </summary>
    /// <returns>A list of available magnitudes.</returns>
    [Get("/magnitudes")]
    Task<ApiResponse<MagnitudesResponse>> GetMagnitudes();

    [Get("/categories/{categoryId}")]
    [QueryUriFormat(UriFormat.Unescaped)]
    protected Task<ApiResponse<CategoriesResponse>> GetCategories(string? categoryId = null, [Query] CategoriesQuery? query = null);

    /// <summary>
    /// Fetches a list of events filtered by the provided category query parameters.
    /// </summary>
    /// <param name="query">Filters applied to the categories request.</param>
    /// <returns>List of categories filtered according to the query parameters.</returns>
    Task<ApiResponse<CategoriesResponse>> GetCategories(CategoriesQuery? query = null) =>
        GetCategories(query?.CategoryId, query == null ? null : query with { CategoryId = null });

    /// <summary>
    /// Fetches a list of layers filtered by category.
    /// </summary>
    /// <param name="categoryId">The category ID to filter the layers.</param>
    /// <returns>List of layers filtered by the specified category.</returns>
    [Get("/layers/{categoryId}")]
    Task<ApiResponse<LayersResponse>> GetLayers(string? categoryId = null);
}
