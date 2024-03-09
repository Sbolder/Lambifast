using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Lambifast.Services;

public interface IErrorResponseService
{
    ObjectResult CreateErrorResponse(Exception ex);
    ObjectResult CreateErrorResponse(IEnumerable<ValidationResult> validationResults);
}