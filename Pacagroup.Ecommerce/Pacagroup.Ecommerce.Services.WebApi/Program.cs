using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using Pacagroup.Ecommerce.Application.UseCases;
using Pacagroup.Ecommerce.Infrastructure;
using Pacagroup.Ecommerce.Persistence;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Authentication;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Feature;
using Pacagroup.Ecommerce.Services.WebApi.Modules.HealthCheck;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Injection;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Middleware;
using Pacagroup.Ecommerce.Services.WebApi.Modules.RateLimiter;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Versioning;
using Pacagroup.Ecommerce.Transversal.Logging;
using Serilog;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Web Api RESTful").Centered().Color(Color.Green));

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddTransversalServices(builder.Configuration);

builder.Services.AddFeature(builder.Configuration);
builder.Services.AddInjection(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddVersioning();
builder.Services.AddSwagger();
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddRatelimiting(builder.Configuration);
builder.Host.UseSerilog();

var app = builder.Build();

// configure the Http request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // build a swagger endpoint for each discovered API version
        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });

    app.UseReDoc(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.DocumentTitle = "Pacagroup Technology Services API Market";
            options.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
        }
    });
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseRequestTimeouts();
app.MapControllers();
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.AddMiddleware();

try
{
    Log.Information("Starting Pacagroup.Ecommerce API");
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

public partial class Program { };