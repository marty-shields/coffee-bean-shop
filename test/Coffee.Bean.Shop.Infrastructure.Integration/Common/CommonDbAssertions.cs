using Coffee.Bean.Shop.Core.Entities;
using Coffee.Bean.Shop.Infrastructure.Models;

namespace Coffee.Bean.Shop.Infrastructure.Integration.Common;

internal static class CommonDbAssertions
{
    internal static void AssertCoffeeBeanIsEqual(CoffeeBean expectedCoffeeBean, CoffeeBeanTable actualCoffeeBean)
    {
        Assert.AreEqual(expectedCoffeeBean.Name, actualCoffeeBean.Name);
        Assert.AreEqual(expectedCoffeeBean.Price, actualCoffeeBean.Price);
        Assert.AreEqual(expectedCoffeeBean.Country, actualCoffeeBean.Country);
        Assert.AreEqual(expectedCoffeeBean.Colour, actualCoffeeBean.Colour);
        Assert.AreEqual(expectedCoffeeBean.Description, actualCoffeeBean.Description);
        Assert.AreEqual(expectedCoffeeBean.Image, actualCoffeeBean.Image);
        Assert.AreEqual(expectedCoffeeBean.IsBeanOfTheDay, actualCoffeeBean.IsBeanOfTheDay);
        Assert.AreEqual(expectedCoffeeBean.Id, actualCoffeeBean.Id);
    }
}
