using Microsoft.EntityFrameworkCore;

namespace Coffee.Bean.Shop.Infrastructure.Integration.Common;

internal static class CoffeeShopContextProvider
{
    internal static async Task<CoffeeShopContext> CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<CoffeeShopContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new CoffeeShopContext(options);
        await context.Database.EnsureCreatedAsync();
        await context.Database.EnsureDeletedAsync();

        return context;
    }
}
