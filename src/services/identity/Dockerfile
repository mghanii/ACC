FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/services/identity/ACC.Services.Identity.csproj", "src/services/identity/"]
RUN dotnet restore "src/services/identity/ACC.Services.Identity.csproj"
COPY . .
WORKDIR "/src/src/services/identity"
RUN dotnet build "ACC.Services.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ACC.Services.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACC.Services.Identity.dll"]