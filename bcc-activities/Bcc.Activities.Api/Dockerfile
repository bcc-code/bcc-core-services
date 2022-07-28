FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:7238
ENV ASPNETCORE_ENVIRONMENT=Staging

ARG DEBIAN_FRONTEND=noninteractive
ENV TZ=Europe/Berlin
RUN apt-get install -y tzdata

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["/MyShare.Activities/Bcc.Activities.Api/.", "./MyShare.Activities/Bcc.Activities.Api/"]
COPY ["/MyShare.Activities/Bcc.Activities.Tests/", "./MyShare.Activities/Bcc.Activities.Tests/"]

RUN dotnet restore "MyShare.Activities/Bcc.Activities.Api/Bcc.Activities.Api.csproj"

WORKDIR /src

RUN dotnet test "MyShare.Activities/Bcc.Activities.Tests/Bcc.Activities.Tests.csproj"

RUN dotnet build "MyShare.Activities/Bcc.Activities.Api/Bcc.Activities.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyShare.Activities/Bcc.Activities.Api/Bcc.Activities.Api.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app

COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "Bcc.Activities.Api.dll"]
