using System.Collections.Immutable;
using System.Reflection;

namespace Eonet;

internal static class ConstExtensions
{
    public static IImmutableList<string> GetStringConsts(this Type type) => type
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
        .Select(static c => (string)c.GetValue(null)!)
        .ToImmutableArray();
}
