using Autofac;

using Coffee.Bean.Shop.Api.Models;

using Coffee.Bean.Shop.Api.Validators;

using FluentValidation;

namespace Coffee.Bean.Shop.Api.DI;

public class ValidatorModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
        .RegisterType<CreateCoffeeBeanRequestValidator>()
        .As<IValidator<CreateCoffeeBeanRequest>>();
    }

}
