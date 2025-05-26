using Coffee.Bean.Shop.Core.Entities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coffee.Bean.Shop.Common;

public static class CommonAssertions
{
    public static void AssertCoffeeBeanIsEqual(CoffeeBean expectedCoffeeBean, CoffeeBean actualCoffeeBean)
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
