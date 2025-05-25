using Coffee.Bean.Shop.Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Coffee.Bean.Shop.Infrastructure.Models;

[Index(nameof(Name), IsUnique = true)]
internal class CoffeeBeanTable
{
    public Guid Id { get; set; }
    public required string Colour { get; set; }
    public required string Country { get; set; }
    public required string Description { get; set; }
    public string? Image { get; set; }
    public bool IsBeanOfTheDay { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }

    public static CoffeeBeanTable FromEntity(CoffeeBean coffeeBean)
    {
        return new CoffeeBeanTable
        {
            Id = coffeeBean.Id,
            Colour = coffeeBean.Colour,
            Country = coffeeBean.Country,
            Description = coffeeBean.Description,
            Image = coffeeBean.Image,
            IsBeanOfTheDay = coffeeBean.IsBeanOfTheDay,
            Name = coffeeBean.Name,
            Price = coffeeBean.Price
        };
    }

    public CoffeeBean ToEntity()
    {
        return new CoffeeBean
        (
            Id,
            Colour,
            Country,
            Description,
            Image,
            IsBeanOfTheDay,
            Name,
            Price
        );
    }
}