using System.Net;
using Lambifast.Dtos;
using Microsoft.AspNetCore.Mvc;
using Lambifast.Exceptions;
using Lambifast.Extensions;
using FluentValidation.Results;

namespace Lambifast.Services.Impl;

public class ErrorResponseService : IErrorResponseService
	{
		public ObjectResult CreateErrorResponse(Exception ex)
		{
			if (ex is NotFoundException)
				return InternalCreateErrorResponse(
					errorCode: HttpStatusCode.NotFound,
					exception: (NotFoundException)ex);
			else if (ex is ConflictException)
				return InternalCreateErrorResponse(
					errorCode: HttpStatusCode.Conflict,
					exception: (ConflictException)ex);
			else if (ex is GenericException)
				return InternalCreateErrorResponse(
					errorCode: HttpStatusCode.InternalServerError,
					exception: (GenericException)ex);
			else if (ex is ValidationException)
				return InternalCreateErrorResponse(
					errorCode: HttpStatusCode.BadRequest,
					exception: (ValidationException)ex);
			else
				return InternalCreateErrorResponse(
					status: (int)HttpStatusCode.InternalServerError,
					exception: ex);
		}

		private ObjectResult InternalCreateErrorResponse(
			int status,
			Exception exception)
		{
			var error = CreateInternalError(status, exception);
			var result = new ObjectResult(error)
			{
				StatusCode = (int)HttpStatusCode.InternalServerError
			};
			return result;
		}

		private ObjectResult InternalCreateErrorResponse<TException>(
					HttpStatusCode errorCode,
					TException exception)
					where TException : BaseException
		{
			ErrorDto error = CreateError<TException>((int)errorCode, exception);
			var result = new ObjectResult(error)
			{
				StatusCode = (int)errorCode
			};
			return result;
		}

		public static ErrorDto CreateInternalError(int statusCode, Exception ex)
		{
			ErrorDto error = new ErrorDto
			{
				Status = statusCode,
				Title = "Internal Error",
				Detail = ex.Message
			};
			return error;
		}

		private static ErrorDto CreateError<TException>(int status, Exception ex)
			where TException : BaseException
		{
			if (ex is ValidationException)
				return TrasformException(
					status: status,
					exception: (ValidationException)ex,
					enhanceException: (err, exc) =>
					{
						err.InvalidParams = exc.ValidationDetails?.Select(v =>
						{
							var detail = new InvalidParameterDto
							{
								Name = v.Field,
								Reason = v.Message
							};
							return detail;
						}).ToList();
					});
			else
				return TrasformException<TException>(
					status: status,
					exception: (TException)ex);
		}

		private static ErrorDto TrasformException<TException>(
			int status,
			TException exception,
			Action<ErrorDto, TException> enhanceException = null)
			where TException : BaseException
		{
			ErrorDto error = new ErrorDto
			{
				Title = exception.Code.Code,
				Detail = exception.Message,
				Status = status,
			};
			if (enhanceException != null)
			{
				enhanceException(error, exception);
			}

			return error;
		}

		public ObjectResult CreateErrorResponse(IEnumerable<ValidationResult> validationResults)
		{
			var validationError = TrasformValidationException(validationResults);
			return new ObjectResult(validationError)
			{
				StatusCode = (int)HttpStatusCode.BadRequest
			};
		}

		private static ErrorDto TrasformValidationException(
			IEnumerable<ValidationResult> validationResults)
		{
			var validationError = new ErrorDto
			{
				Status = (int)HttpStatusCode.BadRequest,
				Title = "VALIDATION",
				Detail = "Error validating input"
			};

			foreach (ValidationResult validationResult in validationResults)
			{
				foreach (var error in validationResult.Errors)
				{
					validationError.InvalidParams.Add(new InvalidParameterDto
					{
						Name = error.PropertyName.ToCamelCase(),
						Reason = error.ErrorMessage,
					});
				};
			}

			return validationError;
		}
	}