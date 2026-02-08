
using Rossina.Common.Domain.Abstractions;

namespace Rossina.Common.Application.Exceptions;

public sealed class GlobalException : Exception
{
    public GlobalException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
