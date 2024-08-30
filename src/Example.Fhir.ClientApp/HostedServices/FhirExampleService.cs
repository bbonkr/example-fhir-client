using System;
using Example.Fhir.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Example.Fhir.ClientApp.HostedServices;

public class FhirExampleService : IHostedLifecycleService
{
    private readonly IFhirClientFactory _fhirClientFactory;
    private readonly ILogger _logger;

    public FhirExampleService(
        IFhirClientFactory fhirClientFactory,
        ILoggerFactory loggerFactory)
    {
        _fhirClientFactory = fhirClientFactory;
        _logger = loggerFactory.CreateLogger<FhirExampleService>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        string message = "Hosted service has requested starting";
        _logger.LogInformation("{Message}", message);
        return Task.CompletedTask;
    }

    public async Task StartedAsync(CancellationToken cancellationToken)
    {
        string message = "Hosted service has started";
        _logger.LogInformation("{Message}", message);

        await AddPatientAsync(cancellationToken);
    }

    public Task StartingAsync(CancellationToken cancellationToken)
    {
        string message = "Hosted service is starting";
        _logger.LogInformation("{Message}", message);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        string message = "Hosted service has requested going down";
        _logger.LogInformation("{Message}", message);
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        string message = "Hosted service has been down";
        _logger.LogInformation("{Message}", message);
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        string message = "Hosted service is going down";
        _logger.LogInformation("{Message}", message);
        return Task.CompletedTask;
    }

    private async Task AddPatientAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Add patient record.");

        using var client = _fhirClientFactory.Create();

        Hl7.Fhir.Model.Patient patient = new()
        {
            Name = new() {
                new (){
                    Family = "Ku",
                    Given = new List<string>(){
                        "PonCheol"
                    },
                    Use = Hl7.Fhir.Model.HumanName.NameUse.Official,
                },
            },
            Gender = Hl7.Fhir.Model.AdministrativeGender.Male,
            BirthDate = "1979-01-20",
        };

        var addedPatient = await client.CreateAsync(patient, cancellationToken);

        // resource record inserted. (Version=1, IsHistory=0)

        _logger.LogInformation("Patient record added. patientId={PatientId} {@Patient}", addedPatient?.Id ?? "NULL", addedPatient);

        if (addedPatient != null)
        {
            var result = await client.SearchByIdAsync(addedPatient.TypeName, addedPatient.Id, ct: cancellationToken);

            _logger.LogInformation("Patient Id={PatientId}", result?.Id ?? "NULL");

            await client.DeleteAsync(addedPatient, ct: cancellationToken);

            // resource record inserted. (Version=2, IsHistory=0, IsDeleted=1)
            // previous record updated (Version=1, IsHistory=1)

            result = await client.SearchByIdAsync(addedPatient.TypeName, addedPatient.Id, ct: cancellationToken);

            _logger.LogInformation("Patient Id={PatientId}", result?.Id ?? "NULL");
        }
    }

}
