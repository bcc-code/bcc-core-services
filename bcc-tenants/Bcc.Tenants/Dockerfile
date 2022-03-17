FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Bcc.Tenants/Bcc.Tenants.csproj", "Bcc.Tenants/"]

RUN dotnet restore "Bcc.Tenants/Bcc.Tenants.csproj"

COPY . .

RUN dotnet build "Bcc.Tenants/Bcc.Tenants.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bcc.Tenants/Bcc.Tenants.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bcc.Tenants.dll"]
