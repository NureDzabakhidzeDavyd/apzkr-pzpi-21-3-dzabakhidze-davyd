#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kolosok.Presentation/Kolosok.Presentation.csproj", "Kolosok.Presentation/"]
COPY ["Kolosok.Application/Kolosok.Application.csproj", "Kolosok.Application/"]
COPY ["Kolosok.Domain/Kolosok.Domain.csproj", "Kolosok.Domain/"]
COPY ["Kolosok.Infrastructure/Kolosok.Infrastructure.csproj", "Kolosok.Infrastructure/"]
COPY ["Kolosok.Persistence/Kolosok.Persistence.csproj", "Kolosok.Persistence/"]
RUN dotnet restore "./Kolosok.Presentation/./Kolosok.Presentation.csproj"
COPY . .
WORKDIR "/src/Kolosok.Presentation"
RUN dotnet build "./Kolosok.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Kolosok.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ConnectionStrings__KolosokConnectionString="Host=kolosok.db;Port=5432;Username=myuser;Password=mypassword;Database=Kolosok;"
ENTRYPOINT ["dotnet", "Kolosok.Presentation.dll"]