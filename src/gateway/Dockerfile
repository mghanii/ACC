FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/gateway/ACC.ApiGateway.csproj", "src/gateway/"]
RUN dotnet restore "src/gateway/ACC.ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/gateway"
RUN dotnet build "ACC.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ACC.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENV ASPNETCORE_ENVIRONMENT Docker
ENTRYPOINT ["dotnet", "ACC.ApiGateway.dll","--environment=Docker"]