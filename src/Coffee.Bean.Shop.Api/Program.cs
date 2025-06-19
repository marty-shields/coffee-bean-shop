using Coffee.Bean.Shop.Api.DI;
using Coffee.Bean.Shop.Api.Endpoints;
using Coffee.Bean.Shop.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.SetupDI();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.ApplyDatabaseMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCoffeeBeanEndpoints();

app.UseHttpsRedirection();
app.Run();

public partial class Program { }