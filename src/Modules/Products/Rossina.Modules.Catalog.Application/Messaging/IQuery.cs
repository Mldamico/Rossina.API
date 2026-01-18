using MediatR;
using Rossina.Modules.Catalog.Domain.Abstractions;

namespace Rossina.Modules.Catalog.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
