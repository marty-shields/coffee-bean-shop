using Autofac;
using Autofac.Extensions.DependencyInjection;

using Coffee.Bean.Shop.Infrastructure.DI;
using Coffee.Bean.Shop.Logic.DI;

namespace Coffee.Bean.Shop.Api.DI;

public static class DIExtensions
{
    public static void SetupDI(this WebApplicationBuilder builder)
    {
        // UseAutoFac(builder);
        UseMicrosoft(builder);
    }

    private static void UseAutoFac(WebApplicationBuilder builder)
    {
        builder.Host
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule<LogicModule>();
                container.RegisterModule<InfrastructureModule>();
                container.RegisterModule<ValidatorModule>();
            });
    }

    private static void UseMicrosoft(WebApplicationBuilder builder)
    {
        builder.Services.AddCoffeeShopDbContext(builder.Configuration.GetConnectionString("CoffeeShopContext")!);
        builder.Services.AddCoffeeShopRepositories();
        builder.Services.AddCoffeeBeanShopLogicServices();
        builder.Services.AddValidators();
    }
}
