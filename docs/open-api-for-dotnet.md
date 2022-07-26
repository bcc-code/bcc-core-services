1. create csproj with properties:
    ```
    <PropertyGroup>
      <GenerateDocumentationFile>true</GenerateDocumentationFile>
      <NoWarn>$(NoWarn);1591</NoWarn>
    </PropertyGroup>
    ```
1. include [BccCode.BuildingBlocks.Api](https://www.nuget.org/packages/BccCode.BuildingBlocks.Api) Nuget package
1. add ```services.ConfigureBlocks(configuration, environment);``` to your Program.cs (.NET 6) 
2. by adding comments to your endpoints you'll get the decriptions in your Swagger documentation according to Open API specs
