FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

ARG DEBIAN_FRONTEND=noninteractive
ENV TZ=Europe/Berlin
RUN apt-get install -y tzdata

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build
WORKDIR /src

COPY ["api/bcc-sender-api/", "./api/bcc-sender-api/"]
RUN dotnet restore "api/bcc-sender-api/bcc-sender-api.csproj"

WORKDIR /src
RUN dotnet build "api/bcc-sender-api/bcc-sender-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api/bcc-sender-api/bcc-sender-api.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Budget.Api.dll"]