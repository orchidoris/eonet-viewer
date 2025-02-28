using Eonet;
using Google.Protobuf.WellKnownTypes;

namespace EonetViewer.Api.Extensions;

public static class EventServiceExtensions
{
    public static EventsQuery FromProto(this Protos.EventsRequest request) =>
        new EventsQuery(
            Sources: request.Sources,
            Categories: request.Categories,
            Status: request.Status.FromProto(),
            Limit: request.HasLimit ? request.Limit : null,
            Days: request.HasDays ? request.Days : null,
            Start: request.Start.ToOptionalDate(),
            End: request.End.ToOptionalDate(),
            Magnitude: request.Magnitude == null ? null : new MagnitudeFilter(
                Id: request.Magnitude.Id,
                Min: request.Magnitude.Min,
                Max: request.Magnitude.Max
            ),
            BoundingBox: request.BoundingBox == null ? null : new BoundingBox(
                MinLongitude: request.BoundingBox.MinLongitude,
                MinLatitude: request.BoundingBox.MinLatitude,
                MaxLongitude: request.BoundingBox.MaxLongitude,
                MaxLatitude: request.BoundingBox.MaxLatitude
            )
        );

    public static EventStatusFilter FromProto(this Protos.EventStatus status) => status switch
    {
        Protos.EventStatus.Open => EventStatusFilter.Open,
        Protos.EventStatus.Closed => EventStatusFilter.Closed,
        Protos.EventStatus.All => EventStatusFilter.All,
        _ => throw new ArgumentOutOfRangeException(nameof(status))
    };

    public static Protos.EventsResponse ToProto(this EventsResponse response)
    {
        var proto = new Protos.EventsResponse();
        proto.Events.AddRange(response.Events.Select(e => e.ToProto()));

        return proto;
    }

    public static Protos.Event ToProto(this Event response)
    {
        var proto = new Protos.Event();

        proto.Id = response.Id;
        proto.Title = response.Title;
        if (response.Description != null) proto.Description = response.Description;
        proto.Link = response.Link;
        if (response.ClosedDate != null)
        {
            proto.ClosedDate = response.ClosedDate?.UtcDateTime.ToTimestamp();
            proto.ClosedDateOffsetMinutes = response.ClosedDate?.TotalOffsetMinutes ?? 0;
        }

        proto.Categories.AddRange(response.Categories.Select(c => c.ToProto()));
        proto.Sources.AddRange(response.Sources.Select(s => s.ToProto()));
        proto.Geometries.AddRange(response.Geometry.Select(g => g.ToProto()));

        return proto;
    }

    public static Protos.EventCategory ToProto(this EventCategory c) => new()
    {
        Id = c.Id,
        Title = c.Title
    };

    public static Protos.EventSource ToProto(this EventSource s) => new()
    {
        Id = s.Id,
        Url = s.Url
    };

    public static Protos.EventGeometry ToProto(this EventGeometry g) => new()
    {
        Date = g.Date.ToTimestamp(),
        Type = g.Type,
        Coordinates = new() { Latitude = g.Coordinates.Latitude, Longitude = g.Coordinates.Longitude },
        MagnitudeUnit = g.MagnitudeUnit,
        MagnitudeValue = g.MagnitudeValue
    };
}
