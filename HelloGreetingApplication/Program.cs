using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Config;
using NLog.Web;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using Middleware.ExceptionMiddleware;
using RepositoryLayer.Service;
using HelloGreetingApplication.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



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
    builder.Services.AddDbContext<UserContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlDatabase"), new MySqlServerVersion(new Version(8, 0, 41))));
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))

        };

    });

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();
    builder.Services.AddScoped<IUserBL, UserBL>();
    builder.Services.AddScoped<IUserRL, UserRL>();
    builder.Services.AddScoped<JwtTokenHelper>();
    var app = builder.Build();



    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseRequestLoggerMiddleware();
    app.ConfigureExceptionMiddleware();

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
