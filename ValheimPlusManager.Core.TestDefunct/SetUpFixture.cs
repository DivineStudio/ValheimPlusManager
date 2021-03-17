using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;

namespace ValheimPlusManager.Core.Test
{
    [SetUpFixture]
    public class SetUpFixture : ISetUpFixture
    {
        [Description("Base One Time Setup Test Fixture Class")]
        public virtual void OneTimeSetup()
        {
        }

        public virtual void OneTimeTearDown()
        {
        }
    }
}
