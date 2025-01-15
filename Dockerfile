# Fase base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Fase di build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestRMQ.csproj", "."]
RUN dotnet restore "./TestRMQ.csproj"
COPY . .
RUN dotnet build "./TestRMQ.csproj" -c Release -o /app/build

# Fase di pubblicazione
FROM build AS publish
RUN dotnet publish "./TestRMQ.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Fase finale (esecuzione)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["appsettings.json", "appsettings.Development.json", "./"]

# Configurazione dell'ambiente per Swagger
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_STARTUPURLS=http://+:80/swagger

ENTRYPOINT ["dotnet", "TestRMQ.dll"]