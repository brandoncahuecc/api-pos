#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["api-pos-usuario/api-pos-usuario.csproj", "api-pos-usuario/"]
COPY ["api-pos-biblioteca/api-pos-biblioteca.csproj", "api-pos-biblioteca/"]
RUN dotnet restore "./api-pos-usuario/api-pos-usuario.csproj"
COPY . .
WORKDIR "/src/api-pos-usuario"
RUN dotnet build "./api-pos-usuario.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./api-pos-usuario.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api-pos-usuario.dll"]