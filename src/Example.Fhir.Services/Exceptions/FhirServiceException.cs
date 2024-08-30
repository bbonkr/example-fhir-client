using System;

namespace Example.Fhir.Services.Exceptions;

public class FhirServiceException : Exception
{
    public FhirServiceException(string? message) : base(message)
    {
    }

    public FhirServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
