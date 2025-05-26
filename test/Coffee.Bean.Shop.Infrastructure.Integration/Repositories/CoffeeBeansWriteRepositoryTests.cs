using Coffee.Bean.Shop.Common;
using Coffee.Bean.Shop.Core.Entities;
using Coffee.Bean.Shop.Infrastructure.Integration.Common;
using Coffee.Bean.Shop.Infrastructure.Models;
using Coffee.Bean.Shop.Infrastructure.Repositories;

namespace Coffee.Bean.Shop.Infrastructure.Integration.Repositories;

[TestClass]
public class CoffeeBeansWriteRepositoryTests
{
    private CoffeeBeansWriteRepository? _repository;
    private CoffeeBeanBuilder? _coffeeBeanBuilder;
    private CoffeeShopContext? _context;

    [TestInitialize]
    public async Task Setup()
    {
        _context = await CoffeeShopContextProvider.CreateContext(nameof(CoffeeBeansWriteRepositoryTests));
        _repository = new CoffeeBeansWriteRepository(_context);
        _coffeeBeanBuilder = new CoffeeBeanBuilder();
    }

    [TestMethod]
    public async Task CreateAsync_ShouldAddCoffeeBean()
    {
        CoffeeBean coffeeBean = _coffeeBeanBuilder!.Build();

        await _repository!.CreateAsync(coffeeBean);

        await AssertCoffeeBeanIsEqual(coffeeBean);
    }

    [TestMethod]
    public async Task CreateAsync_ShouldAddCoffeeBean_NullImage()
    {
        var coffeeBean = _coffeeBeanBuilder!
            .WithImageUrl(null)
            .Build();

        await _repository!.CreateAsync(coffeeBean);

        await AssertCoffeeBeanIsEqual(coffeeBean);
    }

    private async Task AssertCoffeeBeanIsEqual(CoffeeBean expectedCoffeeBean)
    {
        CoffeeBeanTable? actualCoffeeBean = await _context!.CoffeeBeans.FindAsync(expectedCoffeeBean.Id);
        Assert.IsNotNull(actualCoffeeBean);
        CommonDbAssertions.AssertCoffeeBeanIsEqual(expectedCoffeeBean, actualCoffeeBean);
    }
}