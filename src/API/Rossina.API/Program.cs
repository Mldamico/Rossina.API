using Rossina.API.Extensions;
using Rossina.API.Middleware;
using Rossina.Common.Application;
using Rossina.Common.Infrastructure;
using Rossina.Modules.Products.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Configuration.AddModuleConfiguration(["catalog"]);
builder.Services.AddApplication([Rossina.Modules.Catalog.Application.AssemblyReference.Assembly]);
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!,
    builder.Configuration.GetConnectionString("Cache")!);

builder.Services.AddProductsModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ApplyMigrations();
}

ProductsModule.MapEndpoints(app);

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.Run();