
# for client
https://react-icons.github.io/react-icons/


# for microservices

https://mongodb-entities.com/wiki/Get-Started.html
https://docs.duendesoftware.com/identityserver/v7/quickstarts/0_overview/
https://microsoft.github.io/reverse-proxy/articles/getting-started.html
https://github.com/whiskpo0/Carsties/tree/main/src

## migration commands
ef is 8.0.3 version, but updated to 9.0.1 at some point.
Creating a migration:
-> Auction folder

- dotnet ef migrations add "InitialCreate" -o Data/Migrations
- dotnet ef database update 
- dotnet ef database drop
- dotnet ef migrations add Outbox  <!-- adding mass transit outbox ( = DLQ ) -->

## docker commands

docker compose up -d
docker compose down
docker volume list
dotnet list package --outdated

## .net commands

dotnet watch
dotnet build

# single docker file
docker build -f src/AuctionService/Dockerfile -t testing123 .
# generate solution
dotnet new webapi -o src/SearchService

# add to main sln
dotnet sln add src/SearchService

# creating shared lib
dotnet new classlib -o src/Contracts

# cd -> AuctionService + SearchService 
dotnet add reference ../../src/Contracts

# RabbitMQ
web ui mngment:
localhost:15672
username: guest
password: guest

# Identity Server
dotnet new install Duende.IdentityServer.Templates

dotnet new isaspid -o src/IdentityService