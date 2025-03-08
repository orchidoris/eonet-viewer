using Moq;
using Moq.Language;
using Refit;
using System.Net;

namespace Eonet.Tests.Extensions;

public static class RefitExtensions
{
    public static void ReturnsApiResponse<T, TContent>(
        this IReturns<T, Task<IApiResponse<TContent>>> returns,
        TContent? content = default,
        HttpStatusCode status = HttpStatusCode.Accepted
    ) where T : class =>
        returns.ReturnsAsync(new ApiResponse<TContent>(new HttpResponseMessage(status), content, new RefitSettings()));
}
