//Database Tracked Id Provider

using EvalPro.Web.AppStart;
using Serilog;
using Serilog.Core;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug().WriteTo.Console().CreateLogger();
var logger = Log.Logger;

logger.Debug("Starting web host");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

logger.Debug("Services Registered");
DependencyInjection.RegisterDependencies(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

logger.Debug("Running App");
app.Run();
