using System;
using ValheimPlusManager.Core.Services;
using ValheimPlusManager.Core.Repositories;
using ValheimPlusManager.Core.Factories;
using NUnit.Framework;
using System.IO;

namespace ValheimPlusManager.Core.Test.IntegrationTests
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "ExcludeTestMethodNames")]
    public class FileInformationServiceTests : BaseFixture
    {
        private readonly string _solutionDirectory;
        private readonly Uri _testExeFilePath;

        public FileInformationServiceTests()
        {
            _solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            _testExeFilePath = new Uri($@"{_solutionDirectory}\TestExe\Test.exe");
        }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            base.Ioc.RegisterSingleton<IFileInformationRepository>(RepositoryFactory.Create<IFileInformationRepository>());
            base.Ioc.RegisterSingleton<IFileInformationService>(ServiceFactory.Create<IFileInformationService>());
        }

        [Test]
        public void IsLoggerCreated()
        {
            var sut = base.Ioc.Resolve<IFileInformationService>();
            Assert.NotNull(sut.IsLoggerCreated);
        }

        [Test]
        public void GetProductVersion_SuccessfulReturn()
        {
            base.ClearAll();

            var expected = "1.2.3.4567";
            var actual = Ioc.Resolve<IFileInformationService>().GetProductVersion(_testExeFilePath.ToString()).ToString();

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void GetProductVersion_IncorrectlyProvidedUriForFilepath_ReturnsNull()
        {
            base.ClearAll();

            var actual = Ioc.Resolve<IFileInformationService>().GetProductVersion("http://10.10.10.1");

            Assert.IsNull(actual);
        }
    }
}
