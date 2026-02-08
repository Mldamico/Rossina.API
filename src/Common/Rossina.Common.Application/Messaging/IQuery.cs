using MediatR;
using Rossina.Common.Domain.Abstractions;

namespace Rossina.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
