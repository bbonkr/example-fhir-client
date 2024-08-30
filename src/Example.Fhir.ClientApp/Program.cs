// See https://aka.ms/new-console-template for more information
using Example.Fhir.ClientApp.HostedServices;
using Example.Fhir.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddLogging();
builder.Services.AddFhirClient();
builder.Services.AddHostedService<FhirExampleService>();

IHost host = builder.Build();

CancellationTokenSource cancellationTokenSource = new();
var cancellationToken = cancellationTokenSource.Token;

await host.RunAsync(cancellationToken);