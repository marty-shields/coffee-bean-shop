using System.Net;
using System.Text.Json;

using Coffee.Bean.Shop.Api.EndToEnd.Utilties;
using Coffee.Bean.Shop.Api.Models;

using Microsoft.AspNetCore.Http;

namespace Coffee.Bean.Shop.Api.EndToEnd.Endpoints.CoffeeBeanEndpoints;

[TestClass]
public class PostCoffeeBeanTests
{
    [TestMethod]
    public async Task Success201_AllValidValuesInRequestSupplied()
    {
        CreateCoffeeBeanRequest requestBody = this.GetRequestBody();

        var response = await TestConfiguration.HttpClient!.PostAsync("/api/beans",
            new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json"));

        await this.EnsureSuccessfullResponse(requestBody, response);
    }

    [TestMethod]
    public async Task Success201_NoneRequiredPropertiesNotSet()
    {
        CreateCoffeeBeanRequest requestBody = this.GetRequestBody();
        requestBody.Image = null;

        var response = await TestConfiguration.HttpClient!.PostAsync("/api/beans",
            new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json"));

        await this.EnsureSuccessfullResponse(requestBody, response);
    }

    [TestMethod]
    [DataRow("Colour", null, "'Colour' must not be empty.")]
    [DataRow("Country", null, "'Country' must not be empty.")]
    [DataRow("Description", null, "'Description' must not be empty.")]
    [DataRow("Name", null, "'Name' must not be empty.")]
    [DataRow("Colour", "", "'Colour' must not be empty.")]
    [DataRow("Country", "", "'Country' must not be empty.")]
    [DataRow("Description", "", "'Description' must not be empty.")]
    [DataRow("Name", "", "'Name' must not be empty.")]
    [DataRow("Colour", " ", "'Colour' must not be empty.")]
    [DataRow("Country", " ", "'Country' must not be empty.")]
    [DataRow("Description", " ", "'Description' must not be empty.")]
    [DataRow("Name", " ", "'Name' must not be empty.")]
    [DataRow("Price", "0.00", "'Price' must be greater than '0'.")]
    [DataRow("Price", "-1.00", "'Price' must be greater than '0'.")]
    public async Task Failure400_InvalidPropertiesInRequestSupplied(string propertyName, string? propertyValue, string errorMessage)
    {
        CreateCoffeeBeanRequest requestBody = this.GetRequestBody();
        switch (propertyName)
        {
            case "Colour":
                requestBody.Colour = propertyValue;
                break;
            case "Country":
                requestBody.Country = propertyValue;
                break;
            case "Description":
                requestBody.Description = propertyValue;
                break;
            case "Name":
                requestBody.Name = propertyValue;
                break;
            case "Price":
                if (propertyValue is null)
                    requestBody.Price = null;
                else
                    requestBody.Price = decimal.Parse(propertyValue);
                break;
        }

        var response = await TestConfiguration.HttpClient!.PostAsync("/api/beans",
            new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        HttpValidationProblemDetails validationProblemResponse = JsonSerializer.Deserialize<HttpValidationProblemDetails>(
            await response.Content.ReadAsStringAsync())!;
        Assert.IsNotNull(validationProblemResponse);
        Assert.AreEqual("One or more validation errors occurred.", validationProblemResponse.Title);
        Assert.AreEqual((int)HttpStatusCode.BadRequest, validationProblemResponse.Status);
        Assert.AreEqual("https://tools.ietf.org/html/rfc9110#section-15.5.1", validationProblemResponse.Type);
        Assert.AreEqual(1, validationProblemResponse.Errors.Count);

        string actualKey = validationProblemResponse.Errors.Keys.First();
        Assert.AreEqual(propertyName, actualKey);
        string actualValue = validationProblemResponse.Errors[actualKey].First();
        Assert.AreEqual(errorMessage, actualValue);
    }

    private CreateCoffeeBeanRequest GetRequestBody()
    {
        var requestBody = new CreateCoffeeBeanRequest
        {
            Colour = TestDataGenerator.RandomString(TestDataGenerator.Random.Next(5, 15)),
            Country = TestDataGenerator.RandomString(TestDataGenerator.Random.Next(5, 15)),
            Description = TestDataGenerator.RandomSentence(TestDataGenerator.Random.Next(6, 15)),
            Image = TestDataGenerator.RandomImageUrl(),
            Name = TestDataGenerator.RandomString(TestDataGenerator.Random.Next(8, 20)),
            Price = TestDataGenerator.RandomPrice(),
        };
        return requestBody;
    }

    private async Task EnsureSuccessfullResponse(CreateCoffeeBeanRequest requestBody, HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();
        CreateCoffeeBeanResponse coffeeBean = JsonSerializer.Deserialize<CreateCoffeeBeanResponse>(
            await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        Assert.IsNotNull(coffeeBean);
        Assert.AreEqual(requestBody.Colour, coffeeBean.Colour);
        Assert.AreEqual(requestBody.Country, coffeeBean.Country);
        Assert.AreEqual(requestBody.Description, coffeeBean.Description);
        Assert.AreEqual(requestBody.Image, coffeeBean.Image);
        Assert.AreEqual(requestBody.Name, coffeeBean.Name);
        Assert.AreEqual(requestBody.Price, coffeeBean.Price);
        Assert.IsFalse(coffeeBean.IsBeanOfTheDay);
    }
}
