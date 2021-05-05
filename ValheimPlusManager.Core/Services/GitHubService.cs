using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValheimPlusManager.Core.Models;
using ValheimPlusManager.Core.Repositories;
using ValheimPlusManager.Core.Exceptions;
using ValheimPlusManager.Core.ErrorHandling;

namespace ValheimPlusManager.Core.Services
{
    /// <summary>
    /// Service providing tools to communicate with the <see href="https://github.com/valheimPlus/ValheimPlus">Valheim Plus GitHub repository</see>,
    ///     downloading releases, and installing downloaded releases the Valheim game folder.
    /// </summary>
    public class GitHubService : BaseService<GitHubService>, IGitHubService
    {
        #region Fields
        /// <summary>
        /// IoC provided <see cref="IGitHubRepository"/> object.
        /// </summary>
        private IGitHubRepository _gitHubRepository;
        #endregion

        public GitHubService(IGitHubRepository gitHubRepository)
        {
            _gitHubRepository = gitHubRepository;
        }

        public override bool IsLoggerCreated => base.IsLoggerCreated;

        #region Methods
        /// <summary>
        /// Returns a URI as a file system location, including name of the download file, to save to.
        /// </summary>
        /// <param name="downloadableAsset">The assets available for downloading from the Valheim Plus GitHub.</param>
        /// <param name="downloadSaveLocation">The file system location that the download will be saved to.</param>
        /// <returns>URI file system download save location.</returns>
        private string GetFilenameFromDownloadableAsset(DownloadableAssets downloadableAsset, string downloadSaveLocation)
        {
            return $@"{downloadSaveLocation}\{downloadableAsset.ToString().ToLower()}.zip";
        }

        /// <inheritdoc/>
        public async Task<List<ReleaseInfo>> GetAllReleasesAsync()
        {
            return await _gitHubRepository.GetAllReleasesAsync() as List<ReleaseInfo>;
        }

        /// <inheritdoc/>
        public async Task<ReleaseInfo> GetExplicitReleaseAsync(string tag)
        {
            return await _gitHubRepository.GetExplicitReleaseAsync(tag);
        }

        /// <inheritdoc/>
        public async Task<ReleaseInfo> GetLatestReleaseAsync()
        {
            return await _gitHubRepository.GetLatestReleaseAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> DownloadReleaseAsync(DownloadableAssets downloadableAsset, ReleaseInfo releaseInfo, string downloadSaveLocation)
        {
            var errorMessages = new ErrorMessageEnvelope();
            var filename = GetFilenameFromDownloadableAsset(downloadableAsset, downloadSaveLocation);
            var isValidReleaseUri = false;
            var isValidFilenameUri = false;
            Uri releaseUri = null;
            Uri filenameUri = null;

            if (releaseInfo != null)
            {
                try
                {
                    isValidReleaseUri = Uri.TryCreate(releaseInfo.Url.ToString(), UriKind.Absolute, out releaseUri);
                }
                catch (UriFormatException)
                {
                }

                if (!isValidReleaseUri)
                {
                    errorMessages.AddMessage(releaseInfo.Url, nameof(releaseInfo), nameof(releaseInfo.Url));
                }

            }
            else
            {
                errorMessages.AddMessage(releaseInfo, nameof(releaseInfo));
            }

            try
            {
                isValidFilenameUri = Uri.TryCreate(filename, UriKind.Absolute, out filenameUri);
            }
            catch (UriFormatException)
            {
            }

            if (!isValidFilenameUri)
            {
                errorMessages.AddMessage(downloadSaveLocation, nameof(downloadSaveLocation));
            }

            if (errorMessages.HasMessages)
            {
                errorMessages.Throw<ArgumentException>();
            }

            return await _gitHubRepository.DownloadReleaseAsync(releaseUri, filenameUri);
        }

        /// <inheritdoc/>
        public async Task<bool> DownloadReleaseAsync(DownloadableAssets downloadableAsset, string downloadSaveLocation = null, string tag = null)
        {
            var releaseResponse = string.IsNullOrWhiteSpace(tag) ?
                await GetLatestReleaseAsync() :
                await GetExplicitReleaseAsync(tag);

            return await DownloadReleaseAsync(downloadableAsset, releaseResponse, downloadSaveLocation);
        }

        /// <inheritdoc/>
        public async Task<bool> InstallAsync(string downloadSaveLocation, string gameFolderLocation)
        {
            var isInstalled = false;

            if (Uri.TryCreate(downloadSaveLocation, UriKind.RelativeOrAbsolute, out var dsl) &&
                Uri.TryCreate(gameFolderLocation, UriKind.RelativeOrAbsolute, out var gfl))
            {
                isInstalled = await _gitHubRepository.InstallAsync(dsl, gfl);
            }

            return isInstalled;
        }

        /// <inheritdoc/>
        public Task<bool> DownloadLatestReleaseAndInstallAsync(DownloadableAssets downloadableAsset, string gameFolderLocation, string downloadSaveLocation = null) => throw new NotImplementedException();

        /// <inheritdoc/>
        public Task<bool> DownloadExplicitReleaseAndInstallAsync(DownloadableAssets downloadableAsset, string tag, string gameFolderLocation, string downloadSaveLocation = null) => throw new NotImplementedException(); 
        #endregion
    }
}
