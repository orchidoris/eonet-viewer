using System.Collections.Immutable;

namespace Eonet;

/// <summary>
/// Known event later id in the EONET system.
/// </summary>
public static class KnownLayerId
{
    public static readonly IImmutableList<string> All = typeof(KnownLayerId).GetStringConsts();

    public const string AIRS_Precipitation_Day = "AIRS_Precipitation_Day";
    public const string AIRS_Precipitation_Night = "AIRS_Precipitation_Night";
    public const string MODIS_Aqua_CorrectedReflectance_Bands367 = "MODIS_Aqua_CorrectedReflectance_Bands367";
    public const string MODIS_Aqua_CorrectedReflectance_TrueColor = "MODIS_Aqua_CorrectedReflectance_TrueColor";
    public const string MODIS_Aqua_Data_No_Data = "MODIS_Aqua_Data_No_Data";
    public const string MODIS_Aqua_Snow_Cover = "MODIS_Aqua_Snow_Cover";
    public const string MODIS_Aqua_SurfaceReflectance_Bands121 = "MODIS_Aqua_SurfaceReflectance_Bands121";
    public const string MODIS_Aqua_SurfaceReflectance_Bands721 = "MODIS_Aqua_SurfaceReflectance_Bands721";
    public const string MODIS_Terra_CorrectedReflectance_Bands367 = "MODIS_Terra_CorrectedReflectance_Bands367";
    public const string MODIS_Terra_CorrectedReflectance_Bands721 = "MODIS_Terra_CorrectedReflectance_Bands721";
    public const string MODIS_Terra_CorrectedReflectance_TrueColor = "MODIS_Terra_CorrectedReflectance_TrueColor";
    public const string MODIS_Terra_Data_No_Data = "MODIS_Terra_Data_No_Data";
    public const string MODIS_Terra_Snow_Cover = "MODIS_Terra_Snow_Cover";
    public const string MODIS_Terra_SurfaceReflectance_Bands121 = "MODIS_Terra_SurfaceReflectance_Bands121";
    public const string MODIS_Terra_SurfaceReflectance_Bands721 = "MODIS_Terra_SurfaceReflectance_Bands721";
    public const string ndh_drought_hazard_frequency_distribution = "ndh-drought-hazard-frequency-distribution";
    public const string ndh_drought_mortality_risks_distribution = "ndh-drought-mortality-risks-distribution";
    public const string VIIRS_SNPP_CorrectedReflectance_BandsM11_I2_I1 = "VIIRS_SNPP_CorrectedReflectance_BandsM11-I2-I1";
    public const string VIIRS_SNPP_CorrectedReflectance_BandsM3_I3_M11 = "VIIRS_SNPP_CorrectedReflectance_BandsM3-I3-M11";
    public const string VIIRS_SNPP_CorrectedReflectance_TrueColor = "VIIRS_SNPP_CorrectedReflectance_TrueColor";
    public const string AIRS_CO_Total_Column_Day = "AIRS_CO_Total_Column_Day";
    public const string AIRS_CO_Total_Column_Night = "AIRS_CO_Total_Column_Night";
    public const string AIRS_Dust_Score_Ocean_Day = "AIRS_Dust_Score_Ocean_Day";
    public const string AIRS_Dust_Score_Ocean_Night = "AIRS_Dust_Score_Ocean_Night";
    public const string AURA_NO2_D = "AURA_NO2_D";
    public const string MODIS_Aqua_Aerosol = "MODIS_Aqua_Aerosol";
    public const string MODIS_Combined_Value_Added_AOD = "MODIS_Combined_Value_Added_AOD";
    public const string MODIS_Terra_Aerosol = "MODIS_Terra_Aerosol";
    public const string MODIS_Aqua_CorrectedReflectance_Bands721 = "MODIS_Aqua_CorrectedReflectance_Bands721";
    public const string MODIS_Water_Mask = "MODIS_Water_Mask";
    public const string ndh_flood_hazard_frequency_distribution = "ndh-flood-hazard-frequency-distribution";
    public const string ndh_flood_mortality_risks_distribution = "ndh-flood-mortality-risks-distribution";
    public const string TRMM_3B43D = "TRMM_3B43D";
    public const string MODIS_Aqua_Brightness_Temp_Band31_Day = "MODIS_Aqua_Brightness_Temp_Band31_Day";
    public const string MODIS_Aqua_Brightness_Temp_Band31_Night = "MODIS_Aqua_Brightness_Temp_Band31_Night";
    public const string MODIS_Aqua_Land_Surface_Temp_Day = "MODIS_Aqua_Land_Surface_Temp_Day";
    public const string MODIS_Aqua_Land_Surface_Temp_Night = "MODIS_Aqua_Land_Surface_Temp_Night";
    public const string MODIS_Aqua_Sea_Ice = "MODIS_Aqua_Sea_Ice";
    public const string MODIS_Terra_Brightness_Temp_Band31_Day = "MODIS_Terra_Brightness_Temp_Band31_Day";
    public const string MODIS_Terra_Brightness_Temp_Band31_Night = "MODIS_Terra_Brightness_Temp_Band31_Night";
    public const string MODIS_Terra_Land_Surface_Temp_Day = "MODIS_Terra_Land_Surface_Temp_Day";
    public const string MODIS_Terra_Land_Surface_Temp_Night = "MODIS_Terra_Land_Surface_Temp_Night";
    public const string MODIS_Terra_Sea_Ice = "MODIS_Terra_Sea_Ice";
    public const string MODIS_Aqua_Water_Vapor_5km_Day = "MODIS_Aqua_Water_Vapor_5km_Day";
    public const string MODIS_Aqua_Water_Vapor_5km_Night = "MODIS_Aqua_Water_Vapor_5km_Night";
    public const string MODIS_Terra_Water_Vapor_5km_Day = "MODIS_Terra_Water_Vapor_5km_Day";
    public const string MODIS_Terra_Water_Vapor_5km_Night = "MODIS_Terra_Water_Vapor_5km_Night";
    public const string MYDAL2_D_CLD_FR = "MYDAL2_D_CLD_FR";
    public const string ndh_cyclone_hazard_frequency_distribution = "ndh-cyclone-hazard-frequency-distribution";
    public const string ndh_cyclone_mortality_risks_distribution = "ndh-cyclone-mortality-risks-distribution";
    public const string MOD11C1_D_LSTDA = "MOD11C1_D_LSTDA";
    public const string MOD11C1_D_LSTNI = "MOD11C1_D_LSTNI";
    public const string MOD_LSTAD_D = "MOD_LSTAD_D";
    public const string MOD_LSTAN_D = "MOD_LSTAN_D";
    public const string AIRS_Prata_SO2_Index_Day = "AIRS_Prata_SO2_Index_Day";
    public const string AIRS_Prata_SO2_Index_Night = "AIRS_Prata_SO2_Index_Night";
}
