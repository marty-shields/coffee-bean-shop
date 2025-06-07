
using Coffee.Bean.Shop.Core;
using Coffee.Bean.Shop.Core.Entities;
using Coffee.Bean.Shop.Core.Repositories;
using Coffee.Bean.Shop.Core.Services;

namespace Coffee.Bean.Shop.Logic.Services;

internal class CoffeeBeanService : ICoffeeBeanService
{
    private readonly ICoffeeBeansReadRepository _coffeeBeansReadRepository;
    private readonly ICoffeeBeansWriteRepository _coffeeBeansWriteRepository;

    public CoffeeBeanService(
        ICoffeeBeansReadRepository coffeeBeansReadRepository,
        ICoffeeBeansWriteRepository coffeeBeansWriteRepository)
    {
        _coffeeBeansReadRepository = coffeeBeansReadRepository;
        _coffeeBeansWriteRepository = coffeeBeansWriteRepository;
    }

    public async Task<Result<CoffeeBean>> CreateCoffeeBeanAsync(CoffeeBean coffeeBean)
    {
        if (!coffeeBean.IsValid())
        {
            return Result<CoffeeBean>.Failure(coffeeBean.GetInvalidDetails());
        }

        var existingBean = await _coffeeBeansReadRepository.GetByAsync(coffeeBean.Name);
        if (existingBean is not null)
        {
            return Result<CoffeeBean>.Failure(new[] { "Coffee bean with the same name already exists." });
        }

        await _coffeeBeansWriteRepository.CreateAsync(coffeeBean);
        return Result<CoffeeBean>.Success(coffeeBean);
    }
}
