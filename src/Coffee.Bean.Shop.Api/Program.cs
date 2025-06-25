using Coffee.Bean.Shop.Api.DI;
using Coffee.Bean.Shop.Api.Endpoints;
using Coffee.Bean.Shop.Infrastructure.DI;

using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog();
    Log.Logger = new LoggerConfiguration()
        .WriteTo.OpenTelemetry(options =>
        {
            options.Headers.Add("X-Seq-ApiKey", builder.Configuration.GetRequiredSection("LogApiToken").Value!);
            options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.HttpProtobuf;
            options.Endpoint = builder.Configuration.GetRequiredSection("LogUrl").Value!;
            options.ResourceAttributes = new Dictionary<string, object>
            {
                ["service.name"] = "coffee-shop-api"
            };
        })
        .CreateLogger();

    builder.SetupDI();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    WebApplication app = builder.Build();
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        await app.ApplyDatabaseMigrations();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapCoffeeBeanEndpoints();

    app.UseHttpsRedirection();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }