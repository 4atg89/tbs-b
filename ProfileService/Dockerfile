FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY ["ProfileService/Profile.Domain/Profile.Domain.csproj", "ProfileService/Profile.Domain/"]
COPY ["ProfileService/Profile.Data/Profile.Data.csproj", "ProfileService/Profile.Data/"]
COPY ["ProfileService/Profile.API/Profile.API.csproj", "ProfileService/Profile.API/"]
COPY ["ProfileContracts/ProfileContracts.csproj", "ProfileContracts/"]

RUN dotnet restore "ProfileService/Profile.API/Profile.API.csproj"

COPY . .

WORKDIR "/app/ProfileService/Profile.API"
RUN dotnet build "Profile.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Profile.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

RUN dotnet publish "/app/ProfileContracts/ProfileContracts.csproj" -c $BUILD_CONFIGURATION -o /app/contracts-publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /app/contracts-publish .

ENTRYPOINT ["dotnet", "Profile.API.dll"] 