using Eonet;
using EonetViewer.Api.Extensions;
using Grpc.Core;

namespace EonetViewer.Api.Services;

public class EventsService(ILogger<EventsService> Logger, IEonetClient EonetClient) : Protos.EventsService.EventsServiceBase
{
    public override async Task<Protos.EventsResponse> GetEvents(Protos.EventsRequest request, ServerCallContext context)
    {
        var apiResponse = await EonetClient.GetEvents(request.FromProto());
        return apiResponse.ToProto();
    }

    public override async Task<Protos.EventsContextResponse> GetEventsContext(Protos.EventsContextRequest request, ServerCallContext context)
    {
        var apiResponse = await EonetClient.GetContext();
        return apiResponse.ToProto();
    }
}