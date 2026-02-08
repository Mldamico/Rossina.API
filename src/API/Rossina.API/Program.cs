using Rossina.API.Extensions;
using Rossina.Common.Application;
using Rossina.Common.Infrastructure;
using Rossina.Modules.Products.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Configuration.AddModuleConfiguration(["catalog"]);
builder.Services.AddApplication([Rossina.Modules.Catalog.Application.AssemblyReference.Assembly]);
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddProductsModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ApplyMigrations();
}

ProductsModule.MapEndpoints(app);


app.Run();