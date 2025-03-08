using Refit;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
    internal Task<IApiResponse<EventsResponse>> GetEventsRaw([Query] RawEventsQuery? query);

    /// <summary>
    /// Retrieves a list of events based on the provided filters.
    /// </summary>
    /// <param name="query">Filters applied to the event request.</param>
    /// <returns>Events matching provided filters.</returns>
    Task<IApiResponse<EventsResponse>> GetEvents(EventsFilters? filters = null) => GetEventsRaw(filters.ToRawQuery());

    /// <summary>
    /// Retrieves the list of acceptable sources from which the event was first curated,
    /// although there can be multiple sources per event.
    /// Source(s) can be used to filter the output of the Events API.
    /// </summary>
    /// <returns>A list of available sources.</returns>
    [Get("/sources")]
    Task<IApiResponse<SourcesResponse>> GetSources();

    /// <summary>
    /// Retrieves the list of available magnitudes for events.
    /// </summary>
    /// <returns>A list of available magnitudes.</returns>
    [Get("/magnitudes")]
    Task<IApiResponse<MagnitudesResponse>> GetMagnitudes();

    /// <summary>
    /// Fetches a list of events filtered by the provided category query parameters.
    /// </summary>
    /// <param name="query">Filters applied to the categories request.</param>
    /// <returns>List of categories filtered according to the query parameters.</returns>
    [Get("/categories")]
    Task<IApiResponse<CategoriesResponse>> GetCategories();

    /// <summary>
    /// Fetches a list of layers filtered by category.
    /// </summary>
    /// <param name="categoryId">The category ID to filter the layers.</param>
    /// <returns>List of layers filtered by the specified category.</returns>
    [Get("/layers/{categoryId}")]
    Task<IApiResponse<LayersResponse>> GetLayers(string categoryId);

    /// <summary>
    /// Fetches all available categories, layers, sources, and magnitudes.
    /// Includes categories-layers many-to-many mapping inlined into both categories and layers.
    /// </summary>
    async Task<IApiResponse<ContextResponse>> GetContext()
    {
        // ⚠️ Premature optimization.
        //    In a real-world business app, the performance gain achieved
        //    with the parallelization below in such low-traffic method is not as valuable
        //    as the lost code redability leading to increased cost of maintanence.
        // But hey! It's my pet-project and I'm having fun here playing with C# Tasks ⭐

        var cts = new CancellationTokenSource();
        IApiResponse<ContextResponse>? failedApiResponse = null;
        bool cancelIfNotSuccessful<T>(Task<IApiResponse<T>> task) where T : class
        {
            if (task.Result.IsSuccessful == true) return false;

            failedApiResponse = task.Result?.ToApiResponse<ContextResponse>();
            cts.Cancel();
            return true;
        }

        var sourcesTask = GetSources();
        var processSourcesTask = sourcesTask.ContinueWith(cancelIfNotSuccessful);

        var magnitudesTask = GetMagnitudes();
        var processMagnitudesTask = magnitudesTask.ContinueWith(cancelIfNotSuccessful);

        var categoriesTask = GetCategories();
        var layersTask = categoriesTask.ContinueWith(async task =>
        {
            if (cts.Token.IsCancellationRequested || cancelIfNotSuccessful(task))
                return (new List<CategoryWithLayers>(), new List<LayerWithCategories>());

            var categories = task.Result.Content!.Categories;
            var categoriesWithLayers = new List<CategoryWithLayers>(categories.Count);
            var uniqueLayersWithCategories = new Dictionary<string, (Layer Layer, List<string> Categories)>(KnownLayerId.All.Count);

            var layersTasks = categories.Select(async category =>
            {
                if (cts.Token.IsCancellationRequested) return;

                // EONET v3 layers endpoint returns result without mapping to categories
                // so we need to call it for each category separatelly.
                var categoryLayersTask = GetLayers(category.Id);
                var categoryLayersApiResponse = await categoryLayersTask;
                if (cancelIfNotSuccessful(categoryLayersTask))
                    return;

                var categoryLayers = categoryLayersApiResponse.Content!.Categories[0].Layers;
                categoriesWithLayers.Add(new(category, categoryLayers.Select(l => l.Id).ToList()));

                // collect unique layers and category ids associated with them
                foreach (var layer in categoryLayers)
                    if (!uniqueLayersWithCategories.TryAdd(layer.Id, new(layer, [category.Id])))
                        uniqueLayersWithCategories[layer.Id].Categories.Add(category.Id);
            });

            await Task.WhenAll(layersTasks);

            return (categoriesWithLayers, uniqueLayersWithCategories.Values.Select(v => new LayerWithCategories(v.Layer, [.. v.Categories])).ToList());
        }).Unwrap();

        await Task.WhenAll(processSourcesTask, processMagnitudesTask, layersTask);
        if (failedApiResponse != null)
            return failedApiResponse;

        var (categories, layers) = layersTask.Result;
        return categoriesTask.Result.ToApiResponse<ContextResponse>(new(
            categories,
            sourcesTask.Result.Content!.Sources,
            magnitudesTask.Result.Content!.Magnitudes,
            layers));
    }
}
