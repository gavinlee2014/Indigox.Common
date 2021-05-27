using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.TestUtility.TestFixtures
{
    public class BaseTestFixtureProxy : ITestFixtureProxy
    {
        public virtual void OnSetUp()
        {
        }

        public virtual void OnTearDown()
        {
        }

        public virtual void OnTestFixtureSetUp()
        {
        }

        public virtual void OnTestFixtureTearDown()
        {
        }
    }
}
