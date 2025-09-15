namespace Huntask.Common;

public static class Constants
{
  public const string LogTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] [CorrelationId] {Message:lj}{NewLine}{Exception}";

  public static class SizeUnitsConstants
  {
    public const int KB = 1024;
    public const int MB = KB * 1024;
    public const int GB = MB * 1024;
  }
}
