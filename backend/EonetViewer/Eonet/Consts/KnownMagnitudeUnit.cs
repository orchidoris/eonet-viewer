using System.Collections.Immutable;

namespace Eonet;

public static class KnownMagnitudeUnit
{
    public static readonly IImmutableList<string> All = typeof(KnownCategoryId).GetStringConsts();

    public const string acres = "acres";
    public const string hectare = "hectare";
    public const string kts = "kts";
    public const string Mb = "Mb";
    public const string Mi_Mwp = "Mi/Mwp";
    public const string Ml = "Ml";
    public const string Mw_Mww = "Mw/Mww";
    public const string Mwb = "Mwb";
    public const string Mwc = "Mwc";
    public const string Mwr = "Mwr";
    public const string NM_2 = "NM^2";
}
