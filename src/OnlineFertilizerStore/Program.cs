using OnlineFertilizerStore.IoC;
using OnlineFertilizerStore.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .Build();

var settings = OnlineFertilizerStoreSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);

DbContextConfigurator.ConfigureService(builder.Services, settings);
SerilogConfigurator.ConfigureService(builder);
SwaggerConfigurator.ConfigureService(builder.Services);


var app = builder.Build();

DbContextConfigurator.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.Run();

