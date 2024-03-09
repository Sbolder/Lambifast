using Lambifast.Errors;

namespace Lambifast.Exceptions;

public class GenericException : BaseException
{
    public GenericException(BaseErrorCode code, string message)
        : base(code, message)
    {
    }

    public GenericException(Exception innerException, BaseErrorCode code, string message) 
        : base(innerException, code, message)
    {
    }
}