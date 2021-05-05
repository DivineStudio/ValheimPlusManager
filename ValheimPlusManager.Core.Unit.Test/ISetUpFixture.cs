using NUnit.Framework;

namespace ValheimPlusManager.Core.Unit.Test
{
    public interface ISetUpFixture
    {
        [OneTimeSetUp]
        void OneTimeSetup();

        [OneTimeTearDown]
        void OneTimeTearDown();
    }
}
