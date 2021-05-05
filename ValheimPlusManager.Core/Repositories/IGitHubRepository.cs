using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;
using ValheimPlusManager.Core.Models;

namespace ValheimPlusManager.Core.Repositories
{
    public interface IGitHubRepository : IRepository
    {
        /// <summary>
        /// Returns a list of all releases found on the Valheim Plus GitHub repository.
        /// </summary>
        /// <returns>IEnumerable of Release responses for all releases.</returns>
        /// <exception cref="ApiException"/>
        Task<IEnumerable<ReleaseInfo>> GetAllReleasesAsync();

        /// <summary>
        /// Returns an explicit release found on the Valheim Plus GitHub repository.
        /// </summary>
        /// <returns>Release response for an explicit release.</returns>
        /// <exception cref="ApiException"/>
        Task<ReleaseInfo> GetExplicitReleaseAsync(string version);

        /// <summary>
        /// Returns an explicit release found on the Valheim Plus GitHub repository.
        /// </summary>
        /// <returns>Release response for latest release.</returns>
        /// <exception cref="ApiException"/>
        Task<ReleaseInfo> GetLatestReleaseAsync();

        /// <summary>
        /// Download a specified release of Valheim Plus to a specified file location.
        /// </summary>
        /// <param name="releaseUri">URL of Valheim Plus release.</param>
        /// <param name="downloadSaveLocation">File location of downloaded release to save to the file system.
        ///     Use a file system path to specify the location to save the file.
        /// </param>
        /// <returns>Boolean indicating the success of the download file.</returns>
        /// <exception cref="ApiException"/>
        Task<bool> DownloadReleaseAsync(Uri releaseUri, Uri downloadSaveLocation);

        /// <summary>
        /// Installs a downloaded release of Valheim Plus to the Valheim game folder.
        /// </summary>
        /// <param name="downloadSaveLocation">The file system location of the downloaded release.</param>
        /// <param name="gameFolderLocation">The file system location of the Valheim game folder where the downloaded Valheim Plus release will be installed to.</param>
        /// <returns></returns>
        /// <exception cref="ApiException"/>
        Task<bool> InstallAsync(Uri downloadSaveLocation, Uri gameFolderLocation);
    }
}
