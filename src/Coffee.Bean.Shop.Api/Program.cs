using Coffee.Bean.Shop.Api.DI;
using Coffee.Bean.Shop.Api.Endpoints;
using Coffee.Bean.Shop.Infrastructure.DI;
using Coffee.Bean.Shop.Logic.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCoffeeShopDbContext(builder.Configuration.GetConnectionString("CoffeeShopContext")!);
builder.Services.AddCoffeeShopRepositories();
builder.Services.AddCoffeeBeanShopLogicServices();
builder.Services.AddValidators();

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