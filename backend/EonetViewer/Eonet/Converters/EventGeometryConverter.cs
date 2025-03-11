using System.Text.Json;
using System.Text.Json.Serialization;

namespace Eonet;

internal class EventGeometryConverter : JsonConverter<EventGeometry>
{
    public override bool CanConvert(Type objectType) => objectType == typeof(EventGeometry);

    public override EventGeometry Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return null!;
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException($"Expected null, object or array token but received {reader.TokenType}");

        var document = JsonDocument.ParseValue(ref reader);
        JsonElement value = document.RootElement;

        if (!value.TryGetProperty("type", out JsonElement token))
            throw new JsonException($"A mandatory JSON \"type\" property is nissing in {nameof(EventGeometry)} pbject");

        if (!Enum.TryParse(token.GetString(), true, out GeometryType geoJsonType))
            throw new JsonException($"Type must be one of: {GeometryType.Point} or {GeometryType.Polygon}");

        return geoJsonType switch
        {
            GeometryType.Point => value.Deserialize<EventPointGeometry>(options)!,
            GeometryType.Polygon => value.Deserialize<EventPolygonGeometry>(options)!,
            _ => throw new NotSupportedException($"Type {Enum.GetName(geoJsonType)} is not supported by {nameof(EventGeometryConverter)}")
        };
    }

    public override void Write(Utf8JsonWriter writer, EventGeometry value, JsonSerializerOptions options)
    {
        switch (value.Type)
        {
            case GeometryType.Point:
                JsonSerializer.Serialize(writer, (EventPointGeometry)value);
                break;
            case GeometryType.Polygon:
                JsonSerializer.Serialize(writer, (EventPolygonGeometry)value);
                break;
            default:
                throw new NotSupportedException($"Type {Enum.GetName(value.Type)} is not supported by {nameof(EventGeometryConverter)}");
        }
    }
}
