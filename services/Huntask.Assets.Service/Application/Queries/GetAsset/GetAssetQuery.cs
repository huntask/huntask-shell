using Huntask.Assets.Service.Domain.Models;
using Huntask.Common.Application.Models;

namespace Huntask.Assets.Service.Application.Queries.GetAsset;


public record GetAssetQuery(string Id) : IRequest<Result<Asset>>;
