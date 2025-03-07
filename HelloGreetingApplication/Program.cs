using NLog;
using NLog.Web;
using NLog.Config;
using BuisnessLayer.Interface;
using BusinessLayer.Service;
using RepositoryLayer.Service;
using RepositoryLayer.Interface;
using RepositoryLayer.Context;
using Microsoft.EntityFrameworkCore;
using Middleware.ExceptionMiddleware; ;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
LogManager.Configuration = new XmlLoggingConfiguration("C:\\Users\\pc\\Desktop\\CloneAppGreet\\GreetingApp_2115000963\\Nlog.Config");
logger.Info("Initializing application...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add NLog to ASP.NET Core
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddDbContext<GreetContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlDatabase"), new MySqlServerVersion(new Version(8, 0, 41))));

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();


    var app = builder.Build();

  
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRequestLoggerMiddleware();
        app.ConfigureExceptionMiddleware();


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
