using MediatR;
using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
