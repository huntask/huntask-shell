namespace Huntask.Common.Infrastructure.Models.Options;

public class ConnectionStringsOptions
{
  public string HuntaskDbConnection { get; set; } = "";
  public string RabbitMqConnection { get; set; } = "";
};
