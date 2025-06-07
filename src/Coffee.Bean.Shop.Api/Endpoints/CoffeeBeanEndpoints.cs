using Coffee.Bean.Shop.Api.Models;
using Coffee.Bean.Shop.Core.Entities;
using Coffee.Bean.Shop.Core.Services;

using FluentValidation;

namespace Coffee.Bean.Shop.Api.Endpoints;

public static class CoffeeBeanEndpoints
{
    public static IEndpointRouteBuilder MapCoffeeBeanEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/beans", async (CreateCoffeeBeanRequest request, ICoffeeBeanService coffeeBeanService, IValidator<CreateCoffeeBeanRequest> validator) =>
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage });
                return Results.ValidationProblem(errors.ToDictionary(
                    e => e.PropertyName,
                    e => new[] { e.ErrorMessage }));
            }

            var coffeeBean = CoffeeBean.Create(
                request.Colour!,
                request.Country!,
                request.Description!,
                request.Image,
                false,
                request.Name!,
                request.Price!.Value);

            await coffeeBeanService.CreateCoffeeBeanAsync(coffeeBean);
            return Results.Created($"/api/beans/{coffeeBean.Id}", CreateCoffeeBeanResponse.FromCoffeeBean(coffeeBean));
        })
            .WithOpenApi()
            .WithName("CreateCoffeeBean")
            .WithSummary("Creates a new coffee bean")
            .WithDescription("Creates a new coffee bean with the specified details. The IsBeanOfTheDay property is set to false by default.")
            .WithTags("Coffee Beans");

        return endpoints;
    }
}