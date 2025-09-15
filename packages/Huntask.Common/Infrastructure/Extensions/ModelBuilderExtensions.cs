using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Huntask.Common.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
  public static ModelBuilder ConfigureMSConsumerOutbox(this ModelBuilder builder, string dbSchemaName)
  {
    builder.AddInboxStateEntity(b  => b.ToTable("inbox_state", dbSchemaName));
    builder.AddOutboxMessageEntity(b => b.ToTable("outbox_message", dbSchemaName));
    builder.AddOutboxStateEntity(b   => b.ToTable("outbox_state", dbSchemaName));

    return builder;
  }
}