namespace Lambifast.Dtos;

public class InvalidParameterDto
{
    /// <summary>
    /// Field Name
    /// </summary>
    /// <example>field1</example>
    public string? Name { get; set; }

    /// <summary>
    /// Error details
    /// </summary>
    /// <example>field1 cannot be null</example>
    public string? Reason { get; set; }
}