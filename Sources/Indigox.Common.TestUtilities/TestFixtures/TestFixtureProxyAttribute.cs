using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.TestUtility.TestFixtures
{
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = true )]
    public class TestFixtureProxyAttribute : Attribute
    {
        public TestFixtureProxyAttribute( params Type[] testFixtureProxyType )
        {
            this.TestFixtureProxyType = testFixtureProxyType;
        }

        public Type[] TestFixtureProxyType
        {
            get;
            set;
        }
    }
}
