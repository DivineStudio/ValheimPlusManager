using NUnit.Framework;

namespace ValheimPlusManager.Core.Test.IntegrationTests
{
    public interface IFixture
    {
        [SetUp]
        void SetUp();
    }
}
