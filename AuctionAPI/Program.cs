using AuctionAPI.Services;
using NLog;
using NLog.Web;
using sidecar_lib;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.FeatureManagement;
using VaultSharp;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;


var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
   AuthSidecar sidecar = new(logger);

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddFeatureManagement();

    //builder.Services.AddSingleton<IVaultClient>(sidecar.vaultClient);
    builder.Services.AddScoped<IAuctionService, AuctionService>();
    
    //builder.Services.AddScoped<IAuctionRepo, AuctionRepoMongo>();
    builder.Services.AddTransient<AuctionRepoMongo>();
    builder.Services.AddTransient<AuctionRepoExternal>();
    builder.Services.AddTransient<ServiceResolver>(serviceProvider => key =>
    {
        switch (key)
        {
            case "Mongo":
                return serviceProvider.GetService<AuctionRepoMongo>()!;
            case "External":
                return serviceProvider.GetService<AuctionRepoExternal>()!;
            default:
                throw new KeyNotFoundException(); // or maybe return null, up to you
        }
    });


    builder.Services.AddScoped<IInfraRepo, InfraRepoRender>();
    
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add OpenTelemetry services
builder.Services.AddOpenTelemetry()
    .WithMetrics(builder =>
    {
        builder.AddPrometheusExporter();
        
        //builder.AddMeter(Instrumentation.MeterName);

        builder.AddMeter("Microsoft.AspNetCore.Hosting","Microsoft.AspNetCore.Server.Kestrel");
        
        builder.AddView("http.server.request.duration",
            new ExplicitBucketHistogramConfiguration
            {
                Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05,
                       0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
            });       

    });
    

    
    builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = sidecar.GetTokenValidationParameters();
    });
    

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureSwagger("AuctionAPI");

    var app = builder.Build();


        app.UseSwagger();
        app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("./v1/swagger.json", "Your Microservice API V1");
    });

    app.UseHttpsRedirection();

    app.UseAuthorization();
    
    app.MapPrometheusScrapingEndpoint();

    app.MapControllers();

    app.MapGet("/telemetry", () => "OpenTelemetry! ticks:" + DateTime.Now.Ticks.ToString()[^5..]);

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
