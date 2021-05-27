using System;
using System.Reflection;
using Indigox.Common.Utilities.Test.Assembly;
using NUnit.Framework;
using NUnitAssert = NUnit.Framework.Assert;

namespace Indigox.Common.Utilities.Test.ReflectUtilTests
{
    public class InvokeMethodTest : BaseTest
    {
        [Test]
        public void TestInvokeMethod_NoArgs()
        {
            MethodClass instance = new MethodClass();
            Type[] argTypes = new Type[] { };
            object[] args = new object[] { };
            object ret = ReflectUtil.InvokeMethod( instance, "MethodNoArgs", argTypes, args );
        }

        [Test]
        public void TestInvokeMethod_WithArgs()
        {
            MethodClass instance = new MethodClass();
            Type[] argTypes = new Type[] { typeof( string ) };
            object[] args = new object[] { "test" };
            object ret = ReflectUtil.InvokeMethod( instance, "MethodWithArgs", argTypes, args );
        }

        [Test]
        public void TestInvokeMethod_ReturnValue()
        {
            MethodClass instance = new MethodClass();
            Type[] argTypes = new Type[] { typeof( int ), typeof( int ) };
            object[] args = new object[] { 10, 20 };
            object ret = ReflectUtil.InvokeMethod( instance, "MethodWithReturnValue", argTypes, args );
            NUnitAssert.AreEqual( 30, ret );
            NUnitAssert.AreEqual( typeof( int ), ret.GetType() );
        }

        [Test]
        public void TestInvokeMethod_StaticNoArgs()
        {
            Type[] argTypes = new Type[] { };
            object[] args = new object[] { };
            object ret = ReflectUtil.InvokeStaticMethod( typeof( MethodClass ), "StaticMethodNoArgs", argTypes, args );
        }

        [Test]
        public void TestInvokeMethod_StaticWithArgs()
        {
            Type[] argTypes = new Type[] { typeof( string ) };
            object[] args = new object[] { "test" };
            object ret = ReflectUtil.InvokeStaticMethod( typeof( MethodClass ), "StaticMethodWithArgs", argTypes, args );
        }
    }
}
