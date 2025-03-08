using System.Collections.Immutable;

namespace Eonet;

/// <summary>
/// Known event source id in the EONET system.
/// </summary>
public static class KnownSourceId
{
    public static readonly IImmutableList<string> All = typeof(KnownSourceId).GetStringConsts();

    public const string AVO = "AVO";
    public const string ABFIRE = "ABFIRE";
    public const string AU_BOM = "AU_BOM";
    public const string BYU_ICE = "BYU_ICE";
    public const string BCWILDFIRE = "BCWILDFIRE";
    public const string CALFIRE = "CALFIRE";
    public const string CEMS = "CEMS";
    public const string EO = "EO";
    public const string Earthdata = "Earthdata";
    public const string FEMA = "FEMA";
    public const string FloodList = "FloodList";
    public const string GDACS = "GDACS";
    public const string GLIDE = "GLIDE";
    public const string InciWeb = "InciWeb";
    public const string IRWIN = "IRWIN";
    public const string IDC = "IDC";
    public const string JTWC = "JTWC";
    public const string MRR = "MRR";
    public const string MBFIRE = "MBFIRE";
    public const string NASA_ESRS = "NASA_ESRS";
    public const string NASA_DISP = "NASA_DISP";
    public const string NASA_HURR = "NASA_HURR";
    public const string NOAA_NHC = "NOAA_NHC";
    public const string NOAA_CPC = "NOAA_CPC";
    public const string PDC = "PDC";
    public const string ReliefWeb = "ReliefWeb";
    public const string SIVolcano = "SIVolcano";
    public const string NATICE = "NATICE";
    public const string UNISYS = "UNISYS";
    public const string USGS_EHP = "USGS_EHP";
    public const string USGS_CMT = "USGS_CMT";
    public const string HDDS = "HDDS";
    public const string DFES_WA = "DFES_WA";
}
