using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.StateContainer.States;
using NUnit.Framework;

namespace Indigox.Common.StateContainer.Test
{
    public class TestBase
    {
        [SetUp]
        protected void NUnitTestSetUp()
        {
            InitStateContainer();
        }

        [TearDown]
        protected void NUnitTestTearDown()
        {
            DisposeStateContainer();
        }

        private void InitStateContainer()
        {
            StateContext.Current.BeginApplication();
            StateContext.Current.BeginSession( SessionFactory.CreateCurrentUserProvider() );
            StateContext.Current.BeginTransaction();
        }

        private void DisposeStateContainer()
        {
            StateContext.Current.EndApplication();
            StateContext.Current.EndSession();
            StateContext.Current.EndTransaction();
        }
    }
}
