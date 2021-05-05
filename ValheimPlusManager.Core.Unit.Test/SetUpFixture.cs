using NUnit.Framework;
using MvvmCross.Tests;
using Serilog;

namespace ValheimPlusManager.Core.Unit.Test
{
    [SetUpFixture]
    public class SetUpFixture : MvxIoCSupportingTest, ISetUpFixture
    {
        [OneTimeSetUp]
        public virtual void OneTimeSetup()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
        }
    }
}
