using MvvmCross.Tests;
using NUnit.Framework;

namespace ValheimPlusManager.Core.Test.IntegrationTests
{
    /// <summary>
    /// The base fixture for testing. Be sure to override AdditionalSetup() from <see cref="MvxIoCSupportingTest"/> for setting IoC.
    /// </summary>
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "ExcludeTestMethodNames")]
    public abstract class BaseFixture : MvxIoCSupportingTest, IFixture
    {
        public BaseFixture()
        {
            base.ClearAll();
        }

        public virtual void SetUp()
        {
        }
    }
}
