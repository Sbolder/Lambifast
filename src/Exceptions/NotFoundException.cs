using Lambifast.Errors;

namespace Lambifast.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(BaseErrorCode code, string message) 
        : base(code, message)
    {
    }

    public NotFoundException(Exception innerException, BaseErrorCode code, string message) 
        : base(innerException, code, message)
    {
    }
}