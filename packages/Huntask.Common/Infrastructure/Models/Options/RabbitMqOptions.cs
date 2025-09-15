namespace Huntask.Common.Infrastructure.Models.Options;

public class RabbitMqOptions
{
  public string User { get; set; } = "";
  public string Password { get; set; } = "";
  public string Host { get; set; } = "rabbitmq";
};
