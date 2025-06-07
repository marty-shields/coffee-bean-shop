using Coffee.Bean.Shop.Core.Entities;

using Coffee.Bean.Shop.Core.Repositories;
using Coffee.Bean.Shop.Infrastructure.Models;

namespace Coffee.Bean.Shop.Infrastructure.Repositories;

internal class CoffeeBeansWriteRepository : ICoffeeBeansWriteRepository
{
    private readonly CoffeeShopContext _dbContext;


    public CoffeeBeansWriteRepository(CoffeeShopContext dbContext)
    {
        _dbContext = dbContext;

    }

    public async Task CreateAsync(CoffeeBean coffeeBean)
    {
        var coffeeBeanTable = CoffeeBeanTable.FromEntity(coffeeBean);
        await _dbContext.CoffeeBeans.AddAsync(coffeeBeanTable);
        await _dbContext.SaveChangesAsync();
    }
}
