FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY ["NotificationService/NotificationService.csproj", "NotificationService/"]
COPY ["Shared.RabbitMQ/Shared.RabbitMQ.csproj", "Shared.RabbitMQ/"]

RUN dotnet restore "NotificationService/NotificationService.csproj"

COPY . .

WORKDIR "/app/NotificationService"
RUN dotnet build "NotificationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "NotificationService.dll"]