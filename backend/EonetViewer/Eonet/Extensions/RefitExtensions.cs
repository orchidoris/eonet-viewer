using Refit;
using System.Reflection;

namespace Eonet;

internal static class RefitExtensions
{
    /// <summary>
    /// Map ApiResponse to a different generic parameter in order to pass an error through when multipe endpoints are called internally.
    /// </summary>
    public static ApiResponse<T> ToApiResponse<T>(this IApiResponse apiResponse, T? content = default)
    {
        Type type = apiResponse.GetType();

        if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(ApiResponse<>))
            throw new ArgumentException($"{nameof(apiResponse)} is not of {typeof(ApiResponse<>).FullName} type");

        var httpResponseFieldName = "response";
        FieldInfo? responseFieldInfo = type.GetField(httpResponseFieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new Exception($"Failed to locate a private field {type.Name}.{httpResponseFieldName} with reflection.");

        if (responseFieldInfo.FieldType != typeof(HttpResponseMessage))
            throw new Exception(
                $"A private field {nameof(type.Name)}.{httpResponseFieldName} " +
                $"is not of type {nameof(HttpResponseMessage)}, but {responseFieldInfo.FieldType.FullName} instead.");
        
        var settingsPropInfo = type.GetProperty(nameof(ApiResponse<object>.Settings), BindingFlags.Public | BindingFlags.Instance)
            ?? throw new Exception($"Failed to locate property {type.Name}.{nameof(ApiResponse<object>.Settings)}.");

        return new(
            (HttpResponseMessage)responseFieldInfo.GetValue(apiResponse)!,
            content,
            (RefitSettings)settingsPropInfo.GetValue(apiResponse)!,
            apiResponse.Error);
    }
}
