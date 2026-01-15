using System.Data;
using System.Data.Common;

namespace Rossina.Modules.Catalog.Application.Abstractions.Data;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}