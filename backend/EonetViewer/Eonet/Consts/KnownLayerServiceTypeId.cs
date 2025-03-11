using System.Collections.Immutable;

namespace Eonet;

public static class KnownLayerServiceTypeId
{
    public static readonly IImmutableList<string> All = typeof(KnownCategoryId).GetStringConsts();

    public const string WMTS_1_0_0 = "WMTS_1_0_0";
    public const string WMS_1_1_1 = "WMS_1_1_1";
}
