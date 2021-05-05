using System;
using ValheimPlusManager.Core.Services;
using ValheimPlusManager.Core.Repositories;
using NUnit.Framework;
using Moq;
using Serilog;
using ValheimPlusManager.Core.Exceptions;

namespace ValheimPlusManager.Core.Unit.Test
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "ExcludeTestMethodNames")]
    public class FileInformationServiceUnitTests
    {
        private Mock<ILogger> _mockLogger;
        private Mock<IFileInformationRepository> _mockRepo;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _mockRepo = new Mock<IFileInformationRepository>();
        }

        [Test]
        public void GetProductVersion_IncorrectlyProvidedUriForFilepath_ReturnsNull()
        {
            var testFilepath = @"http://10.0.0.1";

            var sut = new FileInformationService(_mockRepo.Object);
            _mockRepo.Setup(repo => repo.GetProductVersion(It.IsAny<Uri>())).Throws(new ArgumentException());

            Assert.Throws<ArgumentException>(() => sut.GetProductVersion(testFilepath));

            _mockRepo.Verify(repo => repo.GetProductVersion(It.IsAny<Uri>()), Times.Never);
        }

        [Test]
        public void GetProductVersion_SuccessfulReturn()
        {
            var expected = "1.2.3.4567";
            var testFilepath = @"C://Test.exe";

            var sut = new FileInformationService(_mockRepo.Object);
            _mockRepo.Setup(repo => repo.GetProductVersion(It.IsAny<Uri>())).Returns(expected);

            var actual = sut.GetProductVersion(testFilepath)?.ToString();

            _mockRepo.Verify(repo => repo.GetProductVersion(It.IsAny<Uri>()), Times.AtMostOnce);

            Assert.AreEqual(actual, expected);
        }
    }
}
