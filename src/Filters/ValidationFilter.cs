using Lambifast.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lambifast.Filters;

public class ValidationFilter : IActionFilter
{
    private IServiceProvider ServiceProvider { get; }
    private IErrorResponseService ErrorResponseService { get; }

    public ValidationFilter(
        IServiceProvider serviceProvider, 
        IErrorResponseService errorResponseService)
    {
        ServiceProvider = serviceProvider;
        ErrorResponseService = errorResponseService;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Method intentionally left empty.
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        List<ValidationResult> validationResults = new List<ValidationResult>();
        foreach (var arg in context.ActionArguments)
        {
            var value = arg.Value;
            if (value != null)
            {
                Type type = value.GetType();
                Type validatorType = typeof(IValidator).MakeGenericType(type);
                var validator = ServiceProvider.GetService(validatorType) as IValidator;
                if (validator != null)
                {
                    Type contextType = typeof(ValidationContext<>);
                    var validationContext = (IValidationContext)Activator.CreateInstance(contextType.MakeGenericType(type), value);
                    var validationResult = validator.Validate(validationContext);
                    if (!validationResult.IsValid)
                        validationResults.Add(validationResult);
                }
            }
        }
        if (validationResults.Any())
        {
            var result = ErrorResponseService.CreateErrorResponse(validationResults);
            context.Result = result;
        }
    }
}