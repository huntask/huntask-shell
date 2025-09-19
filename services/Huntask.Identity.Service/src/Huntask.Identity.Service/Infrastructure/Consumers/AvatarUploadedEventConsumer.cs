using Huntask.Identity.Service.Domain.Models;
using Huntask.Identity.Service.Infrastructure.Contexts;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Huntask.Identity.Service.Infrastructure.Consumers;

public class AvatarUploadedEventConsumer(
  IdentityContext dbContext,
  UserManager<User> userManager) : IConsumer<AvatarUploadedEvent>
{
  public async Task Consume(ConsumeContext<AvatarUploadedEvent> context)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(context.Message.AssetId, nameof(context.Message.AssetId));
    ArgumentException.ThrowIfNullOrWhiteSpace(context.Message.UserId, nameof(context.Message.UserId));

    var user = await userManager.FindByIdAsync(context.Message.UserId)
      ?? throw new InvalidOperationException($"User with ID '{context.Message.UserId}' not found.");

    user.AvatarAssetId = context.Message.AssetId;

    await userManager.UpdateAsync(user);
    await dbContext.SaveChangesAsync();
  }
}