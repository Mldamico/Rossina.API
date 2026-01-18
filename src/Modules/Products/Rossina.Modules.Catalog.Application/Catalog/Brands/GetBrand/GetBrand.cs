using System.Data.Common;
using Dapper;
using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Application.Catalog.Brands.GetBrand;

public sealed record GetBrandQuery(Guid Id) : IQuery<BrandResponse?>;


public class GetBrand(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetBrandQuery, BrandResponse?>
{
    public async Task<Result<BrandResponse?>> Handle(GetBrandQuery query, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = $"""
            SELECT
                id AS {nameof(BrandResponse.Id)},
                name AS {nameof(BrandResponse.Name)},
                logo AS {nameof(BrandResponse.Logo)},
            FROM 
                Products.Brands
            WHERE id = @Id;
            """;

        BrandResponse? brand = await connection.QuerySingleOrDefaultAsync(sql, query);

        return brand;
    }
}
