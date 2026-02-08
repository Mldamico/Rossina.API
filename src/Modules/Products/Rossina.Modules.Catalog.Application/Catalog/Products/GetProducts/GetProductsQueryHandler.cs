using System.Data.Common;
using Dapper;
using Rossina.Common.Application.Data;
using Rossina.Common.Application.Messaging;
using Rossina.Common.Domain.Abstractions;
using Rossina.Modules.Catalog.Application.Catalog.Products.GetProduct;

namespace Rossina.Modules.Catalog.Application.Catalog.Products.GetProducts;

internal sealed class GetProductsQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    :IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<Result<GetProductsResponse>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync();

        var parameters = new GetProductsParameters(
            query.Title,
            query.Article,
            query.Brand,
            query.PageSize,
            (query.Page - 1) * query.PageSize);
        
        var products = await GetProductsAsync(dbConnection, parameters);
        
        int totalCount = await CountProductsAsync(dbConnection, parameters);
        
        return Result.Success(new GetProductsResponse(query.Page, query.PageSize, totalCount, products));

    }

    private static async Task<IReadOnlyCollection<ProductResponse>> GetProductsAsync(DbConnection dbConnection, GetProductsParameters parameters)
    {
        const string sql =
            $"""
              SELECT
                 id AS {nameof(ProductResponse.Id)},
                 title as {nameof(ProductResponse.Title)},
                 article as {nameof(ProductResponse.Article)},
                 description as {nameof(ProductResponse.Description)},
              FROM Products.products
              WHERE deleted = 0
              ORDER BY title
              OFFSET @Skip
              LIMIT @Take;
              """;

        List<ProductResponse> products = (await dbConnection.QueryAsync<ProductResponse>(sql, parameters)).AsList();
        
        return products;
    }
    
    private static async Task<int> CountProductsAsync(DbConnection connection, GetProductsParameters parameters)
    {
        const string sql =
            $"""
            SELECT COUNT(*)
            FROM Products.products
            WHERE deleted = 0
            """;

        int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return totalCount;
    }
    
    private sealed record GetProductsParameters(
        string Title,
        string Article,
        string Brand,
        int Take,
        int Skip);
}