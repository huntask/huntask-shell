namespace Huntask.Common.Presentation.Models;

public record ApiLink(string Rel, string Href, string Method = "GET", string? Description = null!)
{
  public ApiLink() : this(string.Empty, string.Empty) { }
}