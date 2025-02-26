using Refit;

namespace Eonet.Tests.Extensions;

public static class ApiResponseExtensions
{
    public static string? GetUriString<T>(this ApiResponse<T> apiResponse) =>
        apiResponse.RequestMessage?.RequestUri?.ToString();
}
