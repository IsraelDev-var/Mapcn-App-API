# =========================
# BUILD
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar solución y proyecto
COPY Mapcn-app.sln .
COPY TransformadorWebAPI/TransformadorWebAPI.csproj TransformadorWebAPI/

RUN dotnet restore

# Copiar todo
COPY . .

WORKDIR /src/TransformadorWebAPI
RUN dotnet publish -c Release -o /app/publish

# =========================
# RUNTIME
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "TransformadorWebAPI.dll"]
