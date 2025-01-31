## migration commands

Creating a migration:
-> Auction folder

- dotnet ef migrations add "InitialCreate" -o Data/Migrations
- dotnet ef database update 
- dotnet ef database drop
## docker commands

docker compose up -d
docker compose down
docker volume list

## .net commands
dotnet watch
dotnet build

# generate solution
dotnet new webapi -o src/SearchService

# add to main sln
dotnet sln add src/SearchService

# RabbitMQ
web ui mngment:
localhost:15672
username: guest
password: guest