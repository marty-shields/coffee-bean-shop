using Coffee.Bean.Shop.Core.Entities;

namespace Coffee.Bean.Shop.Core.Repositories;

public interface ICoffeeBeansReadRepository
{
    Task<CoffeeBean?> GetByAsync(string name);
}
