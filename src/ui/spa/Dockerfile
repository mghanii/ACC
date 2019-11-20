FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS builder
WORKDIR /source

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

COPY *.csproj .
RUN dotnet restore

COPY ./ ./

RUN dotnet publish "./ACC.UI.SPA.csproj" --output "./dist" --configuration Release --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim
WORKDIR /app
COPY --from=builder /source/dist .
EXPOSE 80
ENTRYPOINT ["dotnet", "ACC.UI.SPA.dll"]
