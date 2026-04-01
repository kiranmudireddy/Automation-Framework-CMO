using CMOS_Automation_Framework.src.Models.FileModels;
using CMOS_Automation_Framework.src.Services.Sftp;
using CMOS_Automation_Framework.src.Utils.Common;

namespace CMOS_Automation_Framework.src.Helpers.File;

public class FileTransferHelper : IFileTransferHelper
{
    private readonly ISftpService _sftpService;
    private readonly PathHelper _pathHelper;

    public FileTransferHelper(ISftpService sftpService, PathHelper pathHelper)
    {
        _sftpService = sftpService;
        _pathHelper = pathHelper;
    }

    public Task UploadAsync(FileArtifact fileArtifact, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(fileArtifact);

        if (!System.IO.File.Exists(fileArtifact.FullPath))
        {
            throw new FileNotFoundException($"Cannot upload file because it does not exist: {fileArtifact.FullPath}", fileArtifact.FullPath);
        }

        return _sftpService.UploadAsync(fileArtifact, cancellationToken);
    }

    public Task<string> DownloadAsync(string remoteFileName, string localDirectory, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteFileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(localDirectory);

        _pathHelper.EnsureDirectory(localDirectory);
        return _sftpService.DownloadAsync(remoteFileName, localDirectory, cancellationToken);
    }
}
