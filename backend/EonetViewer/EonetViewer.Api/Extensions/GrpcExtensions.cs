using Google.Protobuf.WellKnownTypes;

namespace EonetViewer.Api.Extensions;

public static class GrpcExtensions
{
    public static DateOnly? ToOptionalDate(this Timestamp timestamp) =>
        timestamp == null ? null : DateOnly.FromDateTime(timestamp.ToDateTime());
}
