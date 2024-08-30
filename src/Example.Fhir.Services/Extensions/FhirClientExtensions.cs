using System;
using Example.Fhir.Services.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Example.Fhir.Services.Extensions;

public static class FhirClientExtensions
{
    public static IServiceCollection AddFhirClient(this IServiceCollection services)
    {
        services.AddOptions<FhirServerConfiguration>()
            .Configure<IConfiguration>((options, configuration) =>
            {
                configuration.GetSection(FhirServerConfiguration.Name).Bind(options);
            });

        services.AddHttpClient(FhirClientFactory.HttpClientName, (serviceProvider, httpClient) =>
        {
            var scope = serviceProvider.CreateScope();
            var fhirServerConfigurationAccessor = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<FhirServerConfiguration>>();
            var fhirServerConfiguration = fhirServerConfigurationAccessor.CurrentValue;
            if (!string.IsNullOrWhiteSpace(fhirServerConfiguration?.BaseUrl))
            {
                httpClient.BaseAddress = new Uri(fhirServerConfiguration.BaseUrl);
            }
        }).ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(10),
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
            MaxConnectionsPerServer = 10,
        });

        services.AddTransient<IFhirClientFactory, FhirClientFactory>();

        return services;
    }
}
