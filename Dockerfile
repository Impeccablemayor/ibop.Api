# ===============================
# Base runtime image
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Data Protection keys directory
RUN mkdir /keys
VOLUME ["/keys"]

EXPOSE 8080

# ===============================
# Build stage
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ibop.Api/ibop.Api.csproj", "ibop.Api/"]
RUN dotnet restore "ibop.Api/ibop.Api.csproj"

COPY . .
WORKDIR "/src/ibop.Api"
RUN dotnet build "ibop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ===============================
# Publish stage
# ===============================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ibop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ===============================
# Final runtime image
# ===============================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ibop.Api.dll"]
