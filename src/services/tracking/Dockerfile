FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/services/tracking/ACC.Services.Tracking.csproj", "src/services/tracking/"]
COPY ["src/persistence/mongo/ACC.Persistence.Mongo.csproj", "src/persistence/mongo/"]
COPY ["src/common/ACC.Common.csproj", "src/common/"]
COPY ["src/messaging/rabbitMq/ACC.Messaging.RabbitMq.csproj", "src/messaging/rabbitMq/"]
RUN dotnet restore "src/services/tracking/ACC.Services.Tracking.csproj"
COPY . .
WORKDIR "/src/src/services/tracking"
RUN dotnet build "ACC.Services.Tracking.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ACC.Services.Tracking.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENV ASPNETCORE_ENVIRONMENT Docker
ENTRYPOINT ["dotnet", "ACC.Services.Tracking.dll"]
