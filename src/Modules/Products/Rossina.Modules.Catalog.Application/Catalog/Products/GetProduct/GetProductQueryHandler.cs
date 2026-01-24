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
                p.id AS {nameof(ProductVariantRow.Id)},
                p.title as {nameof(ProductVariantRow.Title)},
                p.article as {nameof(ProductVariantRow.Article)},
                p.description as {nameof(ProductVariantRow.Description)},
                p.created_at as {nameof(ProductVariantRow.CreatedAt)},
                p.updated_at as {nameof(ProductVariantRow.UpdatedAt)},
                v.size as {nameof(ProductVariantRow.Size)},
                v.color as {nameof(ProductVariantRow.Color)},
                v.price as {nameof(ProductVariantRow.Price)},
                v.stock as {nameof(ProductVariantRow.Stock)}
             FROM products.products p
             INNER JOIN products.product_variants v ON p.id = v.product_id
             WHERE p.id = @Id AND p.deleted = false AND v.deleted = false
             """;

        
        var productResponse = (await connection.QueryAsync<ProductVariantRow>(sql, request)).ToList();

        if (productResponse.Count == 0)
        {
            return Result.Failure<ProductResponse>(ProductsError.NotFound(request.Id));
        }
        
        var product = productResponse
            .GroupBy(r => new
            {
                r.Id,
                r.Title,
                r.Article,
                r.Description,
                r.CreatedAt,
                r.UpdatedAt
            })
            .Select(g => new ProductResponse(
                g.Key.Id,
                g.Key.Title,
                g.Key.Article,
                g.Key.Description,
                g.Key.CreatedAt,
                g.Key.UpdatedAt,
                g.Select(v => new VariantResponse(
                    v.Size,
                    v.Color,
                    v.Price,
                    v.Stock
                )).ToArray()
            ))
            .Single();

        return Result.Success(product);
    }

   
}