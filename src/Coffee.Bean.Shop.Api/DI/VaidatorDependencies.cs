using Coffee.Bean.Shop.Api.Models;
using Coffee.Bean.Shop.Api.Validators;

using FluentValidation;

namespace Coffee.Bean.Shop.Api.DI;

public static class VaidatorDependencies
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateCoffeeBeanRequest>, CreateCoffeeBeanRequestValidator>();
        return services;
    }
}
