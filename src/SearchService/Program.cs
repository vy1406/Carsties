using MongoDB.Driver;
using MongoDB.Entities;
using SearchService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();

await DB.InitAsync("SearchDb", MongoClientSettings
    .FromConnectionString(builder.Configuration.GetConnectionString("MongoDBConnection")));

await DB.Index<Item>()
    .Key(x => x.Make, KeyType.Text)
    .Key(x => x.Model, KeyType.Text)
    .Key(x => x.Color, KeyType.Text)
    .CreateAsync();

app.Run();
