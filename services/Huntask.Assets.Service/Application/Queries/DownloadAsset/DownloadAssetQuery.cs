using System;
using Huntask.Assets.Service.Application.Models;
using Huntask.Assets.Service.Domain.Models;

namespace Huntask.Assets.Service.Application.Queries.DownloadAsset;

public record DownloadAssetQuery(string Id) : IRequest<Result<DownloadAssetModel>>
{
}
