using System.Data.Common;
using Dapper;
using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;
using Rossina.Modules.Catalog.Domain.Catalog.Brands;

namespace Rossina.Modules.Catalog.Application.Catalog.Brands.GetBrand;

public sealed record GetBrandQuery(Guid Id) : IQuery<BrandResponse>;


public class GetBrand(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetBrandQuery, BrandResponse>
{
    public async Task<Result<BrandResponse>> Handle(GetBrandQuery query, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = $"""
            SELECT
                id AS {nameof(BrandResponse.Id)},
                name AS {nameof(BrandResponse.Name)},
                logo AS {nameof(BrandResponse.Logo)}
            FROM 
                Products.brands
            WHERE id = @Id AND deleted = 0;
            """;

        BrandResponse? brand = await connection.QuerySingleOrDefaultAsync<BrandResponse?>(sql, query);

        if(brand is null)
        {
            return Result.Failure<BrandResponse>(BrandsErrors.NotFound(query.Id));
        }
        
        return Result.Success(brand);
    }
}
