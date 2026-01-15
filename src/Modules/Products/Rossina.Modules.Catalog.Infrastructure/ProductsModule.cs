using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Rossina.Modules.Catalog.Application;
using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Domain.Catalog.Products;
using Rossina.Modules.Products.Infrastructure.Data;
using Rossina.Modules.Products.Infrastructure.Database;
using Rossina.Modules.Products.Infrastructure.Repository;
using Rossina.Modules.Products.Presentation.Catalog.Products;

namespace Rossina.Modules.Products.Infrastructure;

public static class ProductsModule
{
    public static IServiceCollection AddProductsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => { config.RegisterServicesFromAssemblies(AssemblyReference.Assembly); });

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
        
        services.AddInfrastructure(configuration);

        return services;
    }

    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        ProductEndpoints.MapEndpoints(app);
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConnectionString = configuration.GetConnectionString("Database")!;

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.AddDbContext<ProductsDbContext>(options =>
        {
            options.UseNpgsql(databaseConnectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                        Schemas.Products))
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ProductsDbContext>());
    }
}