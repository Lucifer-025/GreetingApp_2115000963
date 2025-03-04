using NLog;
using NLog.Web;
using NLog.Config;
using BuisnessLayer.Interface;
using BusinessLayer.Service;
using RepositoryLayer.Service;
using RepositoryLayer.Interface;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
LogManager.Configuration = new XmlLoggingConfiguration("C:\\Users\\pc\\Desktop\\C# Programs\\HelloGreetingApplication\\Nlog.Config");
logger.Info("Initializing application...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add NLog to ASP.NET Core
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
