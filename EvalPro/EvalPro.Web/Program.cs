using System.Text.Json.Serialization;
using EvalPro.Web.AppStart;
using Microsoft.OpenApi;
using Serilog;

const string corsPolicy = "_allowedOrigins";
#pragma warning disable S2221 
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", false,
            true).AddEnvironmentVariables().Build();
    DependencyInjection.RegisterServices(builder.Services);
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
        options.MapType<DateOnly>(() => new OpenApiSchema { Type = JsonSchemaType.String, Format = "date" }));
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(corsPolicy,
            policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
    });
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseCors(corsPolicy);
    }
    else
    {
        app.UseHsts();
        app.UseDefaultFiles();
        app.UseStaticFiles();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.MapControllers();
    app.MapFallbackToFile("index.html");
    await app.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly: {ErrorMessage}", e.Message);
}
finally
{
    await Log.CloseAndFlushAsync();
}