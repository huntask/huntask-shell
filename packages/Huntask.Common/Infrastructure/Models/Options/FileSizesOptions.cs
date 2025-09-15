namespace Huntask.Common.Infrastructure.Models.Options;

public class FileSizesOptions
{
  public int MaxAssetFileSize { get; set; } = 50;
  public int MessageDataThresholdSize { get; set; } = 5;
  public int MaxAvatarFileSize { get; set; } = 3;
};
