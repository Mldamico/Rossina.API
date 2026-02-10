using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Rossina.Common.Application.Caching;
using Rossina.Common.Application.Data;
using Rossina.Common.Infrastructure.Caching;
using Rossina.Common.Infrastructure.Data;
using StackExchange.Redis;

namespace Rossina.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseConnectionString,
        string redisConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);
        
        IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        services.TryAddSingleton(connectionMultiplexer);
        services.AddStackExchangeRedisCache(options => 
            options.ConnectionMultiplexerFactory= () => Task.FromResult(connectionMultiplexer));        
        services.TryAddSingleton<ICacheService, CacheService>();
        
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        return services;
    }
}