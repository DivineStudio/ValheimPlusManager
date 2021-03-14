using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;

namespace ValheimPlusManager.Core.Test
{
    [TestFixture]
    public abstract class BaseFixture : IFixture
    {
        [Description("Base Test Fixture Class")]
        public abstract void SetUp();
    }
}
