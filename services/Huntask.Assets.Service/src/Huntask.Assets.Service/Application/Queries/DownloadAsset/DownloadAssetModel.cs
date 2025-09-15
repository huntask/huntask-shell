using System.Text.Json.Serialization;
using Huntask.Assets.Service.Domain.Models;

namespace Huntask.Assets.Service.Application.Queries.DownloadAsset;

public class DownloadAssetModel
{
  public Asset Asset { get; set; } = default!;

  [JsonIgnore]
  public Stream Stream { get; set; } = default!;
}
