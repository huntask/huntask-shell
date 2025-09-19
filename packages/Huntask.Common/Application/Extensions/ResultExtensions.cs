using FluentValidation.Results;
using static Huntask.Common.Constants;

namespace Huntask.Common.Application.Extensions;

public static class ResultExtensions
{
  public static IActionResult ToActionResult<T>(this Result<T> result, int? successStatus = null)
  {
    if (result.IsSuccess)
    {

      return new ObjectResult(result.Value)
      {
        StatusCode = successStatus ?? GetResultStatusCode(result)
      };
    }

    return HandleErrors(result);
  }

  public static IActionResult ToActionResult(this Result result, int? successStatus = null)
  {
    if (result.IsSuccess)
    {
      return new StatusCodeResult(successStatus ?? GetResultStatusCode(result));
    }

    return HandleErrors(result);
  }

  public static Result AddValidationErrors(this Result result, ValidationResult validationResult)
  {
    validationResult.Errors.ForEach(e =>
    {
      result.WithError(e.ErrorMessage);
    });

    return result;
  }

  public static Result<T> AddValidationErrors<T>(this Result<T> result, ValidationResult validationResult)
  {
    validationResult.Errors.ForEach(e =>
    {
      result.WithError(e.ErrorMessage);
    });

    return result;
  }

  private static ObjectResult HandleErrors(IResultBase result)
  {
    var errorCode = result
      .Errors[0]?
      .Metadata
      .GetValueOrDefault(ResultMetadataKeys.ErrorCode) as string ?? ErrorCodes.InternalServerError;
    var status = ErrorCodes.ErrorCodeToHttpStatusCode.GetValueOrDefault(errorCode, (int)HttpStatusCode.InternalServerError);

    var problem = new ProblemDetails
    {
      Title = errorCode,
      Status = status,
      Detail = string.Join("; ", result.Errors.Select(e => e.Message))
    };
    problem.Extensions["errors"] = result.Errors.Select(e => new
    {
      message = e.Message,
      metadata = e.Metadata
    });

    IEnumerable<IReason> warnings = [];

    if (result is Result r)
    {
      warnings = r.Reasons.Where(r => r is not IError);
    }
    else
    {
      warnings = ((dynamic)result).Warnings;
    }

    problem.Extensions["warnings"] = warnings.Select(e => new
    {
      message = e.Message,
      metadata = e.Metadata
    });

    return new ObjectResult(problem)
    {
      StatusCode = status
    };
  }

  private static int GetResultStatusCode(IResultBase result)
  {
    var statusCodeStr = result
      .Successes[0]?
      .Metadata
      .GetValueOrDefault(ResultMetadataKeys.SuccessStatusCode) as string ?? HttpStatusCode.OK.ToString();
    if (int.TryParse(statusCodeStr, out var statusCode))
    {
      return statusCode;
    }

    return (int)HttpStatusCode.OK;
  }
}