using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eonet.IntegrationTests.Helpers;

internal static class EonetClientHelper
{
    public static IEonetClient GetRealEonetClient() {
        var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        var server = new TestServer(new WebHostBuilder()
            .Configure(app => { })
            .ConfigureServices(services => services.AddEonet(configuration)));

        return server.Services.GetRequiredService<IEonetClient>();
    }
}
