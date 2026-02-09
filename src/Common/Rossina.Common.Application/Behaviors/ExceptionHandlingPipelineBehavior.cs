using MediatR;
using Microsoft.Extensions.Logging;
using Rossina.Common.Application.Exceptions;

namespace Rossina.Common.Application.Behaviors;

internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unhandled exception for {request}", typeof(TRequest).Name);
            throw new GlobalException(typeof(TRequest).Name, innerException: e);
        }
    }
}