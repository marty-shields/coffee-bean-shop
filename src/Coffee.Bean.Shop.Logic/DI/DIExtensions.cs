using Coffee.Bean.Shop.Core.Services;
using Coffee.Bean.Shop.Logic.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Coffee.Bean.Shop.Logic.DI;

public static class DIExtensions
{
    public static void AddCoffeeBeanShopLogicServices(this IServiceCollection services)
    {
        services.AddScoped<ICoffeeBeanService, CoffeeBeanService>();
    }
}
