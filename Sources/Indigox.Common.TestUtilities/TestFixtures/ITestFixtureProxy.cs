using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.TestUtility.TestFixtures
{
    public interface ITestFixtureProxy
    {
        void OnSetUp();
        void OnTearDown();
        void OnTestFixtureSetUp();
        void OnTestFixtureTearDown();
    }
}
