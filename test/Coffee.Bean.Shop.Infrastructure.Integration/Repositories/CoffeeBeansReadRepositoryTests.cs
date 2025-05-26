using Coffee.Bean.Shop.Common;
using Coffee.Bean.Shop.Core.Entities;
using Coffee.Bean.Shop.Core.Repositories;
using Coffee.Bean.Shop.Infrastructure.Integration.Common;
using Coffee.Bean.Shop.Infrastructure.Models;
using Coffee.Bean.Shop.Infrastructure.Repositories;

namespace Coffee.Bean.Shop.Infrastructure.Integration.Repositories;

[TestClass]
public class CoffeeBeansReadRepositoryTests
{
    private CoffeeShopContext? _context;
    private ICoffeeBeansReadRepository? _repository;
    private CoffeeBeanBuilder? _coffeeBeanBuilder;

    [TestInitialize]
    public async Task Setup()
    {
        _context = await CoffeeShopContextProvider.CreateContext(nameof(CoffeeBeansReadRepositoryTests));
        _repository = new CoffeeBeansReadRepository(_context);
        _coffeeBeanBuilder = new CoffeeBeanBuilder();
    }

    [TestMethod]
    [DataRow("Arabica")]
    [DataRow("arabica")]
    [DataRow("ARIBICA")]
    [DataRow("aRaBiCa")]
    public async Task GetByAsync_ReturnsCoffeeBean_WhenExists(string name)
    {
        CoffeeBean expetedCoffeeBean = await CreateCoffeeBeanInDb(name.ToLower());

        CoffeeBean? actualCoffeeBean = await _repository!.GetByAsync(name);

        // Assert
        Assert.IsNotNull(actualCoffeeBean);
        Assert.AreEqual(expetedCoffeeBean, actualCoffeeBean);
    }

    [TestMethod]
    public async Task GetByAsync_ReturnsNull_WhenDoesNotExist()
    {
        await CreateCoffeeBeanInDb("Arabica");

        // Act
        var result = await _repository!.GetByAsync("Robusta");

        // Assert
        Assert.IsNull(result);
    }

    private async Task<CoffeeBean> CreateCoffeeBeanInDb(string name)
    {
        var expetedCoffeeBean = _coffeeBeanBuilder!
            .WithName(name)
            .Build();

        _context!.CoffeeBeans.Add(CoffeeBeanTable.FromEntity(expetedCoffeeBean));
        await _context.SaveChangesAsync();
        return expetedCoffeeBean;
    }
}
