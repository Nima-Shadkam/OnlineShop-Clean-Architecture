
using API.Installers;
using Application;
using Core;
using Infrastructure.Common;
using Infrastructure.Common.GlobalExceptionHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System.IO;

Serilog.Log.Logger = new LoggerConfiguration()
#if DEBUG
          .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
          .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
          .MinimumLevel.Override("Microsoft.EntityFrameworkCore",
LogEventLevel.Warning)
          .Enrich.FromLogContext()
          .WriteTo.Async(c => c.File("Logs/log-.txt", rollingInterval:
RollingInterval.Day))
#if DEBUG
          .WriteTo.Async(c => c.Console())
#endif
          .CreateLogger();
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    WebRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    Args = args
});
builder.Host.UseSerilog();
builder.Services.AddOptions();
builder.Services.Configure<Configs>(builder.Configuration.GetSection("Configs"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Application - register Application Service
builder.Services.AddApplicaiton();
//builder.Services.AddScoped<IProductService, ProductService>();

#endregion

#region Core - register DbContext
builder.Services.AddCore();
//string connectionString = builder.Configuration.GetConnectionString("SqlConnection");

//builder.Services.AddDbContext<OnlineShopDbContext>(options => {
//    options.UseSqlServer(connectionString);
//});
#endregion


builder.Services.AddInfrastructure();
builder.Services.AddSwagger();
builder.Services.AddCustomCors();
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.EnableAnnotations();  
}
);



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineShop.API v1"));
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("OnlineShopAPI");

app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
