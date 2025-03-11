using System.Collections.Immutable;

namespace Eonet;

public static class KnownMagnitudesId
{
    public static readonly IImmutableList<string> All = typeof(KnownCategoryId).GetStringConsts();

    public const string ac = "ac";
    public const string ha = "ha";
    public const string mag_kts = "mag_kts";
    public const string mb = "mb";
    public const string mi = "mi";
    public const string ml = "ml";
    public const string mms = "mms";
    public const string mwb = "mwb";
    public const string mwc = "mwc";
    public const string mwr = "mwr";
    public const string sq_NM = "sq_NM";
}
