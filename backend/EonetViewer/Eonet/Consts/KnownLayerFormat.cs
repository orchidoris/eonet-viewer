using System.Collections.Immutable;

namespace Eonet;

/// <summary>
/// Known values of layer's format (FORMAT parameter).
/// </summary>
public static class KnownLayerFormat
{
    public static readonly IImmutableList<string> All = typeof(KnownCategoryId).GetStringConsts();

    public const string ImagePng = "image/png";
    public const string ImageJpeg = "image/jpeg";
}
