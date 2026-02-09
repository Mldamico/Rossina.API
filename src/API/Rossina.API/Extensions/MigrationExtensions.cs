using Microsoft.EntityFrameworkCore;
using Rossina.Modules.Products.Infrastructure.Data;
using Rossina.Modules.Products.Infrastructure.Database;

namespace Rossina.API.Extensions;

internal static class MigrationExtensions
{
    internal static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<ProductsDbContext>(scope);
        await SeedData(app.ApplicationServices);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        context.Database.Migrate();
    }

    private static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        
        var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAllAsync();
        }
    }
}