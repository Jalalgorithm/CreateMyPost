#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CreateMyPost.WebAPI.Server/CreateMyPost.WebAPI.Server.csproj", "CreateMyPost.WebAPI.Server/"]
COPY ["CreateMyPost.Infrastructure/CreateMyPost.Infrastructure.csproj", "CreateMyPost.Infrastructure/"]
COPY ["CreateMyPost.Application/CreateMyPost.Application.csproj", "CreateMyPost.Application/"]
COPY ["CreateMyPost.Domain/CreateMyPost.Domain.csproj", "CreateMyPost.Domain/"]
RUN dotnet restore "./CreateMyPost.WebAPI.Server/CreateMyPost.WebAPI.Server.csproj"
COPY . .
WORKDIR "/src/CreateMyPost.WebAPI.Server"
RUN dotnet build "./CreateMyPost.WebAPI.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CreateMyPost.WebAPI.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreateMyPost.WebAPI.Server.dll"]