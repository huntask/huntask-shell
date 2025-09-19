using Microsoft.Extensions.Logging;

namespace Huntask.Common.Application.Extensions;

public static class ResultLoggingExtensions
{
  public static Result<T> LogErrors<T, L>(this Result<T> result, ILogger<L> logger, string? message = null)
  {
    if (result.IsFailed)
    {
      foreach (var error in result.Errors)
      {
        if (message is null)
        {
          logger.LogError("Error: {ErrorMessage}, Metadata: {Metadata}", error.Message, error.Metadata);
          continue;
        }
        logger.LogError("{Message}. Error: {ErrorMessage}, Metadata: {Metadata}", message, error.Message, error.Metadata);
      }
    }

    return result;
  }

  public static Result LogErrors<L>(this Result result, ILogger<L> logger, string? message = null)
  {
    if (result.IsFailed)
    {
      foreach (var error in result.Errors)
      {
        if (message is null)
        {
          logger.LogError("Error: {ErrorMessage}, Metadata: {Metadata}", error.Message, error.Metadata);
          continue;
        }
        logger.LogError("{Message}. Error: {ErrorMessage}, Metadata: {Metadata}", message, error.Message, error.Metadata);
      }
    }

    return result;
  }

  public static Result<T> LogWarnings<T, L>(this Result<T> result, ILogger<L> logger, string? message = null)
  {
    if (result.IsFailed)
    {
      var warnings = result.Reasons.Where(r => r is not IError);
      foreach (var warning in warnings)
      {
        if (message is null)
        {
          logger.LogWarning("Warning: {WarningMessage}, Metadata: {Metadata}", warning.Message, warning.Metadata);
          continue;
        }
        logger.LogWarning("{Message}. Warning: {WarningMessage}, Metadata: {Metadata}", message, warning.Message, warning.Metadata);
      }
    }

    return result;
  }

  public static Result LogWarnings<L>(this Result result, ILogger<L> logger, string? message = null)
  {
    if (result.IsFailed)
    {
      var warnings = result.Reasons.Where(r => r is not IError);
      foreach (var warning in warnings)
      {
        if (message is null)
        {
          logger.LogWarning("Warning: {WarningMessage}, Metadata: {Metadata}", warning.Message, warning.Metadata);
          continue;
        }
        logger.LogWarning("{Message}. Warning: {WarningMessage}, Metadata: {Metadata}", message, warning.Message, warning.Metadata);
      }
    }

    return result;
  }
}