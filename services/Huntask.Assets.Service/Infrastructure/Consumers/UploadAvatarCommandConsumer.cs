using Huntask.Assets.Service.Application.Commands.UploadAsset;
using Huntask.Assets.Service.Domain.Services;
using Huntask.Assets.Service.Infrastructure.Contexts;
using Huntask.Common.Contracts;
using MassTransit;

namespace Huntask.Assets.Service.Infrastructure.Consumers;

public class UploadAvatarCommandConsumer(
  AssetsContext dbContext,
  IAssetService assetService) : IConsumer<UploadAvatarCommand>
{
  public async Task Consume(ConsumeContext<UploadAvatarCommand> context)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(context.Message.UserId, nameof(context.Message.UserId));

    if (context.Message.Avatar is null && !context.Message.Avatar!.HasValue)
    {
      return;
    }

    await using var stream = await context.Message.Avatar.Value;

    var asset = await assetService.UploadAsync(new UploadAssetModel(
      FileStream: stream,
      FileName: context.Message.FileName,
      ContentType: context.Message.ContentType,
      ContainerName: context.Message.ContainerName));

    await context.Publish<AvatarUploadedEvent>(new
    {
      UserId = context.Message.UserId,
      AssetId = asset.Id
    });

    await dbContext.SaveChangesAsync();
  }
}