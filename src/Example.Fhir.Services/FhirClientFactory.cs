using Example.Fhir.Services.Configurations;
using Example.Fhir.Services.Exceptions;
using Hl7.Fhir.Rest;
using Microsoft.Extensions.Options;

namespace Example.Fhir.Services;

public class FhirClientFactory : IFhirClientFactory
{
    public const string HttpClientName = "fhir-client-httpclient";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly FhirServerConfiguration _fhirServerConfiguration;

    public FhirClientFactory(
        IHttpClientFactory httpClientFactory,
        IOptionsMonitor<FhirServerConfiguration> fhirServerConfigurationAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _fhirServerConfiguration = fhirServerConfigurationAccessor.CurrentValue;
    }

    public FhirClient Create()
    {
        if (_fhirServerConfiguration == null)
        {
            throw new FhirServerConfigurationException("Configuration is invaild");
        }

        if (string.IsNullOrWhiteSpace(_fhirServerConfiguration.BaseUrl))
        {
            throw new FhirServerConfigurationException("Configuration (BaseUrl) is invaild");
        }

        Uri endpoint = new(_fhirServerConfiguration.BaseUrl);
        var httpClient = _httpClientFactory.CreateClient("");

        FhirClientSettings settings = new() { };
        FhirClient client = new(endpoint, httpClient, settings);

        return client;
    }

    public FhirServerConfiguration GetFhirServerConfiguration() => _fhirServerConfiguration;
}