using AuctionService;
using AuctionService.Data;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(
    x =>
    {
        x.AddEntityFrameworkOutbox<AuctionDbContext>(o =>
        {
            o.QueryDelay = TimeSpan.FromSeconds(10);

            o.UsePostgres();
            o.UseBusOutbox();
        });
        x.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();
        x.AddConsumersFromNamespaceContaining<AuctionUpdatedFaultConsumer>();
        x.AddConsumersFromNamespaceContaining<AuctionDeletedFaultConsumer>();
        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));


        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
            {
                host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
            });
            cfg.ConfigureEndpoints(context);
        });
    }
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
    });
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddGrpc();

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<GrpcAuctionService>();
try
{
    DbInitializer.InitDb(app);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
app.Run();

public partial class Program { };