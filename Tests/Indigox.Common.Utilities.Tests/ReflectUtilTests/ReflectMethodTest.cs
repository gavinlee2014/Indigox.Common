using System;
using System.Reflection;
using Indigox.Common.Utilities.Test.Assembly;
using NUnit.Framework;
using NUnitAssert = NUnit.Framework.Assert;

namespace Indigox.Common.Utilities.Test.ReflectUtilTests
{
    public class ReflectMethodTest : BaseTest
    {
        #region test get method

        [Test]
        public void TestGetMethod_StaticNoArgs()
        {
            MethodInfo method = ReflectUtil.GetMethod( typeof( MethodClass ), "StaticMethodNoArgs" );
            NUnitAssert.NotNull( method );
            method.Invoke( null, null );
        }

        [Test]
        public void TestGetMethod_StaticWithArgs()
        {
            Type[] argTypes = new Type[] { typeof( string ) };
            object[] args = new object[] { "test" };
            MethodInfo method = ReflectUtil.GetMethod( typeof( MethodClass ), "StaticMethodWithArgs", argTypes );
            NUnitAssert.NotNull( method );
            method.Invoke( null, args );
        }

        #endregion
    }
}
