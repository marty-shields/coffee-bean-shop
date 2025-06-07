using Microsoft.AspNetCore.Mvc.Testing;

namespace Coffee.Bean.Shop.Api.EndToEnd;

[TestClass]
public class TestConfiguration
{
    public static HttpClient? HttpClient { get; private set; }
    public static WebApplicationFactory<Program>? Factory { get; private set; }

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        HttpClient = client;
        Factory = factory;
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup(TestContext context)
    {
        HttpClient!.Dispose();
        Factory!.Dispose();
    }
}
