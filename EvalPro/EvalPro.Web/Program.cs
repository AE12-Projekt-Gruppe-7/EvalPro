using EvalPro.Web.AppStart;
using Serilog;
using Serilog.Core;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug().WriteTo.Console().CreateLogger();
var logger = Log.Logger;

logger.Debug("Starting web host");
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

//builder.Services.RegisterSerices(builder.Configuration);

DependencyInjection.RegisterDependencies(builder.Services);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

logger.Debug("Services Registered");

var app = builder.Build();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EvalPro"));
}

app.UseRouting();

app.UseHttpsRedirection();

app.MapControllers();
//app.MapFallbackToFile("index.html");


logger.Debug("Running App");
await app.RunAsync();
