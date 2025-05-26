namespace Coffee.Bean.Shop.Core.Entities;

public record CoffeeBean(
    Guid Id,
    string Colour,
    string Country,
    string Description,
    string? Image,
    bool IsBeanOfTheDay,
    string Name,
    decimal Price)
{
    public static CoffeeBean Create(
        string colour,
        string country,
        string description,
        string? image,
        bool isBeanOfTheDay,
        string name,
        decimal price)
    {
        return new CoffeeBean(Guid.NewGuid(), colour, country, description, image, isBeanOfTheDay, name, price);
    }

    public IEnumerable<string> GetInvalidDetails()
    {
        if (string.IsNullOrWhiteSpace(Colour))
            yield return "Colour is required.";
        if (string.IsNullOrWhiteSpace(Country))
            yield return "Country is required.";
        if (string.IsNullOrWhiteSpace(Description))
            yield return "Description is required.";
        if (string.IsNullOrWhiteSpace(Name))
            yield return "Name is required.";
        if (Price <= 0)
            yield return "Price must be greater than zero.";
    }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Colour) &&
               !string.IsNullOrWhiteSpace(Country) &&
               !string.IsNullOrWhiteSpace(Description) &&
               !string.IsNullOrWhiteSpace(Name) &&
               Price > 0;
    }
}