namespace Coffee.Bean.Shop.Infrastructure;

using Coffee.Bean.Shop.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;

internal class CoffeeShopContext : DbContext
{
    public CoffeeShopContext(DbContextOptions options) : base(options)
    {
    }

    internal DbSet<CoffeeBean> CoffeeBeans { get; set; }
}