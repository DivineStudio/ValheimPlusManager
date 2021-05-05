using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Octokit;
using ValheimPlusManager.Core.Extensions;
using ValheimPlusManager.Core.Models;

namespace ValheimPlusManager.Core.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class GitHubRepository : BaseRepository<GitHubRepository>, IGitHubRepository
    {
        #region Fields
        /// <summary>
        /// The repository owner's name in GitHub for the <see href="https://github.com/valheimPlus/ValheimPlus">Valheim Plus GitHub repository</see>
        /// </summary>
        private const string REPO_OWNER = "valheimPlus";
        /// <summary>
        /// The repository name in GitHub for the <see href="https://github.com/valheimPlus/ValheimPlus">Valheim Plus GitHub repository</see>
        /// </summary>
        private const string REPO_NAME = "ValheimPlus";

        /// <summary>
        /// IoC provided <see cref="IGitHubClient"/> object.
        /// </summary>
        private IGitHubClient _client; 
        #endregion

        public GitHubRepository(IGitHubClient gitHubClient)
        {
            _client = gitHubClient;
        }

        public override bool IsLoggerCreated => base.IsLoggerCreated;

        #region Methods
        /// <summary>
        /// Call and forget logging for rate limits of the API calls to GitHub.
        /// </summary>
        private Task LogRateLimit()
        {
            return Task.Run(() =>
            {
                var lastApiInfo = _client.GetLastApiInfo();
                if (lastApiInfo != null && IsLoggerCreated)
                {
                    Logger.Debug("{@lastApiInfo.RateLimit.Limit}", lastApiInfo.RateLimit.Limit);
                    Logger.Debug("{@lastApiInfo.RateLimit.Remaining}", lastApiInfo.RateLimit.Remaining);
                    Logger.Debug("{@lastApiInfo.RateLimit.Reset}", lastApiInfo.RateLimit.Reset);
                }
            });
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ReleaseInfo>> GetAllReleasesAsync()
        {
            var releases = await _client.Repository.Release.GetAll(REPO_OWNER, REPO_NAME);
            var releaseResponses = releases.Select(releases => releases.ToReleaseInfo());

            LogRateLimit().FireAndForget();

            return releaseResponses;
        }

        /// <inheritdoc/>
        public async Task<ReleaseInfo> GetExplicitReleaseAsync(string tag)
        {
            var release = await _client.Repository.Release.Get(REPO_OWNER, REPO_NAME, tag);

            LogRateLimit().FireAndForget();

            return release.ToReleaseInfo();
        }

        /// <inheritdoc/>
        public async Task<ReleaseInfo> GetLatestReleaseAsync()
        {
            var release = await _client.Repository.Release.GetLatest(REPO_OWNER, REPO_NAME);

            LogRateLimit().FireAndForget();

            return release.ToReleaseInfo();
        }

        /// <inheritdoc/>
        public async Task<bool> DownloadReleaseAsync(Uri releaseUri, Uri downloadSaveLocation)
        {
            using (var webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(releaseUri.ToString(), downloadSaveLocation.LocalPath);
            }

            LogRateLimit().FireAndForget();

            return true;
        }

        /// <inheritdoc/>
        public Task<bool> InstallAsync(Uri downloadSaveLocation, Uri gameFolderLocation) => throw new NotImplementedException();
        #endregion
    }
}
