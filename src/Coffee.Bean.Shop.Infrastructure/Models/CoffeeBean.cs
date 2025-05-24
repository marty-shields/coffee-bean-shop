namespace Coffee.Bean.Shop.Infrastructure.Models;

internal class CoffeeBean
{
    public Guid Id { get; set; }
    public required string Colour { get; set; }
    public required string Country { get; set; }
    public required string Description { get; set; }
    public string? Image { get; set; }
    public bool IsBeanOfTheDay { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}