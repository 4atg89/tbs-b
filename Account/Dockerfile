FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY ["Account/Account.csproj", "Account/"]
COPY ["JwtLibrary/JwtLibrary.csproj", "JwtLibrary/"]

RUN dotnet restore "Account/Account.csproj"

COPY . .

WORKDIR "/app/Account"
RUN dotnet build "Account.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Account.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5030
ENTRYPOINT ["dotnet", "Account.dll"]