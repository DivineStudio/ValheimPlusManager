using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using ValheimPlusManager.Core.Models;
using ValheimPlusManager.Core.Repositories;
using ValheimPlusManager.Core.Services;
using Octokit;

namespace ValheimPlusManager.Core.Unit.Test
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "ExcludeTestMethodNames")]
    public class GitHubServiceUnitTests
    {
        private Mock<IGitHubRepository> _mockRepo;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<IGitHubRepository>();
        }

        [Test]
        public void GetAllReleasesAsync_ThrowsApiException()
        {
            _mockRepo.Setup(repo => repo.GetAllReleasesAsync().Result).Throws(new ApiException());

            var sut = new GitHubService(_mockRepo.Object);

            var ex = Assert.ThrowsAsync<ApiException>(() => sut.GetAllReleasesAsync());
        }

        [Test]
        public async Task GetAllReleasesAsync_SuccessfullyRetrieveListOfReleases()
        {
            var tag = Version.Parse("1.2.3.4");
            var url = new Uri("http://github.com");
            var mockReleaseResponse = new Mock<ReleaseInfo>();
            mockReleaseResponse.SetupGet(prop => prop.Tag).Returns(tag);
            mockReleaseResponse.SetupGet(prop => prop.Url).Returns(url);
            var expected = new List<ReleaseInfo>()
            {
                mockReleaseResponse.Object
            };

            _mockRepo.Setup(repo => repo.GetAllReleasesAsync().Result).Returns(expected);

            var sut = new GitHubService(_mockRepo.Object);

            var actual = await sut.GetAllReleasesAsync();

            _mockRepo.Verify(repo => repo.GetAllReleasesAsync(), Times.Once);

            var releaseInfo = actual.First();

            Assert.IsTrue(actual.Any());
            Assert.AreEqual(expected.First(), actual.First());
            Assert.AreEqual(tag, releaseInfo.Tag);
            Assert.AreEqual(url, releaseInfo.Url);
        }

        [Test]
        public void GetLatestReleaseAsync_ThrowsApiException()
        {
            _mockRepo.Setup(repo => repo.GetLatestReleaseAsync().Result).Throws(new ApiException());

            var sut = new GitHubService(_mockRepo.Object);

            var ex = Assert.ThrowsAsync<ApiException>(() => sut.GetLatestReleaseAsync());
        }

        [Test]
        public async Task GetLatestReleaseAsync_SuccessfullyRetrieveLatestRelease()
        {
            var tag = Version.Parse("1.2.3.4");
            var url = new Uri("http://github.com");
            var mockReleaseResponse = new Mock<ReleaseInfo>();
            mockReleaseResponse.SetupGet(prop => prop.Tag).Returns(tag);
            mockReleaseResponse.SetupGet(prop => prop.Url).Returns(url);

            _mockRepo.Setup(repo => repo.GetLatestReleaseAsync()).ReturnsAsync(mockReleaseResponse.Object);

            var sut = new GitHubService(_mockRepo.Object);

            var actual = await sut.GetLatestReleaseAsync();

            _mockRepo.Verify(repo => repo.GetLatestReleaseAsync(), Times.Once);

            Assert.NotNull(actual);
            Assert.AreEqual(tag, actual.Tag);
            Assert.AreEqual(url, actual.Url);
        }

        [Test]
        public void GetExplicitReleaseAsync_ThrowsApiException()
        {
            _mockRepo.Setup(repo => repo.GetExplicitReleaseAsync(It.IsAny<string>()).Result).Throws(new ApiException());

            var sut = new GitHubService(_mockRepo.Object);

            var ex = Assert.ThrowsAsync<ApiException>(() => sut.GetExplicitReleaseAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task GetExplicitReleaseAsync_SuccessfullyRetrieveExplicitRelease()
        {
            var tag = Version.Parse("1.2.3.4");
            var url = new Uri("http://github.com");
            var mockReleaseResponse = new Mock<ReleaseInfo>();
            mockReleaseResponse.SetupGet(prop => prop.Tag).Returns(tag);
            mockReleaseResponse.SetupGet(prop => prop.Url).Returns(url);

            _mockRepo.Setup(repo => repo.GetExplicitReleaseAsync(It.IsAny<string>()).Result).Returns(mockReleaseResponse.Object);

            var sut = new GitHubService(_mockRepo.Object);

            var actual = await sut.GetExplicitReleaseAsync(tag.ToString());

            _mockRepo.Verify(repo => repo.GetExplicitReleaseAsync(It.IsAny<string>()), Times.Once);

            Assert.NotNull(actual);
            Assert.AreEqual(tag, actual.Tag);
            Assert.AreEqual(url, actual.Url);
        }

        [Test]
        public void DownloadReleaseAsync_ThrowsApiException_WhenTagOverloadIsUsed()
        {
            var tag = "1.2.3.4";
            _mockRepo.Setup(repo => repo.GetExplicitReleaseAsync(It.IsAny<string>())).Throws(new ApiException());

            var sut = new GitHubService(_mockRepo.Object);

            var ex = Assert.ThrowsAsync<ApiException>(() => sut.DownloadReleaseAsync(It.IsAny<DownloadableAssets>(), It.IsAny<string>(), tag));

            _mockRepo.Verify(repo => repo.GetExplicitReleaseAsync(tag));
            _mockRepo.Verify(repo => repo.DownloadReleaseAsync(It.IsAny<Uri>(), It.IsAny<Uri>()), Times.Never);
        }

        [Test]
        public void DownloadReleaseAsync_ThrowsApiException_WhenNullTagOverloadIsUsed()
        {
            _mockRepo.Setup(repo => repo.GetLatestReleaseAsync()).Throws(new ApiException());

            var sut = new GitHubService(_mockRepo.Object);

            var ex = Assert.ThrowsAsync<ApiException>(() => sut.DownloadReleaseAsync(It.IsAny<DownloadableAssets>(), It.IsAny<string>()));

            _mockRepo.Verify(repo => repo.GetLatestReleaseAsync());
            _mockRepo.Verify(repo => repo.DownloadReleaseAsync(It.IsAny<Uri>(), It.IsAny<Uri>()), Times.Never);
        }

        [Test]
        public void DownloadReleaseAsync_ThrowsArgumentException_WhenInvalidFilepathIsProvided()
        {
            var expectedFilepathErrorMessage = "downloadSaveLocation is invalid.";
            var expectedReleaseInfoUrlErrorMessage = "releaseInfo.Url is invalid.";
            var mockReleaseInfo = new Mock<ReleaseInfo>();
            mockReleaseInfo.SetupGet(prop => prop.Url).Returns(new Uri("https://github.com/"));

            _mockRepo.Setup(repo => repo.GetLatestReleaseAsync()).Throws(new ArgumentException());

            var sut = new GitHubService(_mockRepo.Object);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => sut.DownloadReleaseAsync(It.IsAny<DownloadableAssets>(), mockReleaseInfo.Object, string.Empty));

            _mockRepo.Verify(repo => repo.DownloadReleaseAsync(It.IsAny<Uri>(), It.IsAny<Uri>()), Times.Never);

            Assert.IsTrue(ex.Message.Contains(expectedFilepathErrorMessage));
            Assert.IsFalse(ex.Message.Contains(expectedReleaseInfoUrlErrorMessage));
        }

        [Test]
        public void DownloadReleaseAsync_ThrowsArgumentException_WhenInvalidReleaseInfoUrlIsProvided()
        {
            var expectedFilepathErrorMessage = "downloadSaveLocation is invalid.";
            var expectedReleaseInfoUrlErrorMessage = "releaseInfo.Url is invalid.";
            var filepath = "C:/";
            var mockReleaseInfo = new Mock<ReleaseInfo>();
            mockReleaseInfo.SetupGet(prop => prop.Url).Returns(new Uri("/github.com", UriKind.RelativeOrAbsolute));

            _mockRepo.Setup(repo => repo.GetLatestReleaseAsync()).Throws(new ArgumentException());

            var sut = new GitHubService(_mockRepo.Object);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => sut.DownloadReleaseAsync(It.IsAny<DownloadableAssets>(), mockReleaseInfo.Object, filepath));

            _mockRepo.Verify(repo => repo.DownloadReleaseAsync(It.IsAny<Uri>(), It.IsAny<Uri>()), Times.Never);

            Assert.IsFalse(ex.Message.Contains(expectedFilepathErrorMessage));
            Assert.IsTrue(ex.Message.Contains(expectedReleaseInfoUrlErrorMessage));
        }

        [Test]
        public void DownloadReleaseAsync_ThrowsArgumentException_WhenInvalidReleaseInfoUrlAndInvalidFilepathIsProvided()
        {
            var expectedFilepathErrorMessage = "downloadSaveLocation is invalid.";
            var expectedReleaseInfoUrlErrorMessage = "releaseInfo.Url is invalid.";
            var filepath = "http://10.0.0.1";
            var mockReleaseInfo = new Mock<ReleaseInfo>();
            mockReleaseInfo.SetupGet(prop => prop.Url).Returns(new Uri("/github.com", UriKind.RelativeOrAbsolute));

            _mockRepo.Setup(repo => repo.GetLatestReleaseAsync()).Throws(new ArgumentException());

            var sut = new GitHubService(_mockRepo.Object);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => sut.DownloadReleaseAsync(It.IsAny<DownloadableAssets>(), mockReleaseInfo.Object, filepath));

            _mockRepo.Verify(repo => repo.DownloadReleaseAsync(It.IsAny<Uri>(), It.IsAny<Uri>()), Times.Never);

            Assert.IsTrue(ex.Message.Contains(expectedFilepathErrorMessage));
            Assert.IsTrue(ex.Message.Contains(expectedReleaseInfoUrlErrorMessage));
        }

        // TODO Write tests for throwing new ArgumentException().

        [Test]
        public async Task DownloadReleaseAsync_UsingTagArgument_SuccessfullyDownloadAsset()
        {
            var tag = Version.Parse("1.2.3.4");
            var url = new Uri("http://github.com");
            var downloadSaveLocation = "C:/";
            var expected = true;
            var mockReleaseResponse = new Mock<ReleaseInfo>();
            mockReleaseResponse.SetupGet(prop => prop.Tag).Returns(tag);
            mockReleaseResponse.SetupGet(prop => prop.Url).Returns(url);

            _mockRepo.Setup(repo => repo.GetExplicitReleaseAsync(It.IsAny<string>())).ReturnsAsync(mockReleaseResponse.Object);
            _mockRepo.Setup(repo => repo.DownloadReleaseAsync(It.IsAny<Uri>(), It.IsAny<Uri>()).Result).Returns(expected);

            var sut = new GitHubService(_mockRepo.Object);

            var response = await sut.DownloadReleaseAsync(It.IsAny<DownloadableAssets>(), downloadSaveLocation, tag.ToString());

            _mockRepo.Verify(repo => repo.GetExplicitReleaseAsync(It.IsAny<string>()), Times.Once);
            _mockRepo.Verify(repo => repo.DownloadReleaseAsync(It.IsAny<Uri>(), It.IsAny<Uri>()), Times.Once);

            Assert.IsTrue(response);
        }

        
    }
}
