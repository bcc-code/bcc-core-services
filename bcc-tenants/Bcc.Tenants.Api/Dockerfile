FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:5174

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Bcc.Tenants.Api/Bcc.Tenants.Api.csproj", "Bcc.Tenants.Api/"]

RUN dotnet restore "Bcc.Tenants.Api/Bcc.Tenants.Api.csproj"

COPY . .

RUN dotnet build "Bcc.Tenants.Api/Bcc.Tenants.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bcc.Tenants.Api/Bcc.Tenants.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bcc.Tenants.Api.dll"]
