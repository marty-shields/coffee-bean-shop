
using Microsoft.AspNetCore.Mvc.Testing;

namespace Coffee.Bean.Shop.Api.EndToEnd;

[TestClass]
public class TestConfiguration
{
    public static WebApplicationFactory<Program>? Factory { get; private set; }
    public static HttpClient? Client { get; private set; }

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var factory = new WebApplicationFactory<Program>();
        Factory = factory;
        Client = factory.CreateClient();
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup(TestContext context)
    {
        Factory!.Dispose();
    }
}
