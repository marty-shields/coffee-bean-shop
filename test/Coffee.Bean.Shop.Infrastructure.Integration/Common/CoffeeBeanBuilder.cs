using Coffee.Bean.Shop.Core.Entities;

namespace Coffee.Bean.Shop.Infrastructure.Integration.Common;

public class CoffeeBeanBuilder
{
    private readonly string _colour = "Light";
    private readonly string _country = "Ethiopia";
    private readonly string _description = "Fruity and floral notes";
    private string? _imageUrl = "image_url";
    private bool _isBeanOfTheDay = false;
    private string _name = "Ethiopian Light Roast";
    private readonly decimal _price = 10.99m;
    private readonly Guid _id = Guid.NewGuid();

    public CoffeeBean Build()
    {
        return new CoffeeBean(
            _id,
            _colour,
            _country,
            _description,
            _imageUrl,
            _isBeanOfTheDay,
            _name,
            _price);
    }

    public CoffeeBeanBuilder WithImageUrl(string? image)
    {
        _imageUrl = image;
        return this;
    }

    public CoffeeBeanBuilder WithIsBeanOfTheDay(bool IsBeanOfTheDay)
    {
        _isBeanOfTheDay = IsBeanOfTheDay;
        return this;
    }

    public CoffeeBeanBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
}