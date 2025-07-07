namespace Huntask.Identity.Service.Presentation;

public static class ApiUtilConstants
{
  public static class Headers
  {
    public const string Authorization = "Authorization";
  }

  public static class CacheTTL
  {
    public const int Hot = 10;
    public const int Warm = 30;
    public const int Cold = 60;
  }
}