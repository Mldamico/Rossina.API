using System.Data.Common;
using Npgsql;
using Rossina.Modules.Catalog.Application.Abstractions.Data;

namespace Rossina.Modules.Products.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}