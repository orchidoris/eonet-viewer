using System.Collections.Immutable;

namespace Eonet;

/// <summary>
/// Known event category id in the EONET system.
/// </summary>
public static class KnownCategoryId
{
    public static readonly IImmutableList<string> All = typeof(KnownCategoryId).GetStringConsts();

    public const string Drought = "drought";
    public const string DustHaze = "dustHaze";
    public const string Earthquakes = "earthquakes";
    public const string Floods = "floods";
    public const string Landslides = "landslides";
    public const string Manmade = "manmade";
    public const string SeaLakeIce = "seaLakeIce";
    public const string SevereStorms = "severeStorms";
    public const string Snow = "snow";
    public const string TempExtremes = "tempExtremes";
    public const string Volcanoes = "volcanoes";
    public const string WaterColor = "waterColor";
    public const string Wildfires = "wildfires";
}
