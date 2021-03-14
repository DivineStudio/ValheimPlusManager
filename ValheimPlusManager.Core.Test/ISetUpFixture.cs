using NUnit;
using NUnit.Framework;

namespace ValheimPlusManager.Core.Test
{
    public interface ISetUpFixture
    {
        [OneTimeSetUp]
        void OneTimeSetup();

        [OneTimeTearDown]
        void OneTimeTearDown();
    }
}
