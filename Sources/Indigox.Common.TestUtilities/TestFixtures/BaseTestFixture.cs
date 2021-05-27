using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Indigox.TestUtility.Exceptions;

namespace Indigox.TestUtility.TestFixtures
{
    public class BaseTestFixture
    {
        private bool errorOnSetUp = false;

        [SetUp]
        protected void SafeSetUp()
        {
            try
            {
                this.errorOnSetUp = false;
                this.SetUp();
            }
            catch ( Exception ex )
            {
                this.errorOnSetUp = true;
                throw new TestFixtureException( "Error on SetUp.", ex );
            }
        }

        [TearDown]
        protected void SafeTearDown()
        {
            try
            {
                this.TearDown();
            }
            catch ( Exception ex )
            {
                if ( !this.errorOnSetUp )
                {
                    throw new TestFixtureException( "Error on TearDown.", ex );
                }
                else
                {
                    Console.Error.WriteLine( "Error on TearDown.\r\n" + ex.ToString() );
                }
            }
        }

        [TestFixtureSetUp]
        protected void SafeTestFixtureSetUp()
        {
            try
            {
                this.TestFixtureSetUp();
            }
            catch ( Exception ex )
            {
                throw new TestFixtureException( "Error on TestFixtureSetUp.", ex );
            }
        }

        [TestFixtureTearDown]
        protected void SafeTestFixtureTearDown()
        {
            try
            {
                this.TestFixtureTearDown();
            }
            catch ( Exception ex )
            {
                throw new TestFixtureException( "Error on TestFixtureTearDown.", ex );
            }
        }

        protected virtual void SetUp()
        {
            ITestFixtureProxy[] proxyInstances = GetProxyInstance();
            for ( int i = 0; i < proxyInstances.Length; i++ )
            {
                proxyInstances[ i ].OnSetUp();
            }
        }

        protected virtual void TearDown()
        {
            ITestFixtureProxy[] proxyInstances = GetProxyInstance();
            for ( int i = proxyInstances.Length - 1; i >= 0; i-- )
            {
                proxyInstances[ i ].OnTearDown();
            }
        }

        protected virtual void TestFixtureSetUp()
        {
            ITestFixtureProxy[] proxyInstances = GetProxyInstance();
            for ( int i = 0; i < proxyInstances.Length; i++ )
            {
                proxyInstances[ i ].OnTestFixtureSetUp();
            }
        }

        protected virtual void TestFixtureTearDown()
        {
            ITestFixtureProxy[] proxyInstances = GetProxyInstance();
            for ( int i = proxyInstances.Length - 1; i >= 0; i-- )
            {
                proxyInstances[ i ].OnTestFixtureTearDown();
            }
        }

        private ITestFixtureProxy[] GetProxyInstance()
        {
            if ( proxyInstances == null )
            {
                //string proxyTypeName = (string)TestContext.CurrentContext.Test.Properties[ TestFixtureProxy ];
                //Type proxyType = Type.GetType( proxyTypeName );

                object[] atts = this.GetType().GetCustomAttributes( typeof( TestFixtureProxyAttribute ), true );
                if ( atts.Length == 0 )
                {
                    Assert.Fail( "Not set test fixture proxy." );
                }

                Type[] proxyTypes = ( (TestFixtureProxyAttribute)atts[ 0 ] ).TestFixtureProxyType;
                List<ITestFixtureProxy> proxyInstanceList = new List<ITestFixtureProxy>();
                foreach ( Type proxyType in proxyTypes )
                {
                    ITestFixtureProxy proxyInstance = (ITestFixtureProxy)Activator.CreateInstance( proxyType );
                    proxyInstanceList.Add( proxyInstance );
                }
                proxyInstances = proxyInstanceList.ToArray();
            }
            return proxyInstances;
        }

        private ITestFixtureProxy[] proxyInstances;
    }
}
