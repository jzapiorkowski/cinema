FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY *.sln ./
COPY Cinema.API/*.csproj ./Cinema.API/
COPY Cinema.Application/*.csproj ./Cinema.Application/
COPY Cinema.Infrastructure/*.csproj ./Cinema.Infrastructure/
COPY Cinema.Domain/*.csproj ./Cinema.Domain/

RUN dotnet restore Cinema.API/Cinema.API.csproj

COPY . .

RUN dotnet publish Cinema.API/Cinema.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Cinema.API.dll"]
