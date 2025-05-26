using Coffee.Bean.Shop.Common;
using Coffee.Bean.Shop.Core.Entities;
using Coffee.Bean.Shop.Core.Repositories;
using Coffee.Bean.Shop.Core.Services;
using Coffee.Bean.Shop.Logic.Services;

using Moq;

namespace Coffee.Bean.Shop.Logic.Unit.Services;

[TestClass]
public class CoffeeBeanServiceTests
{
    private ICoffeeBeanService? _coffeeBeanService;
    private CoffeeBeanBuilder? _coffeeBeanBuilder;
    private Mock<ICoffeeBeansReadRepository>? _coffeeBeansReadRepositoryMock;
    private Mock<ICoffeeBeansWriteRepository>? _coffeeBeansWriteRepositoryMock;

    [TestInitialize]
    public void Setup()
    {
        _coffeeBeansReadRepositoryMock = new Mock<ICoffeeBeansReadRepository>();
        _coffeeBeansWriteRepositoryMock = new Mock<ICoffeeBeansWriteRepository>();
        _coffeeBeanService = new CoffeeBeanService(
            _coffeeBeansReadRepositoryMock.Object,
            _coffeeBeansWriteRepositoryMock.Object);
        _coffeeBeanBuilder = new CoffeeBeanBuilder();
    }

    [TestMethod]
    [DataRow(new string[] { "Colour" }, new string[] { "" }, new string[] { "Colour is required." })]
    [DataRow(new string[] { "Colour" }, new string[] { "  " }, new string[] { "Colour is required." })]
    [DataRow(new string[] { "Country" }, new string[] { "" }, new string[] { "Country is required." })]
    [DataRow(new string[] { "Country" }, new string[] { "  " }, new string[] { "Country is required." })]
    [DataRow(new string[] { "Description" }, new string[] { "" }, new string[] { "Description is required." })]
    [DataRow(new string[] { "Description" }, new string[] { "  " }, new string[] { "Description is required." })]
    [DataRow(new string[] { "Name" }, new string[] { "" }, new string[] { "Name is required." })]
    [DataRow(new string[] { "Name" }, new string[] { "  " }, new string[] { "Name is required." })]
    [DataRow(new string[] { "Price" }, new string[] { "0" }, new string[] { "Price must be greater than zero." })]
    [DataRow(new string[] { "Price" }, new string[] { "-1" }, new string[] { "Price must be greater than zero." })]
    [DataRow(new string[] { "Colour", "Country" }, new string[] { "", "" }, new string[] { "Colour is required.", "Country is required." })]
    [DataRow(new string[] { "Colour", "Description" }, new string[] { "", "" }, new string[] { "Colour is required.", "Description is required." })]
    [DataRow(new string[] { "Colour", "Name" }, new string[] { "", "" }, new string[] { "Colour is required.", "Name is required." })]
    [DataRow(new string[] { "Colour", "Price" }, new string[] { "", "0" }, new string[] { "Colour is required.", "Price must be greater than zero." })]
    [DataRow(new string[] { "Colour", "Country", "Description" }, new string[] { "", "", "" }, new string[] { "Colour is required.", "Country is required.", "Description is required." })]
    [DataRow(new string[] { "Colour", "Country", "Name" }, new string[] { "", "", "" }, new string[] { "Colour is required.", "Country is required.", "Name is required." })]
    [DataRow(new string[] { "Colour", "Country", "Price" }, new string[] { "", "", "0" }, new string[] { "Colour is required.", "Country is required.", "Price must be greater than zero." })]
    [DataRow(new string[] { "Colour", "Country", "Description", "Name" }, new string[] { "", "", "", "" }, new string[] { "Colour is required.", "Country is required.", "Description is required.", "Name is required." })]
    [DataRow(new string[] { "Colour", "Country", "Description", "Price" }, new string[] { "", "", "", "0" }, new string[] { "Colour is required.", "Country is required.", "Description is required.", "Price must be greater than zero." })]
    [DataRow(new string[] { "Colour", "Country", "Description", "Name", "Price" }, new string[] { "", "", "", "", "0" }, new string[] { "Colour is required.", "Country is required.", "Description is required.", "Name is required.", "Price must be greater than zero." })]
    public async Task CreateCoffeeBeanAsync_ShouldReturnFailure_WhenCoffeeBeanIsInvalid(
        string[] propertyNames,
        string[] invalidValues,
        string[] errorMessages)
    {
        // Arrange
        foreach (var propertyName in propertyNames)
        {
            var invalidValue = invalidValues[Array.IndexOf(propertyNames, propertyName)];

            switch (propertyName)
            {
                case "Colour":
                    _coffeeBeanBuilder = _coffeeBeanBuilder!.WithColour(invalidValue);
                    break;
                case "Country":
                    _coffeeBeanBuilder = _coffeeBeanBuilder!.WithCountry(invalidValue);
                    break;
                case "Description":
                    _coffeeBeanBuilder = _coffeeBeanBuilder!.WithDescription(invalidValue);
                    break;
                case "Name":
                    _coffeeBeanBuilder = _coffeeBeanBuilder!.WithName(invalidValue);
                    break;
                case "Price":
                    _coffeeBeanBuilder = _coffeeBeanBuilder!.WithPrice(decimal.Parse(invalidValue));
                    break;
                default:
                    throw new ArgumentException("Invalid property name", nameof(propertyName));
            }
        }

        var invalidCoffeeBean = _coffeeBeanBuilder!.Build();

        // Act
        var result = await _coffeeBeanService!.CreateCoffeeBeanAsync(invalidCoffeeBean);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsTrue(result.Errors.Any());
        CollectionAssert.AreEqual(errorMessages.ToList(), result.Errors.ToList());
    }

    [TestMethod]
    [DataRow("test")]
    [DataRow("Test")]
    [DataRow("TEST")]
    [DataRow("teST")]
    public async Task CreateCoffeeBeanAsync_ShouldReturnError_WhenCoffeeBeanAlreadyExists(string name)
    {
        // Arrange
        var existingCoffeeBean = _coffeeBeanBuilder!
            .WithName(name)
            .Build();
        _coffeeBeansReadRepositoryMock!.Setup(repo => repo.GetByAsync(existingCoffeeBean.Name))
            .ReturnsAsync(existingCoffeeBean);

        // Act
        var result = await _coffeeBeanService!.CreateCoffeeBeanAsync(existingCoffeeBean);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsTrue(result.Errors.Any());
        Assert.AreEqual(1, result.Errors.Count());
        Assert.AreEqual("Coffee bean with the same name already exists.", result.Errors.First());
        _coffeeBeansReadRepositoryMock.Verify(repo => repo.GetByAsync(It.IsAny<string>()), Times.Once);
        _coffeeBeansReadRepositoryMock.Verify(repo => repo.GetByAsync(existingCoffeeBean.Name), Times.Once);
        _coffeeBeansWriteRepositoryMock!.Verify(repo => repo.CreateAsync(It.IsAny<CoffeeBean>()), Times.Never);
    }

    [TestMethod]
    public async Task CreateCoffeeBeanAsync_ShouldReturnSuccess_WhenCoffeeBeanIsValid()
    {
        // Arrange
        var validCoffeeBean = _coffeeBeanBuilder!.Build();
        _coffeeBeansReadRepositoryMock!.Setup(repo => repo.GetByAsync(validCoffeeBean.Name))
            .ReturnsAsync((CoffeeBean?)null);

        // Act
        var result = await _coffeeBeanService!.CreateCoffeeBeanAsync(validCoffeeBean);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(result.Errors.Any());
        Assert.AreEqual(validCoffeeBean, result.Value);
        _coffeeBeansReadRepositoryMock.Verify(repo => repo.GetByAsync(It.IsAny<string>()), Times.Once);
        _coffeeBeansReadRepositoryMock.Verify(repo => repo.GetByAsync(validCoffeeBean.Name), Times.Once);
        _coffeeBeansWriteRepositoryMock!.Verify(repo => repo.CreateAsync(It.IsAny<CoffeeBean>()), Times.Once);
        _coffeeBeansWriteRepositoryMock!.Verify(repo => repo.CreateAsync(validCoffeeBean), Times.Once);
    }
}