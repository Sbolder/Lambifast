namespace Lambifast.Errors;

public abstract class BaseErrorCode
{
    public string Code { get; }
    
    protected BaseErrorCode(string code)
    {
        Code = code;
    }

    public override bool Equals(object? obj)
    {
        return obj is BaseErrorCode code &&
               Code == code.Code;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code);
    }
}