using System;
using System.Reflection;
using Indigox.Common.Utilities.Test.Assembly;
using NUnit.Framework;
using NUnitAssert = NUnit.Framework.Assert;

namespace Indigox.Common.Utilities.Test.ReflectUtilTests
{
    public class CreateInstanceTest : BaseTest
    {
        [Test]
        public void TestCreateInstance_PublicConstructor()
        {
            string typeName = "Indigox.Common.Utilities.Test.Assembly.ClassWithPublicConstructor, Indigox.Common.Utilities.Tests.Assembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            object obj = ReflectUtil.CreateInstance( typeName, new object[] { } );
            NUnitAssert.NotNull( obj );
        }

        [Test]
        public void TestCreateInstance_NonPublicConstructor()
        {
            string typeName = "Indigox.Common.Utilities.Test.Assembly.ClassWithNonPublicConstructor, Indigox.Common.Utilities.Tests.Assembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            object obj = ReflectUtil.CreateInstance( typeName, new object[] { }, true );
            NUnitAssert.NotNull( obj );
        }

        [Test]
        public void TestCreateInstance_HasStaticConstructor()
        {
            string typeName = "Indigox.Common.Utilities.Test.Assembly.ClassHasStaticConstructor, Indigox.Common.Utilities.Tests.Assembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            object obj = ReflectUtil.CreateInstance( typeName, new object[] { }, true );
            NUnitAssert.NotNull( obj );
        }

        [Test]
        public void TestCreateInstance_ArgumentConstructor()
        {
            string typeName = "Indigox.Common.Utilities.Test.Assembly.ClassWithArgumentConstructor, Indigox.Common.Utilities.Tests.Assembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            object[] args = new object[] { "name", 10 };
            ClassWithArgumentConstructor obj = ReflectUtil.CreateInstance( typeName, args ) as ClassWithArgumentConstructor;
            NUnitAssert.NotNull( obj );
            NUnitAssert.AreEqual( ClassWithArgumentConstructor.ConstructorType.WithArgument_String_Int, obj.CreatedBy );

            args = new object[] { new object(), 10 };
            obj = ReflectUtil.CreateInstance( typeName, args ) as ClassWithArgumentConstructor;
            NUnitAssert.NotNull( obj );
            NUnitAssert.AreEqual( ClassWithArgumentConstructor.ConstructorType.WithArgument_Object_Int, obj.CreatedBy );

            args = new object[] { new object() };
            obj = ReflectUtil.CreateInstance( typeName, args ) as ClassWithArgumentConstructor;
            NUnitAssert.NotNull( obj );
            NUnitAssert.AreEqual( ClassWithArgumentConstructor.ConstructorType.WithArgument_Object, obj.CreatedBy );
        }
    }
}
