using System.Data.Common;
using Dapper;
using MediatR;
using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.GetProduct;

public sealed record GetProductQuery(Guid Id) : IQuery<ProductResponse?>;

internal sealed class GetProductQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductQuery, ProductResponse?>
{
    public async Task<Result<ProductResponse?>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                id AS {nameof(ProductResponse.Id)},
                title as {nameof(ProductResponse.Title)},
                article as {nameof(ProductResponse.Article)},
                description as {nameof(ProductResponse.Description)},
             FROM Products.products
             WHERE id = @Id";"
             """;

        
        ProductResponse? product = await connection.QuerySingleOrDefaultAsync(sql, request);

        return product;
    }

   
}