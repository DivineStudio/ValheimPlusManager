using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;
using ValheimPlusManager.Core.Models;

namespace ValheimPlusManager.Core.Services
{
    public interface IGitHubService : IService
    {
        /// <summary>
        /// Returns a list of all releases found on the <see href="https://github.com/valheimPlus/ValheimPlus">Valheim Plus GitHub repository</see>
        /// </summary>
        /// <returns>Release response for all releases.</returns>
        /// <exception cref="ApiException"/>
        Task<List<ReleaseInfo>> GetAllReleasesAsync();

        /// <summary>
        /// Returns the latest release found on the <see href="https://github.com/valheimPlus/ValheimPlus">Valheim Plus GitHub repository</see>
        /// </summary>
        /// <returns>Release response for an explicit release.</returns>
        /// <exception cref="ApiException"/>
        Task<ReleaseInfo> GetExplicitReleaseAsync(string tag);

        /// <summary>
        /// Returns an explicit release found on the <see href="https://github.com/valheimPlus/ValheimPlus">Valheim Plus GitHub repository</see>
        /// </summary>
        /// <returns>Release response for latest release.</returns>
        /// <exception cref="ApiException"/>
        Task<ReleaseInfo> GetLatestReleaseAsync();

        /// <summary>
        /// Download the specified release of Valheim Plus to a specified file location from the <see href="https://github.com/valheimPlus/ValheimPlus">Valheim Plus GitHub repository</see>.
        /// </summary>
        /// <param name="downloadableAsset">The assets available for downloading from the Valheim Plus GitHub.</param>
        /// <param name="release">The <see cref="Release"/> object that holds the URL to download the release from.</param>
        /// <param name="downloadSaveLocation">The file system location that the download will be saved to.</param>
        /// <returns>Boolean indicating the success of the download file.</returns>
        /// <exception cref="ApiException"/>
        /// <exception cref="ArgumentException"/>
        Task<bool> DownloadReleaseAsync(DownloadableAssets downloadableAsset, ReleaseInfo releaseResponse, string downloadSaveLocation = null);

        /// <summary>
        /// Download the specified or latest release of Valheim Plus to a specified file location from the <see href="https://github.com/valheimPlus/ValheimPlus">Valheim Plus GitHub repository</see>.
        /// </summary>
        /// <param name="downloadableAsset">The assets available for downloading from the Valheim Plus GitHub.</param>
        /// <param name="tag">The version or "tag" of the release asset to download from the Valheim Plus GitGub. Leave <see cref="null"/> for the latest release.</param>
        /// <param name="downloadSaveLocation">The file system location that the download will be saved to.</param>
        /// <returns>Boolean indicating the success of the download file.</returns>
        /// <exception cref="ApiException"/>
        /// <exception cref="ArgumentException"/>
        Task<bool> DownloadReleaseAsync(DownloadableAssets downloadableAsset, string downloadSaveLocation = null, string tag = null);

        /// <summary>
        /// Installs the release currently downloaded in the Valheim game folder.
        /// </summary>
        /// <param name="downloadSaveLocation">The file system location that the download will be saved to.</param>
        /// <param name="gameFolderLocation">The file system location of the Valheim game folder where the downloaded Valheim Plus release will be installed to.</param>
        /// <returns>Returns true if the install is successful, otherwise, returns false.</returns>
        /// <exception cref="ApiException"/>
        /// <exception cref="ArgumentException"/>
        Task<bool> InstallAsync(string downloadSaveLocation, string gameFolderLocation);

        /// <summary>
        /// Download the latest release of Valheim Plus to a specified file location from the <see href="https://github.com/valheimPlus/ValheimPlus">Valheim Plus GitHub repository</see>,
        ///     then install Valheim Plus to the Valheim game folder location provided.
        /// </summary>
        /// <param name="downloadableAsset">The assets available for downloading from the Valheim Plus GitHub.</param>
        /// <param name="gameFolderLocation">The file system location of the Valheim game folder where the downloaded Valheim Plus release will be installed to.</param>
        /// <param name="downloadSaveLocation">The file system location that the download will be saved to.</param>
        /// <returns>Boolean indicating the success of the download and installation.</returns>
        /// <exception cref="ApiException"/>
        /// <exception cref="ArgumentException"/>
        Task<bool> DownloadLatestReleaseAndInstallAsync(DownloadableAssets downloadableAsset, string gameFolderLocation, string downloadSaveLocation = null);

        /// <summary>
        /// Download an explicit release of Valheim Plus to a specified file location from the <see href="https://github.com/valheimPlus/ValheimPlus">Valheim Plus GitHub repository</see>,
        ///     then install Valheim Plus to the Valheim game folder location provided.
        /// </summary>
        /// <param name="downloadableAsset">The assets available for downloading from the Valheim Plus GitHub.</param>
        /// <param name="gameFolderLocation">The file system location of the Valheim game folder where the downloaded Valheim Plus release will be installed to.</param>
        /// <param name="downloadSaveLocation">The file system location that the download will be saved to.</param>
        /// <returns>Boolean indicating the success of the download and installation.</returns>
        /// <exception cref="ApiException"/>
        /// <exception cref="ArgumentException"/>
        Task<bool> DownloadExplicitReleaseAndInstallAsync(DownloadableAssets downloadableAsset, string tag, string gameFolderLocation, string downloadSaveLocation = null);
    }
}
