using Eonet;
using EonetViewer.Api.Extensions;
using Grpc.Core;

namespace EonetViewer.Api.Services
{
    public class EventsService(ILogger<EventsService> logger, IEonetClient eonetClient) : Protos.EventsService.EventsServiceBase
    {
        public override async Task<Protos.EventsResponse> GetEvents(Protos.EventsRequest request, ServerCallContext context)
        {
            var apiResponse = await eonetClient.GetEvents(request.FromProto());
            return apiResponse.Content?.ToProto() ?? new Protos.EventsResponse() { };
        }
    }
}