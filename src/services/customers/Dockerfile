FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/services/customers/ACC.Services.Customers.csproj", "src/services/customers/"]
RUN dotnet restore "src/services/customers/ACC.Services.Customers.csproj"
COPY . .
WORKDIR "/src/src/services/customers"
RUN dotnet build "ACC.Services.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ACC.Services.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACC.Services.Customers.dll"]