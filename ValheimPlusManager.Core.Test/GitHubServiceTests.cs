using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Octokit;
using ValheimPlusManager.Core.Models;
using ValheimPlusManager.Core.Factories;
using ValheimPlusManager.Core.Repositories;
using ValheimPlusManager.Core.Services;
using System.IO;

namespace ValheimPlusManager.Core.Test.IntegrationTests
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "ExcludeTestMethodNames")]
    public class GitHubServiceTests : BaseFixture
    {
        private static readonly string _solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        private static readonly string _testDownloadFilePath = @$"{_solutionDirectory}\TestDownloads";
        private static readonly Uri _winClient = new Uri(@$"{_testDownloadFilePath}\WinClient");
        private static readonly Uri _winServClient = new Uri(@$"{_testDownloadFilePath}\WinServClient");

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            base.Ioc.RegisterSingleton<IGitHubClient>(GitHubClientFactory.Create());
            base.Ioc.RegisterSingleton<IGitHubRepository>(RepositoryFactory.Create<IGitHubRepository>());
            base.Ioc.RegisterSingleton<IGitHubService>(ServiceFactory.Create<IGitHubService>());
        }

        [Test]
        public void IsLoggerCreated()
        {
            var sut = base.Ioc.Resolve<IGitHubService>();
            Assert.NotNull(sut.IsLoggerCreated);
        }

        [Test]
        public async Task GetAllReleasesAsync_SuccessfullyRetrieveListOfReleases()
        {
            base.ClearAll();

            var list = await base.Ioc.Resolve<IGitHubService>().GetAllReleasesAsync();

            Assert.Greater(list.Count, 0);
        }

        [Test]
        public async Task GetLatestReleaseAsync_SuccessfullyRetrieveLatestReleases()
        {
            base.ClearAll();

            var release = await base.Ioc.Resolve<IGitHubService>().GetLatestReleaseAsync();

            Assert.NotNull(release);
        }

        [Test]
        public async Task GetExplicitReleaseAsync_SuccessfullyRetrieveExplicitReleases()
        {
            base.ClearAll();

            var targetedReleaseTag = "0.9.5";

            var releaseResponse = await base.Ioc.Resolve<IGitHubService>().GetExplicitReleaseAsync(targetedReleaseTag);

            Assert.AreEqual(releaseResponse.Tag.ToString(), targetedReleaseTag);
        }

        [TestCase(DownloadableAssets.WindowsClient)]
        [TestCase(DownloadableAssets.WindowsServer)]
        public async Task DownloadLatestReleaseAsync_SuccessfullyDownloadVPWindowsClient(DownloadableAssets downloadableAsset)
        {
            base.ClearAll();

            bool isDownloaded;

            if (downloadableAsset == DownloadableAssets.WindowsClient)
            {
                isDownloaded = await base.Ioc.Resolve<IGitHubService>().DownloadReleaseAsync(downloadableAsset, _winClient.LocalPath);
            }
            else if (downloadableAsset == DownloadableAssets.WindowsServer)
            {
                isDownloaded = await base.Ioc.Resolve<IGitHubService>().DownloadReleaseAsync(downloadableAsset, _winServClient.LocalPath);
            }
            else
            {
                isDownloaded = false;
            }

            Assert.IsTrue(isDownloaded);
        }

        [TestCase(DownloadableAssets.WindowsClient)]
        [TestCase(DownloadableAssets.WindowsServer)]
        public async Task DownloadExplicitReleaseAsync_SuccessfullyDownloadVPWindowsClient(DownloadableAssets downloadableAsset)
        {
            base.ClearAll();

            bool isDownloaded;

            if (downloadableAsset == DownloadableAssets.WindowsClient)
            {
                isDownloaded = await base.Ioc.Resolve<IGitHubService>().DownloadReleaseAsync(downloadableAsset, _winClient.LocalPath, "0.9.6");
            }
            else if (downloadableAsset == DownloadableAssets.WindowsServer)
            {
                isDownloaded = await base.Ioc.Resolve<IGitHubService>().DownloadReleaseAsync(downloadableAsset, _winServClient.LocalPath, "0.9.6");
            }
            else
            {
                isDownloaded = false;
            }

            Assert.IsTrue(isDownloaded);
        }

        [TestCase(DownloadableAssets.WindowsClient)]
        [TestCase(DownloadableAssets.WindowsServer)]
        public async Task InstallAsync_SuccessfullyInstallDownloadedClient(DownloadableAssets downloadableAsset)
        {
            base.ClearAll();

            bool isInstalled;

            if (downloadableAsset == DownloadableAssets.WindowsClient)
            {
                isInstalled = await base.Ioc.Resolve<IGitHubService>().InstallAsync(_winClient.LocalPath, _winClient.LocalPath);
            }
            else if (downloadableAsset == DownloadableAssets.WindowsServer)
            {
                isInstalled = await base.Ioc.Resolve<IGitHubService>().InstallAsync(_winServClient.LocalPath, _winServClient.LocalPath);
            }
            else
            {
                isInstalled = false;
            }

            Assert.IsTrue(isInstalled);
        }
    }
}
