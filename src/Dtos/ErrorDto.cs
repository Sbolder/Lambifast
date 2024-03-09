namespace Lambifast.Dtos;

public class ErrorDto
{
    /// <summary>
    /// Error title
    /// </summary>
    /// <example>Error title</example>
    public string? Title { get; set; }

    /// <summary>
    /// Error details
    /// </summary>
    /// <example>Error details</example>
    public string? Detail { get; set; }

    /// <summary>
    /// Status Code
    /// </summary>
    /// <example>400</example>
    public int? Status { get; set; }

    /// <summary>
    /// Invalid parameters list (only with 400 error code in case of input validation errors)
    /// </summary>
    public IList<InvalidParameterDto> InvalidParams { get; set; } = new List<InvalidParameterDto>();
}