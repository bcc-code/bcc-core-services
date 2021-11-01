#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Bcc.Registrations.Api/Bcc.Registrations.Api.csproj", "Bcc.Registrations.Api/"]
RUN dotnet restore "Bcc.Registrations.Api/Bcc.Registrations.Api.csproj"
COPY . .
WORKDIR "/src/Bcc.Registrations.Api"
RUN dotnet build "Bcc.Registrations.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bcc.Registrations.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bcc.Registrations.Api.dll"]