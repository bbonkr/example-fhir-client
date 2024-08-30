namespace Example.Fhir.Services.Exceptions;

public class FhirServerConfigurationException : Exception
{
    public FhirServerConfigurationException(string? message) : base(message)
    {
    }

    public FhirServerConfigurationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}