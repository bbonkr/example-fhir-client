using Example.Fhir.Services.Configurations;
using Hl7.Fhir.Rest;

namespace Example.Fhir.Services;

public interface IFhirClientFactory
{
    FhirClient Create();

    FhirServerConfiguration GetFhirServerConfiguration();
}
