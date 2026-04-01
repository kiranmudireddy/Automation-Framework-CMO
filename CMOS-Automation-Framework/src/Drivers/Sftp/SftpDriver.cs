using CMOS_Automation_Framework.src.Config;
using CMOS_Automation_Framework.src.Models.FileModels;
using CMOS_Automation_Framework.src.Services.Sftp;

namespace CMOS_Automation_Framework.src.Drivers.Sftp;

public class SftpDriver : ISftpService
{
    private readonly SftpSettings _settings;

    public SftpDriver(SftpSettings settings)
    {
        _settings = settings;
    }

    public bool IsConfigured()
    {
        return !string.IsNullOrWhiteSpace(_settings.Host) &&
               !string.IsNullOrWhiteSpace(_settings.Username) &&
               !string.IsNullOrWhiteSpace(_settings.Password);
    }

    public Task UploadAsync(FileArtifact fileArtifact, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(fileArtifact);

        if (!IsConfigured())
        {
            throw new InvalidOperationException("SFTP settings are incomplete. Configure host, username, and password before uploading files.");
        }

        throw new NotSupportedException("SFTP upload transport is not yet wired to a concrete SSH provider. Add the provider implementation behind SftpDriver when the team selects one.");
    }

    public Task<string> DownloadAsync(string remoteFileName, string localDirectory, CancellationToken cancellationToken = default)
    {
        if (!IsConfigured())
        {
            throw new InvalidOperationException("SFTP settings are incomplete. Configure host, username, and password before downloading files.");
        }

        throw new NotSupportedException("SFTP download transport is not yet wired to a concrete SSH provider. Add the provider implementation behind SftpDriver when the team selects one.");
    }
}
