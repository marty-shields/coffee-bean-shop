using Coffee.Bean.Shop.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCoffeeShopDbContext(builder.Configuration.GetConnectionString("CoffeeShopContext")!);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.ApplyDatabaseMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();