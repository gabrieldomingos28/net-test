#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Teste.AM53.API/Teste.AM53.API.csproj", "Teste.AM53.API/"]
COPY ["Teste.AM53.Application/Teste.AM53.Application.csproj", "Teste.AM53.Application/"]
COPY ["Teste.AM53.Domain/Teste.AM53.Domain.csproj", "Teste.AM53.Domain/"]
COPY ["Teste.AM53.CrossCutting/Teste.AM53.CrossCutting.csproj", "Teste.AM53.CrossCutting/"]
COPY ["Teste.AM53.Infrastructure/Teste.AM53.Infrastructure.csproj", "Teste.AM53.Infrastructure/"]
RUN dotnet restore "./Teste.AM53.API/Teste.AM53.API.csproj"
COPY . .
WORKDIR "/src/Teste.AM53.API"
RUN dotnet build "./Teste.AM53.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Teste.AM53.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Teste.AM53.API.dll"]