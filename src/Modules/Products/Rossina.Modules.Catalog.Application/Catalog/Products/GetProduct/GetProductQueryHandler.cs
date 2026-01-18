using System.Data.Common;
using Dapper;
using MediatR;
using Rossina.Modules.Catalog.Application.Abstractions.Data;
using Rossina.Modules.Catalog.Application.Messaging;
using Rossina.Modules.Catalog.Domain.Abstractions;
using Rossina.Modules.Catalog.Domain.Catalog.Products;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.GetProduct;

public sealed record GetProductQuery(Guid Id) : IQuery<ProductResponse>;

internal sealed class GetProductQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                id AS {nameof(ProductResponse.Id)},
                title as {nameof(ProductResponse.Title)},
                article as {nameof(ProductResponse.Article)},
                description as {nameof(ProductResponse.Description)},
                created_at as {nameof(ProductResponse.CreatedAt)},
                updated_at as {nameof(ProductResponse.UpdatedAt)}
             FROM Products.products
             WHERE id = @Id;
             """;

        
        ProductResponse? product = await connection.QuerySingleOrDefaultAsync<ProductResponse?>(sql, request);

        if (product == null)
        {
            return Result.Failure<ProductResponse>(ProductsError.NotFound(request.Id));
        }

        return Result.Success(product);
    }

   
}