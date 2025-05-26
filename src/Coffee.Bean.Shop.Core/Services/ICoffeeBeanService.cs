using Coffee.Bean.Shop.Core.Entities;

namespace Coffee.Bean.Shop.Core.Services;

public interface ICoffeeBeanService
{
    Task<Result<CoffeeBean>> CreateCoffeeBeanAsync(CoffeeBean coffeeBean);
}
