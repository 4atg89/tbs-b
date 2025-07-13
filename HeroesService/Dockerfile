FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY ["HeroesService/HeroesService.csproj", "HeroesService/"]
COPY ["ProfileContracts/ProfileContracts.csproj", "ProfileContracts/"]

RUN dotnet restore "HeroesService/HeroesService.csproj"

COPY . .

WORKDIR "/app/HeroesService"
RUN dotnet build "HeroesService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "HeroesService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "HeroesService.dll"]