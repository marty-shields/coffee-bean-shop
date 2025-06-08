using Coffee.Bean.Shop.Api.Models;
using Coffee.Bean.Shop.Core.Entities;
using Coffee.Bean.Shop.Core.Errors;
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

            var response = await coffeeBeanService.CreateCoffeeBeanAsync(coffeeBean);

            if (CoffeeBeanAlreadyExists(response))
            {
                return Results.Conflict(new ErrorResponseModel(CoffeeBeanErrors.CoffeeBeanAlreadyExists));
            }

            if (!response.IsSuccess)
            {
                return Results.UnprocessableEntity(new ErrorResponseModel(response.Errors.First()));
            }

            return Results.Created($"/api/beans/{coffeeBean.Id}", response.Value!);
        })
            .WithOpenApi()
            .WithName("CreateCoffeeBean")
            .WithSummary("Creates a new coffee bean")
            .WithDescription("Creates a new coffee bean with the specified details. The IsBeanOfTheDay property is set to false by default.")
            .WithTags("Coffee Beans")
            .Produces(StatusCodes.Status201Created, typeof(CoffeeBean), "application/json")
            .Produces(StatusCodes.Status400BadRequest, typeof(HttpValidationProblemDetails), "application/problem+json")
            .Produces(StatusCodes.Status409Conflict, typeof(ErrorResponseModel), "application/json")
            .Produces(StatusCodes.Status422UnprocessableEntity, typeof(ErrorResponseModel), "application/json");

        return endpoints;
    }

    private static bool CoffeeBeanAlreadyExists(Core.Result<CoffeeBean> response)
        => !response.IsSuccess && response.Errors.Count() == 1 && response.Errors.First() == CoffeeBeanErrors.CoffeeBeanAlreadyExists;
}