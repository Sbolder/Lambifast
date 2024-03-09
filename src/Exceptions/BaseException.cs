using Lambifast.Errors;

namespace Lambifast.Exceptions;

public abstract class BaseException : Exception
{
    public BaseErrorCode Code { get; }

    protected BaseException(BaseErrorCode code, string message)
        : base(message)
    {
        Code = code;
    }

    protected BaseException(Exception innerException, BaseErrorCode code, string message)
        : base(message, innerException)
    {
        Code = code;
    }
}