namespace Coffee.Bean.Shop.Api.Models;

class CreateCoffeeBeanRequest
{
    public required string? Colour { get; set; }
    public required string? Country { get; set; }
    public required string? Description { get; set; }
    public string? Image { get; set; }
    public required string? Name { get; set; }
    public decimal? Price { get; set; }
}