using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnitAssert = NUnit.Framework.Assert;

namespace Indigox.Common.Utilities.Test.ReflectUtilTests
{
    public class ReflectTypeTest : BaseTest
    {
        [Test]
        public void TestGetType_Internal()
        {
            string typeName = "Indigox.Common.Utilities.Test.Assembly.InternalClass, Indigox.Common.Utilities.Tests.Assembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            Type type = ReflectUtil.GetType( typeName );
            NUnitAssert.NotNull( type );
            NUnitAssert.AreEqual( typeName, type.AssemblyQualifiedName );
        }

        [Test]
        public void TestGetType_Public()
        {
            string typeName = "Indigox.Common.Utilities.Test.Assembly.PublicClass, Indigox.Common.Utilities.Tests.Assembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            Type type = ReflectUtil.GetType( typeName );
            NUnitAssert.NotNull( type );
            NUnitAssert.AreEqual( typeName, type.AssemblyQualifiedName );
        }
    }
}
