using Refit;

namespace Eonet;

/// <summary>
/// Represents the EONET API client for fetching events.
/// </summary>
public interface IEonetClient
{
    [Get("/events/{eventId}")]
    Task<IApiResponse<EventsResponse>> GetEvent(string eventId);

    [Get("/events")]
    [QueryUriFormat(UriFormat.Unescaped)]
    internal Task<IApiResponse<EventsResponse>> GetEventsRaw([Query] RawEventsQuery? query, CancellationToken ct = default);

    /// <summary>
    /// Retrieves a list of events based on the provided filters.
    /// </summary>
    /// <param name="query">Filters applied to the event request.</param>
    /// <returns>Events matching provided filters.</returns>
    Task<IApiResponse<EventsResponse>> GetEvents(EventsFilters? filters = null, CancellationToken ct = default) =>
        GetEventsRaw(filters.ToRawQuery(), ct);

    /// <summary>
    /// Retrieves the list of acceptable sources from which the event was first curated,
    /// although there can be multiple sources per event.
    /// Source(s) can be used to filter the output of the Events API.
    /// </summary>
    /// <returns>A list of available sources.</returns>
    [Get("/sources")]
    Task<IApiResponse<SourcesResponse>> GetSources(CancellationToken ct = default);

    /// <summary>
    /// Retrieves the list of available magnitudes for events.
    /// </summary>
    /// <returns>A list of available magnitudes.</returns>
    [Get("/magnitudes")]
    Task<IApiResponse<MagnitudesResponse>> GetMagnitudes(CancellationToken ct = default);

    /// <summary>
    /// Fetches a list of events filtered by the provided category query parameters.
    /// </summary>
    /// <param name="query">Filters applied to the categories request.</param>
    /// <returns>List of categories filtered according to the query parameters.</returns>
    [Get("/categories")]
    Task<IApiResponse<CategoriesResponse>> GetCategories(CancellationToken ct = default);

    /// <summary>
    /// Fetches a list of layers filtered by category.
    /// </summary>
    /// <param name="categoryId">The category ID to filter the layers.</param>
    /// <returns>List of layers filtered by the specified category.</returns>
    [Get("/layers/{categoryId}")]
    Task<IApiResponse<LayersResponse>> GetLayers(string categoryId, CancellationToken ct = default);

    /// <summary>
    /// Fetches all available categories, layers, sources, and magnitudes.
    /// Includes categories-layers many-to-many mapping inlined into both categories and layers.
    /// </summary>
    async Task<IApiResponse<ContextResponse>> GetContext(CancellationToken ct = default)
    {
        // ⚠️ Premature optimization.
        //    In a real-world business app, the parallelization below complicates code maintainence
        //    while the performance gain is not nearly as valuable in such a low-traffic method.
        // But hey! It's my pet-project and I'm having fun here playing with C# Tasks ⭐

        var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        IApiResponse<ContextResponse>? failedApiResponse = null;
        bool cancelIfFailed(IApiResponse apiResponse)
        {
            if (apiResponse.IsSuccessful == true)
                return false;

            failedApiResponse = apiResponse.ToApiResponse<ContextResponse>();
            cts.Cancel();
            return true;
        }

        var sourcesTask = GetSources(cts.Token).ContinueWith(t => cancelIfFailed(t.Result) ? default : t.Result, cts.Token);
        var magnitudesTask = GetMagnitudes(cts.Token).ContinueWith(t => cancelIfFailed(t.Result) ? default : t.Result, cts.Token);
        var categoriesAndLayersTask = GetCategoriesAndLayers(cancelIfFailed, cts.Token);

        await Task.WhenAll(sourcesTask, magnitudesTask, categoriesAndLayersTask);
        if (failedApiResponse != null)
            return failedApiResponse;

        var (categories, layers) = categoriesAndLayersTask.Result;
        return sourcesTask.Result!.ToApiResponse<ContextResponse>(new(
            categories,
            sourcesTask.Result!.Content!.Sources,
            magnitudesTask.Result!.Content!.Magnitudes,
            layers));
    }

    private async Task<(IReadOnlyList<CategoryWithLayers>, IReadOnlyList<Layer>)> GetCategoriesAndLayers(
        Func<IApiResponse, bool> cancelIfFailed, CancellationToken ct = default)
    {
        return await GetCategories(ct).ContinueWith(async task =>
        {
            if (ct.IsCancellationRequested || cancelIfFailed(task.Result))
                return ([], []);

            var uniqueLayers = new Dictionary<string, Layer>(KnownLayerId.All.Count + 10);
            var categories = task.Result.Content!.Categories;
            var categoriesWithLayers = await Task.WhenAll(categories.Select(async category =>
            {
                if (ct.IsCancellationRequested) return null!;

                // EONET v3 layers endpoint returns result without mapping to categories
                // so we need to retrieve layers per category and receive the same layers multimple times.
                var categoryLayersApiResponse = await GetLayers(category.Id, ct);
                if (cancelIfFailed(categoryLayersApiResponse))
                    return null!;

                var layers = categoryLayersApiResponse.Content!.Categories[0].Layers;
                foreach (var layer in layers)
                    uniqueLayers.TryAdd(layer.Id, layer);

                return category.WithLayers(layers.Select(l => l.Id));
            }));

            return (categoriesWithLayers, uniqueLayers.Values.ToList());
        }, ct).Unwrap();
    }
}
