using Refit;

namespace Eonet.IntegrationTests.Extensions;

public static class RefitExtensions
{
    public static string? GetUrl<T>(this IApiResponse<T> apiResponse) =>
        apiResponse.RequestMessage?.RequestUri?.ToString();

    public static string? GetErrorMessage<T>(this IApiResponse<T> apiResponse) =>
        apiResponse.Error?.Message;
}
