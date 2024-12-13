FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

RUN ls -la

COPY ../*.sln ./
COPY ../Cinema.API/*.csproj ./Cinema.API/
COPY ../Cinema.Application/*.csproj ./Cinema.Application/
COPY ../Cinema.Infrastructure/*.csproj ./Cinema.Infrastructure/
COPY ../Cinema.Domain/*.csproj ./Cinema.Domain/

RUN dotnet restore Cinema.API/Cinema.API.csproj

COPY . .

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef --version 8.0.7

ENTRYPOINT dotnet ef database update \
    --project Cinema.Infrastructure/Cinema.Infrastructure.csproj \
    --startup-project Cinema.API/Cinema.API.csproj \
    --connection "$ConnectionStrings__DatabaseConnectionString" \
    --context Cinema.Infrastructure.Core.Data.ApplicationDbContext \
    --configuration Debug
