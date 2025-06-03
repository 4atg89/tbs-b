FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY ["Auth/Auth.csproj", "Auth/"]
COPY ["JwtLibrary/JwtLibrary.csproj", "JwtLibrary/"]
COPY ["Shared.RabbitMQ/Shared.RabbitMQ.csproj", "Shared.RabbitMQ/"]

RUN dotnet restore "Auth/Auth.csproj"

COPY . .

WORKDIR "/app/Auth"
RUN dotnet build "Auth.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5030
ENTRYPOINT ["dotnet", "Auth.dll"]