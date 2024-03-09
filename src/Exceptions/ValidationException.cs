using Lambifast.Errors;

namespace Lambifast.Exceptions;

public class ValidationException : BaseException
{
    public IEnumerable<ValidationDetail> ValidationDetails { get; } = new List<ValidationDetail>();

    public ValidationException(BaseErrorCode code, string message, IEnumerable<ValidationDetail> details) 
        : base(code, message)
    {
        ValidationDetails = details;
    }

    public ValidationException(Exception innerException, BaseErrorCode code, string message, IEnumerable<ValidationDetail> details)
        : base(innerException, code, message)
    {
        ValidationDetails = details;
    }

    public ValidationException(BaseErrorCode code, string message)
        : base(code, message)
    {
    }

    public ValidationException(Exception innerException, BaseErrorCode code, string message) 
        : base(innerException, code, message)
    {
    }

    public class ValidationDetail
    {
        public BaseErrorCode? Code { get; set; }
        public string? Message { get; set; }
        public string? Field { get; set; }
        public string? Scope { get; set; }
    }
}