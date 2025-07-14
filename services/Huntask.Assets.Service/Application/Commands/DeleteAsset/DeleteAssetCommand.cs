using Huntask.Assets.Service.Application.Models;
using Huntask.Assets.Service.Domain.Models;

namespace Huntask.Assets.Service.Application.Commands.DeleteAsset;

public record DeleteAssetCommand(string Id) : IRequest<Result<Asset>>;
