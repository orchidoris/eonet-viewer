using Eonet;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf.Collections;

namespace EonetViewer.Api.Extensions;

public static class EventServiceExtensions
{
    public static EventsQuery FromProto(this Protos.EventsRequest request)
    {
        return new EventsQuery(
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
    }

    public static EventStatusFilter FromProto(this Protos.EventStatus status) => status switch
    {
        Protos.EventStatus.Open => EventStatusFilter.Open,
        Protos.EventStatus.Closed => EventStatusFilter.Closed,
        Protos.EventStatus.All => EventStatusFilter.All,
        _ => throw new ArgumentOutOfRangeException(nameof(status))
    };

    public static Protos.EventsResponse ToProto(this EventsResponse response)
    {
        var result = new Protos.EventsResponse();
        result.Events.AddRange(response.Events.Select(e => e.ToProto()));

        return result;
    }

    public static Protos.Event ToProto(this Event response)
    {
        var result = new Protos.Event {
            Id = response.Id,
            Title = response.Title,
            Description = response.Description,
            Link = response.Link,
            ClosedDate = response.ClosedDate?.UtcDateTime.ToTimestamp(),
            ClosedDateOffsetMinutes = response.ClosedDate?.TotalOffsetMinutes ?? 0,
        };

        result.Categories.AddRange(response.Categories.Select(c => c.ToProto()));
        result.Sources.AddRange(response.Sources.Select(s => s.ToProto()));

        return result;
    }

    public static Protos.EventCategory ToProto(this EventCategory c) => new() { Id = c.Id, Title = c.Title };

    public static Protos.EventSource ToProto(this EventSource s) => new() { Id = s.Id, Url = s.Url };

    public static Protos.EventGeometry ToProto(this EventGeometry g) => new()
    {
        Date = g.Date.ToTimestamp(),
        Type = g.Type,
        Coordinates = new() { Latitude = g.Coordinates.Latitude, Longitude = g.Coordinates.Longitude },
        MagnitudeUnit = g.MagnitudeUnit,
        // MagnitudeValue = g.MagnitudeValue
    };
}
