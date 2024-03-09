using Lambifast.Errors;

namespace Lambifast.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException(BaseErrorCode code, string message)
        : base(code, message)
    {
    }

    public ConflictException(Exception innerException, BaseErrorCode code, string message)
        : base(innerException, code, message)
    {
    }
}