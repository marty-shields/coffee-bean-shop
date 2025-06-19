using Autofac;

using Coffee.Bean.Shop.Core.Services;
using Coffee.Bean.Shop.Logic.Services;

namespace Coffee.Bean.Shop.Logic.DI;

public class LogicModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CoffeeBeanService>().As<ICoffeeBeanService>().InstancePerLifetimeScope();
    }

}
