using System;
using System.Reflection;
using Indigox.Common.Utilities.Test.Assembly;
using NUnit.Framework;
using NUnitAssert = NUnit.Framework.Assert;

namespace Indigox.Common.Utilities.Test.ReflectUtilTests
{
    public class ReflectPropertyTest : BaseTest
    {
        [Test]
        public void TestGetProperty_Instance()
        {
            PropertyClass instance = new PropertyClass();
            string property = "John";
            instance.property = property;

            object ret = ReflectUtil.GetProperty( instance, "PropertyGetterAndSetter" );
            NUnitAssert.AreEqual( property, ret );

            ret = ReflectUtil.GetProperty( instance, "PropertyGetterAndPrivateSetter" );
            NUnitAssert.AreEqual( property, ret );

            ret = ReflectUtil.GetProperty( instance, "PropertyGetter" );
            NUnitAssert.AreEqual( property, ret );
        }

        [Test]
        public void TestSetProperty_Instance()
        {
            PropertyClass instance = new PropertyClass();
            string property = "John";
            string changedProperty = "John Smith";

            instance.property = property;
            ReflectUtil.SetProperty( instance, "PropertyGetterAndSetter", changedProperty );
            NUnitAssert.AreEqual( changedProperty, instance.property );

            instance.property = property;
            ReflectUtil.SetProperty( instance, "PropertyGetterAndPrivateSetter", changedProperty );
            NUnitAssert.AreEqual( changedProperty, instance.property );
        }

        [Test]
        public void TestGetProperty_Static()
        {
            Type type = typeof( PropertyClass );
            string property = "John";
            PropertyClass.staticProperty = property;

            object ret = ReflectUtil.GetStaticProperty( type, "StaticPropertyGetterAndSetter" );
            NUnitAssert.AreEqual( property, PropertyClass.staticProperty );
        }

        [Test]
        public void TestSetProperty_Static()
        {
            Type type = typeof( PropertyClass );
            string property = "John";
            string changedProperty = "John Smith";

            PropertyClass.staticProperty = property;
            ReflectUtil.SetStaticProperty( type, "StaticPropertyGetterAndSetter", changedProperty );
            NUnitAssert.AreEqual( changedProperty, PropertyClass.staticProperty );
        }

    }
}
