using System;
using System.Collections.Generic;
using System.Text;
using ValheimPlusManager.Core.Services;
using ValheimPlusManager.Core.Factories;
using NUnit;
using NUnit.Framework;
using System.IO;

namespace ValheimPlusManager.Core.Test.UnitTests
{
    [TestFixture]
    public class FileInformationServiceTests : BaseFixture
    {
        private IFileInformationService _fileInformationService;
        private readonly string _parentDirectory;
        private readonly Uri _testExeFilePath;

        public FileInformationServiceTests()
        {
            _fileInformationService = ServiceFactory.Create<IFileInformationService>();
            _parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            _testExeFilePath = new Uri($@"{_parentDirectory}\TestExe\Test.exe");
        }

        [Test]
        public void GetProductVersion_SuccessfulReturn()
        {
            var expected = "1.2.3.4567";
            string actual;

            actual = _fileInformationService.GetProductVersion(_testExeFilePath).ToString();

            Assert.AreEqual(actual, expected);
        }
    }
}
