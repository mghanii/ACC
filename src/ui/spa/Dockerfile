FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get install --yes curl
RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/ui/spa/ACC.UI.SPA.csproj", "src/ui/spa/"]
RUN dotnet restore "src/ui/spa/ACC.UI.SPA.csproj"
COPY . .
WORKDIR "/src/src/ui/spa"
RUN dotnet build "ACC.UI.SPA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ACC.UI.SPA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACC.UI.SPA.dll"]