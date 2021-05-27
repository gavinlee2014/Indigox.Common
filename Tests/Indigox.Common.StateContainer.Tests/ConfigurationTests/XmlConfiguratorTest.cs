using System;
using System.Collections.Generic;
using Indigox.Common.StateContainer.Configuration;
using Indigox.Common.StateContainer.CurrentUserProviders;
using NUnit.Framework;

namespace Indigox.Common.StateContainer.Test.ConfigurationTests
{
    [TestFixture]
    class XmlConfiguratorTest
    {
        [Test]
        public void TestConfigureFromXmlFile()
        {
            StateContext.Current.BeginApplication();
            StateContext.Current.BeginSession( new MutableCurrentUserProvider() );
            StateContext.Current.BeginTransaction();
            StateContext.Current.EndTransaction();
            StateContext.Current.EndSession();
            StateContext.Current.EndApplication();
        }
    }
}
