using Huntask.Assets.Service.Domain.Models;
using Huntask.Common.Application.Models;

namespace Huntask.Assets.Service.Application.Commands.UploadAsset;

public record UploadAssetCommand(UploadAssetModel Model) : IRequest<Result<Asset?>>;
