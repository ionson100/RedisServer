#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RedisTr/RedisTr.csproj", "RedisTr/"]
RUN dotnet restore "RedisTr/RedisTr.csproj"
COPY . .
WORKDIR "/src/RedisTr"
RUN dotnet build "RedisTr.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedisTr.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisTr.dll"]