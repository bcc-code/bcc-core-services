FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

ARG DEBIAN_FRONTEND=noninteractive
ENV TZ=Europe/Berlin
RUN apt-get install -y tzdata

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build

WORKDIR "/src/bcc-sender-api/"
COPY ["bcc-sender-api.csproj", "./"]
RUN dotnet restore "bcc-sender-api.csproj"

COPY . .
WORKDIR "/src/bcc-sender-api/"
RUN dotnet build "bcc-sender-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bcc-sender-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bcc-sender-api.dll"]