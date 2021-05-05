using NUnit.Framework;

namespace ValheimPlusManager.Core.Test.IntegrationTests
{
    public interface ISetUpFixture
    {
        [OneTimeSetUp]
        void OneTimeSetup();

        [OneTimeTearDown]
        void OneTimeTearDown();
    }
}
