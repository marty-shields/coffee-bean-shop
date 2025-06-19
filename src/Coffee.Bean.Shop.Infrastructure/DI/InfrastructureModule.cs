using System;

using Autofac;

using Coffee.Bean.Shop.Core.Repositories;

using Coffee.Bean.Shop.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Coffee.Bean.Shop.Infrastructure.DI;

public class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<CoffeeBeansReadRepository>()
            .As<ICoffeeBeansReadRepository>()
            .InstancePerLifetimeScope();
        builder
            .RegisterType<CoffeeBeansWriteRepository>()
            .As<ICoffeeBeansWriteRepository>()
            .InstancePerLifetimeScope();

        builder.Register(x =>
        {
            var config = x.Resolve<IConfiguration>();
            var opt = new DbContextOptionsBuilder<CoffeeShopContext>();
            opt.UseNpgsql(config.GetConnectionString("CoffeeShopContext"));
            return new CoffeeShopContext(opt.Options);
        })
        .InstancePerLifetimeScope();
    }
}
