FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/tracking/ACC.Services.Tracking.csproj", "src/Services/tracking/"]
RUN dotnet restore "src/Services/tracking/ACC.Services.Tracking.csproj"
COPY . .
WORKDIR "/src/src/Services/tracking"
RUN dotnet build "ACC.Services.Tracking.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ACC.Services.Tracking.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACC.Services.Tracking.dll"]