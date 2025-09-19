using Huntask.Assets.Service.Domain.Models;

namespace Huntask.Assets.Service.Application.Commands.UploadAsset;

public record UploadAssetCommand(UploadAssetModel Model) : IRequest<Result<Asset>>;
