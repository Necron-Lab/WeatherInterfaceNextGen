# Siehe https://aka.ms/customizecontainer, um zu erfahren, wie du deinen Debug-Container anpassen und wie Visual Studio dieses Dockerfile verwendet, um Bilder für schnellere Debugging-Zwecke zu erstellen.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Setze die Umgebungsvariable auf Production
ENV ASPNETCORE_ENVIRONMENT="Production"

# Ändere den Port von 5002 auf 5003
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WeatherInterface.csproj", "."]
RUN dotnet restore "./WeatherInterface.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WeatherInterface.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherInterface.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WeatherInterface.dll"]
