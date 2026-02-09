using MediatR;
using Rossina.Common.Domain.Abstractions;

namespace Rossina.Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
