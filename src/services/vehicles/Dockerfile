FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/services/vehicles/ACC.Services.Vehicles.csproj", "src/services/vehicles/"]
COPY ["src/persistence/mongo/ACC.Persistence.Mongo.csproj", "src/persistence/mongo/"]
COPY ["src/common/ACC.Common.csproj", "src/common/"]
COPY ["src/messaging/rabbitMq/ACC.Messaging.RabbitMq.csproj", "src/messaging/rabbitMq/"]
RUN dotnet restore "src/services/vehicles/ACC.Services.Vehicles.csproj"
COPY . .
WORKDIR "/src/src/services/vehicles"
RUN dotnet build "ACC.Services.Vehicles.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ACC.Services.Vehicles.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT Docker
ENTRYPOINT ["dotnet", "ACC.Services.Vehicles.dll","--environment=Docker"]
