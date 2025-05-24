using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Coffee.Bean.Shop.Infrastructure.DI;

public static class DIExtensions
{
    public static void AddCoffeeShopDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CoffeeShopContext>(options => options.UseNpgsql(connectionString));
    }

    public static async Task ApplyMigration(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        using var context = serviceScope.ServiceProvider.GetRequiredService<CoffeeShopContext>();
        await context.Database.MigrateAsync();
    }
}