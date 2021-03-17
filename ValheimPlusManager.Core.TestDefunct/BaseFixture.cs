using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;

namespace ValheimPlusManager.Core.Test
{
    [TestFixture]
    public class BaseFixture : IFixture
    {
        [Description("Base Test Fixture Class")]
        public virtual void SetUp()
        {
        }
    }
}
