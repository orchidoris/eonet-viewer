using GeoJSON.Text.Converters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System.Text.Json;

namespace Eonet;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEonet(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EonetClientOptions>(configuration.GetSection(nameof(EonetClientOptions)));
        services.AddRefitClient<IEonetClient>(new()
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new()
                {
                    Converters =
                    {
                        new EventGeometryConverter(),
                        new PositionConverter(),
                        new LineStringEnumerableConverter(),
                    },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true,
                }),
            })
            .ConfigureHttpClient(ConfigureClient);

        return services;
    }

    private static void ConfigureClient(IServiceProvider builder, HttpClient client)
    {
        var options = builder.GetRequiredService<IOptions<EonetClientOptions>>();
        client.BaseAddress = new Uri(options.Value.BaseAddress);
    }
}
