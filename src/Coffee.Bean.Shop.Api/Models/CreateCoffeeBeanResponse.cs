using Coffee.Bean.Shop.Core.Entities;

namespace Coffee.Bean.Shop.Api.Models;

class CreateCoffeeBeanResponse
{
    public required string Colour { get; set; }
    public required string Country { get; set; }
    public required string Description { get; set; }
    public string? Image { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required bool IsBeanOfTheDay { get; set; }

    public static CreateCoffeeBeanResponse FromCoffeeBean(CoffeeBean coffeeBean)
    {
        return new CreateCoffeeBeanResponse
        {
            Colour = coffeeBean.Colour,
            Country = coffeeBean.Country,
            Description = coffeeBean.Description,
            Image = coffeeBean.Image,
            Name = coffeeBean.Name,
            Price = coffeeBean.Price,
            IsBeanOfTheDay = coffeeBean.IsBeanOfTheDay
        };
    }
}