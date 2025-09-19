namespace Huntask.Common.Application;

[ExcludeFromCodeCoverage]
public static class ErrorCodes
{
  public const string InternalServerError = "InternalServerError";
  public const string ValidationError = "ValidationError";
  public static string Unauthorized { get; set; } = "UserIsUnauthorized";

  public const string FileStreamIsEmptyOrNull = "FileStreamIsEmptyOrNull";
  public const string StreamIsNull = "StreamIsNull";

  public const string AssetNotFound = "AssetNotFound";
  public const string AssetIsNull = "AssetIsNull";

  public const string PasswordIsNotCorrect = "PasswordIsNotCorrect";
  public const string RegistrationIsNotSuccessful = "RegistrationIsNotSuccessful";
  public const string LoginFailed = "LoginFailed";

  public static readonly Dictionary<string, int> ErrorCodeToHttpStatusCode = new()
  {
    { InternalServerError, (int)HttpStatusCode.InternalServerError },
    { FileStreamIsEmptyOrNull, (int)HttpStatusCode.BadRequest },
    { AssetNotFound, (int)HttpStatusCode.NotFound },
    { AssetIsNull, (int)HttpStatusCode.InternalServerError },
    { StreamIsNull, (int)HttpStatusCode.InternalServerError },
    { PasswordIsNotCorrect, (int)HttpStatusCode.BadRequest },
    { RegistrationIsNotSuccessful, (int)HttpStatusCode.BadRequest },
    { LoginFailed, (int)HttpStatusCode.Unauthorized },
    { ValidationError, (int)HttpStatusCode.BadRequest }
  };
}