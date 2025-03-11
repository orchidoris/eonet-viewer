namespace Eonet;

internal static class CategoryExtensions
{
    public static CategoryWithLayers WithLayers(this Category category, IEnumerable<string> layers) =>
        new(category.Id, category.Title, category.Description, category.Url, category.LayersUrl, layers.ToList());
}
