# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProjectR.Backend.csproj", "."]
COPY ["ProjectR.Backend.Application/ProjectR.Backend.Application.csproj", "ProjectR.Backend.Application/"]
COPY ["ProjectR.Backend.Domain/ProjectR.Backend.Domain.csproj", "ProjectR.Backend.Domain/"]
COPY ["ProjectR.Backend.Shared/ProjectR.Backend.Shared.csproj", "ProjectR.Backend.Shared/"]
COPY ["ProjectR.Backend.Infrastructure/ProjectR.Backend.Infrastructure.csproj", "ProjectR.Backend.Infrastructure/"]
COPY ["ProjectR.Backend.Persistence/ProjectR.Backend.Persistence.csproj", "ProjectR.Backend.Persistence/"]
RUN dotnet restore "./ProjectR.Backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ProjectR.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProjectR.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectR.Backend.dll"]