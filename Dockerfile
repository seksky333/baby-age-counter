#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443
EXPOSE 8080

RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["babyagecounter.client/nuget.config", "babyagecounter.client/"]
COPY ["BabyAgeCounter.Server/BabyAgeCounter.Server.csproj", "BabyAgeCounter.Server/"]
COPY ["babyagecounter.client/babyagecounter.client.esproj", "babyagecounter.client/"]
RUN dotnet restore "./BabyAgeCounter.Server/./BabyAgeCounter.Server.csproj"
COPY . .
WORKDIR "/src/BabyAgeCounter.Server"
RUN dotnet build "./BabyAgeCounter.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
#workaround to address Could not create certificate error
RUN mkdir -p /root/.aspnet/https/
RUN dotnet publish "./BabyAgeCounter.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BabyAgeCounter.Server.dll"]