## Prerequisites

* Install Docker

## Application setup

In order to get the application running locally along with seeded database perform these steps:

1. Make sure container named 'tablebooking_api_db' doesn't exist in your docker environment
2. Write those values to 'Solution Items/.env' file:	
    POSTGRES_USER=...
    POSTGRES_PASSWORD=...
    POSTGRES_DB=...
    POSTGRES_VERSION=16.0
3. Execute the 'docker compose up db' command
4. Run the api locally (button)