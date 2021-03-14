using NUnit.Framework;

namespace ValheimPlusManager.Core.Test
{
    public interface IFixture
    {
        [SetUp]
        void SetUp();
    }
}
