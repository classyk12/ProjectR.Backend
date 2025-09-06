# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 5000
EXPOSE 5001

USER root
# Install the agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y 'newrelic-dotnet-agent' \
&& rm -rf /var/lib/apt/lists/*

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=77bf83537d0a90af6bac1f9b494fa075FFFFNRAL \
NEW_RELIC_APP_NAME="projectr"

USER $APP_UID
# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./src/ProjectR.Backend.csproj", "."]
COPY ["./src/ProjectR.Backend.Application/ProjectR.Backend.Application.csproj", "ProjectR.Backend.Application/"]
COPY ["./src/ProjectR.Backend.Domain/ProjectR.Backend.Domain.csproj", "ProjectR.Backend.Domain/"]
COPY ["./src/ProjectR.Backend.Shared/ProjectR.Backend.Shared.csproj", "ProjectR.Backend.Shared/"]
COPY ["./src/ProjectR.Backend.Infrastructure/ProjectR.Backend.Infrastructure.csproj", "ProjectR.Backend.Infrastructure/"]
COPY ["./src/ProjectR.Backend.Persistence/ProjectR.Backend.Persistence.csproj", "ProjectR.Backend.Persistence/"]
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