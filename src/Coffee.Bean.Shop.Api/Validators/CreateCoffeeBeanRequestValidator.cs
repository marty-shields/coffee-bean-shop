using Coffee.Bean.Shop.Api.Models;

using FluentValidation;

namespace Coffee.Bean.Shop.Api.Validators;

class CreateCoffeeBeanRequestValidator : AbstractValidator<CreateCoffeeBeanRequest>
{
    public CreateCoffeeBeanRequestValidator()
    {
        RuleFor(x => x.Colour).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);
    }
}