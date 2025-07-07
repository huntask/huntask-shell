using System.Text.Json.Serialization;

namespace Huntask.Identity.Service.Domain.Entities;

public class TaskItem
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public bool IsComplete { get; set; }

  [JsonIgnore]
  public string UserId { get; set; } = "";

  [JsonIgnore]
  public bool IsNull { get; private set; } = false;

  [JsonIgnore]
  public static TaskItem NullTodo => new()
  {
    Id = -1,
    Name = null,
    IsComplete = false,
    IsNull = true
  };
}