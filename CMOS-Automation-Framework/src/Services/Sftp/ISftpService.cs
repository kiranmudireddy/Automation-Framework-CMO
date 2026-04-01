using CMOS_Automation_Framework.src.Models.FileModels;

namespace CMOS_Automation_Framework.src.Services.Sftp;

public interface ISftpService
{
    Task UploadAsync(FileArtifact fileArtifact, CancellationToken cancellationToken = default);
    Task<string> DownloadAsync(string remoteFileName, string localDirectory, CancellationToken cancellationToken = default);
}
