using Lambifast.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lambifast.Filters;

public class ExceptionFilter : IExceptionFilter
{
    protected IErrorResponseService ErrorResponseService { get; }
    private ILogger<ExceptionFilter> Logger { get; }

    public ExceptionFilter(
        IErrorResponseService errorResponseService, 
        ILogger<ExceptionFilter> logger)
    {
        ErrorResponseService = errorResponseService;
        Logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception != null)
        {
            var result = ErrorResponseService.CreateErrorResponse(context.Exception);
            context.Result = result;
            Logger.LogError(context.Exception, context.Exception.Message);
            context.ExceptionHandled = true;
        }
    }
}