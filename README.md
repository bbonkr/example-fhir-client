# FHIR server usages

- https://www.hl7.org/fhir/
- https://github.com/microsoft/fhir-server
- https://github.com/microsoft/fhir-codegen
  - https://microsoft.github.io/fhir-codegen/
  - https://microsoft.github.io/fhir-codegen/api/index.html
- https://www.nuget.org/packages/Hl7.Fhir.R5

## How to run this project

appsettings.json:

```json
{
  "FhirServer": {
    "BaseUrl": "<Fhir server base uri; https://fhir.app>"
  }
}
```

.env:

```plaintext
FhirServer__BaseUrl=<Fhir server base uri; https://fhir.app>
```

### Visual Studio

1. Open solution into Visual Studio
2. Add appsettings.Development.json into Example.Fhir.ClientApp project
3. Update FhirServer.BaseUrl to your fhir server base url.
4. Hit F5 key.

### Visual Studio Code

1. Open solution into Visual Studio Code
2. Add .env file into Example.Fhir.ClientApp project
3. Update FhirServer\_\_BaseUrl to your fhir server base url.
4. Open `RUN AND DEBUG` pannel on your visual studio code.
5. Click start debugging button. (or HIT F5 key)

## Behavior when application is launched

### Add `Patient` resource

You can find the record added into `Resource` table.

### Query `Patient` resource by id

You can see the identifier of record added into your console.

### Delete `Patient` resource

You can find the record added into `Resource` table.

- Record that is deleted: (Version=2, IsHistory=0, IsDeleted=1)
- Record that is previous: (Version=1, IsHistory=1, IsDelete=0)
