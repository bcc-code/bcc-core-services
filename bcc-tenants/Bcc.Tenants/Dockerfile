FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["bcc-tenants/Bcc.Tenants/Bcc.Tenants.csproj", "Bcc.Tenants/"]
RUN dotnet restore "Bcc.Tenants/Bcc.Tenants.csproj"

COPY . .
WORKDIR "/src/Bcc.Tenants"
RUN dotnet build "Bcc.Tenants.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bcc.Tenants.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bcc.Tenants.dll"]
