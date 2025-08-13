using Huntask.Assets.Service.Domain.Models;
using Huntask.Common.Application.Models;

namespace Huntask.Assets.Service.Application.Commands.DeleteAsset;

public record DeleteAssetCommand(string Id) : IRequest<Result<Asset>>;
