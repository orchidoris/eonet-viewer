using Eonet;
using GeoJSON.Text.Geometry;
using Google.Protobuf.WellKnownTypes;
using Refit;

namespace EonetViewer.Api.Extensions;

public static class EventServiceExtensions
{
    public static EventsFilters FromProto(this Protos.EventsRequest request) => new(
        Sources: request.Sources,
        Categories: request.Categories,
        Status: request.Status.FromProto(),
        Limit: request.HasLimit ? request.Limit : null,
        Days: request.HasDays ? request.Days : null,
        Start: request.Start.ToOptionalDate(),
        End: request.End.ToOptionalDate(),
        Magnitude: request.Magnitude.FromProto(),
        BoundingBox: request.BoundingBox.FromProto()
    );

    public static Protos.EventsResponse ToProto(this IApiResponse<EventsResponse> apiResponse)
    {
        var proto = new Protos.EventsResponse
        {
            Url = apiResponse.RequestMessage?.RequestUri?.ToString(),
        };

        proto.Events.AddRange(apiResponse.Content?.Events.Select(e => e.ToProto()) ?? []);
        return proto;
    }

    public static Protos.EventsContextResponse ToProto(this IApiResponse<ContextResponse> contextApiResponse)
    {
        var proto = new Protos.EventsContextResponse();

        var contextResponse = contextApiResponse.Content;
        if (contextResponse == null)
            return proto;

        proto.Categories.AddRange(contextResponse.Categories.Select(c => c.ToProto()));
        proto.Layers.AddRange(contextResponse.Layers.Select(c => c.ToProto()));
        proto.Sources.AddRange(contextResponse.Sources.Select(c => c.ToProto()));
        proto.Magnitudes.AddRange(contextResponse.Magnitudes.Select(c => c.ToProto()));

        return proto;
    }

    public static MagnitudeFilter? FromProto(this Protos.MagnitudeFilter? magnitude) =>
        magnitude == null ? null : new(
            Id: magnitude.Id,
            Min: magnitude.Min,
            Max: magnitude.Max);

    public static BoundingBox? FromProto(this Protos.BoundingBox? bbox) =>
        bbox == null ? null : new(
            MinLongitude: bbox.MinLongitude,
            MinLatitude: bbox.MinLatitude,
            MaxLongitude: bbox.MaxLongitude,
            MaxLatitude: bbox.MaxLatitude);

    public static EventStatusFilter FromProto(this Protos.EventStatus status) => status switch
    {
        Protos.EventStatus.Open => EventStatusFilter.Open,
        Protos.EventStatus.Closed => EventStatusFilter.Closed,
        Protos.EventStatus.All => EventStatusFilter.All,
        _ => throw new ArgumentOutOfRangeException(nameof(status))
    };

    

    public static Protos.Event ToProto(this Event response)
    {
        var proto = new Protos.Event
        {
            Id = response.Id,
            Title = response.Title,
            Url = response.Url
        };

        if (response.Description != null) proto.Description = response.Description;
        if (response.ClosedDate != null) proto.ClosedDate = response.ClosedDate?.UtcDateTime.ToTimestamp();
        proto.Categories.AddRange(response.Categories.Select(c => c.ToProto()));
        proto.Sources.AddRange(response.Sources.Select(s => s.ToProto()));
        proto.Geometries.AddRange(response.Geometry.Select(g => g.ToProto()));

        return proto;
    }

    public static Protos.EventCategory ToProto(this EventCategory category) => new()
    {
        Id = category.Id,
        Title = category.Title
    };

    public static Protos.EventSource ToProto(this EventSource source) => new()
    {
        Id = source.Id,
        EventSourceUrl = source.EventSourceUrl
    };

    public static Protos.EventGeometry ToProto(this EventGeometry geometry)
    {
        var proto = new Protos.EventGeometry
        {
            Date = geometry.Date.UtcDateTime.ToTimestamp()
        };

        if (geometry.MagnitudeUnit != null) proto.MagnitudeUnit = geometry.MagnitudeUnit;
        if (geometry.MagnitudeValue != null) proto.MagnitudeValue = geometry.MagnitudeValue;

        switch (geometry)
        {
            case EventPointGeometry point:
                proto.Type = Protos.EventGeometryType.Point;
                proto.Point = point.Coordinates.ToProto();
                break;
            case EventPolygonGeometry polygon:
                proto.Type = Protos.EventGeometryType.Polygon;
                proto.Polygon.AddRange(polygon.Coordinates.Select(line => line.ToProto()));
                break;
            default:
                throw new Exception($"Unknown type {geometry.Type} encountered");
        }

        return proto;
    }

    public static Protos.Coordinates ToProto(this IPosition position)
    {
        var proto = new Protos.Coordinates
        {
            Latitude = position.Latitude,
            Longitude = position.Longitude
        };

        if (position.Altitude != null) proto.Altitude = position.Altitude.Value;

        return proto;
    }

    public static Protos.PolygonLine ToProto(this LineString line)
    {
        var proto = new Protos.PolygonLine();
        proto.Line.AddRange(line.Coordinates.Select(point => point.ToProto()));
        return proto;
    }

    public static Protos.Category ToProto(this CategoryWithLayers category) {
        var proto = new Protos.Category()
        {
            Id = category.Id,
            Title = category.Title,
            Description = category.Description,
            Url = category.Url,
            LayersUrl = category.LayersUrl,
        };

        proto.Layers.AddRange(category.Layers);

        return proto;
    }

    public static Protos.Layer ToProto(this LayerWithCategories layer)
    {
        var proto = new Protos.Layer()
        {
            Id = layer.Id,
            ServiceUrl = layer.ServiceUrl,
            ServiceTypeId = layer.ServiceTypeId,
        };

        if (layer.Parameters.Count != 0) {
            var parameters = layer.Parameters[0];
            if (parameters.Format != null) proto.Format = parameters.Format;
            if (parameters.TileMatrixSet != null) proto.TileMatrixSet = parameters.TileMatrixSet;
        }

        proto.Categories.AddRange(layer.Categories);

        return proto;
    }

    public static Protos.Source ToProto(this Source source) => new()
    {
        Id = source.Id,
        Title = source.Title,
        SourceUrl = source.SourceUrl,
        EventsUrl = source.EventsUrl,
    };

    public static Protos.Magnitude ToProto(this Magnitude magnitude) => new()
    {
        Id = magnitude.Id,
        Title = magnitude.Title,
        Unit = magnitude.Unit,
        Description = magnitude.Description,
        EventsUrl = magnitude.EventsUrl,
    };
}
