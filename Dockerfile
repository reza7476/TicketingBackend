
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/API/Ticketing.Api/Ticketing.Api.csproj src/API/Ticketing.Api/
COPY src/Application/Ticketing.Application/Ticketing.Application.csproj src/Application/Ticketing.Application/
COPY src/Domain/Ticketing.Domain/Ticketing.Domain.csproj src/Domain/Ticketing.Domain/
COPY src/Infrastructure/Ticketing.Infrastructure/Ticketing.Infrastructure.csproj src/Infrastructure/Ticketing.Infrastructure/
COPY src/Shared/Ticketing.Shared/Ticketing.Shared.csproj src/Shared/Ticketing.Shared/

RUN dotnet restore "src/API/Ticketing.Api/Ticketing.Api.csproj"

COPY src/ src/

RUN dotnet publish "src/API/Ticketing.Api/Ticketing.Api.csproj" \
    -c Release \
    -o /app/publish 
   


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Ticketing.Api.dll"]
