# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
# This app is containerized and the image is available in my account in hub.docker.com as rvwaran/mvc_inmem_app:latest
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
ENV DOTNET_URLS=http://+:5000
WORKDIR /app



# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sample_MVC.csproj", "."]
RUN dotnet restore "./Sample_MVC.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Sample_MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sample_MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample_MVC.dll"]