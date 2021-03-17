using System;
using System.Collections.Generic;
using System.Text;
using ValheimPlusManager.Core.Services;
using ValheimPlusManager.Core.Repositories;
using ValheimPlusManager.Core.Factories;
using NUnit;
using NUnit.Framework;
using System.IO;
using MvvmCross.Tests;
using MvvmCross;

namespace ValheimPlusManager.Core.Test.IntegrationTests
{
    [TestFixture]
    public class FileInformationServiceTests : BaseFixture
    {
        private string _parentDirectory;
        private Uri _testExeFilePath;

        public FileInformationServiceTests()
        {
            _parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            _testExeFilePath = new Uri($@"{_parentDirectory}\TestExe\Test.exe");
        }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            base.Ioc.RegisterSingleton<IFileInformationRepository>(RepositoryFactory.Create<IFileInformationRepository>());
            base.Ioc.RegisterSingleton<IFileInformationService>(ServiceFactory.Create<IFileInformationService>());
        }

        [Test]
        public void GetProductVersion_SuccessfulReturn()
        {
            base.ClearAll();

            var expected = "1.2.3.4567";
            var actual = Ioc.Resolve<IFileInformationService>().GetProductVersion(_testExeFilePath).ToString();

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void GetProductVersion_IncorrectlyProvidedUriForFilepath_ReturnsNull()
        {
            base.ClearAll();

            var actual = Ioc.Resolve<IFileInformationService>().GetProductVersion(new Uri("http://10.10.10.1"));

            Assert.IsNull(actual);
        }
    }
}
