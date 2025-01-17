# Table Booking API
Client for this api can be found at different repository at this URL: https://github.com/TeriyakiGod/tablebooking_flutter
## Prerequisites

* Install Docker
* Install.NET 8.0

## Application setup

In order to get the application running locally along with seeded database perform these steps:

1. Make sure container named 'tablebooking_api_db' doesn't exist in your docker environment
2. Write those secret values received from Pawel Frankowski to 'Solution Items/.env' file:	
    POSTGRES_USER=...
    POSTGRES_PASSWORD=...
    POSTGRES_DB=...
    POSTGRES_VERSION=16.0
3. Execute command on solution folder: 
```bash
docker compose up db
```
4. Run the api locally (button)

## Migrations

Add migration:
```bash
dotnet ef migrations add {MigrationName} --project "{PATH_TO_PROJECT}\TableBooking\TableBooking.Model\TableBooking.Model.csproj" --startup-project "{PATH_TO_PROJECT}\TableBooking\TableBooking.Api\TableBooking.Api.csproj" 
```

```bash
dotnet ef database update --project "{PATH_TO_PROJECT}\TableBooking\TableBooking.Model\TableBooking.Model.csproj" --startup-project "{PATH_TO_PROJECT}\TableBooking\TableBooking.Api\TableBooking.Api.csproj"
```

## Migrations deployment
Apply migrations to server by generating SQL script
```bash
dotnet ef migrations script --project "{PATH_TO_PROJECT}\TableBooking\TableBooking.Model\TableBooking.Model.csproj" --startup-project "{PATH_TO_PROJECT}\TableBooking\TableBooking.Api\TableBooking.Api.csproj" 
```