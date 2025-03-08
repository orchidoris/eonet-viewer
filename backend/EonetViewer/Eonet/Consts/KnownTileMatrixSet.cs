using System.Collections.Immutable;

namespace Eonet;

/// <summary>
/// Known values of layer's tile matrix set (TILEMATRIXSET parameter).
/// </summary>
public static class KnownTileMatrixSet
{
    public static readonly IImmutableList<string> All = typeof(KnownCategoryId).GetStringConsts();

    public const string _250m = "250m";
    public const string _500m = "500m";
    public const string _1km = "1km";
    public const string _2km = "2km";
}
