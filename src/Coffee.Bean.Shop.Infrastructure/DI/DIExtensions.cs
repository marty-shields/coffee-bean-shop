using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Coffee.Bean.Shop.Infrastructure.DI;

public static class DIExtensions
{
    public static void AddCoffeeShopDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CoffeeShopContext>(options => options.UseNpgsql(connectionString));
    }
}