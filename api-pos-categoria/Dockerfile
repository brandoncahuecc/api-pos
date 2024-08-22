#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["api-pos-categoria/api-pos-categoria.csproj", "api-pos-categoria/"]
COPY ["api-pos-biblioteca/api-pos-biblioteca.csproj", "api-pos-biblioteca/"]
RUN dotnet restore "./api-pos-categoria/api-pos-categoria.csproj"
COPY . .
WORKDIR "/src/api-pos-categoria"
RUN dotnet build "./api-pos-categoria.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./api-pos-categoria.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api-pos-categoria.dll"]