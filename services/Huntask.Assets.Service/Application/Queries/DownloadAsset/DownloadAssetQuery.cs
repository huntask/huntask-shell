using Huntask.Common.Application.Models;

namespace Huntask.Assets.Service.Application.Queries.DownloadAsset;

public record DownloadAssetQuery(string Id) : IRequest<Result<DownloadAssetModel>>
{
}
