FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BuildingBlocks.Api/BuildingBlocks.Api.csproj", "BuildingBlocks.Api/"]
RUN dotnet restore "BuildingBlocks.Api/BuildingBlocks.Api.csproj"
COPY . .
WORKDIR "/src/BuildingBlocks.Api"
RUN dotnet build "BuildingBlocks.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BuildingBlocks.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuildingBlocks.Api.dll"]
