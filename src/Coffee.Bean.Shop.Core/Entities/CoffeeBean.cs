namespace Coffee.Bean.Shop.Core.Entities;

public record CoffeeBean(
    Guid Id,
    string Colour,
    string Country,
    string Description,
    string? Image,
    bool IsBeanOfTheDay,
    string Name,
    decimal Price);