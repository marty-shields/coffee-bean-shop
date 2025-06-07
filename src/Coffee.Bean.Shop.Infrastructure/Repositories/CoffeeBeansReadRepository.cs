using Coffee.Bean.Shop.Core.Entities;
using Coffee.Bean.Shop.Core.Repositories;
using Coffee.Bean.Shop.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;

namespace Coffee.Bean.Shop.Infrastructure.Repositories;

internal class CoffeeBeansReadRepository : ICoffeeBeansReadRepository
{
    private readonly CoffeeShopContext _context;

    public CoffeeBeansReadRepository(CoffeeShopContext context)
    {
        _context = context;
    }

    public async Task<CoffeeBean?> GetByAsync(string name)
    {
        CoffeeBeanTable? coffeeBeanTable = await _context.CoffeeBeans
            .FirstOrDefaultAsync(cb => cb.Name.ToLower() == name.ToLower());

        return coffeeBeanTable?.ToEntity();
    }
}
