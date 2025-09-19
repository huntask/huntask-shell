using Huntask.Assets.Service.Domain.Models;

namespace Huntask.Assets.Service.Application.Queries.GetAsset;


public record GetAssetQuery(string Id) : IRequest<Result<Asset>>;
