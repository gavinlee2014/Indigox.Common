using System.Collections.Generic;
using NUnit.Framework;
using NUnitAssert = NUnit.Framework.Assert;

namespace Indigox.Common.Utilities.Test
{
    [TestFixture]
    public class CollectionUtilTest 
    {
        [Test]
        public void TestConvertToList()
        {
            int[] numbers = new int[] { 1, 2, 3 };
            List<object> objs = CollectionUtil.ConvertToList<object>(numbers);
            for (int i = 0; i < numbers.Length; i++ )
            {
                NUnitAssert.AreEqual(objs[i], (object)numbers[i]);
            }
        }

        [Test]
        public void TestConvertToArray()
        {
            int[] numbers = new int[] { 1, 2, 3 };
            object[] objs = CollectionUtil.ConvertToArray<object>(numbers);
            for (int i = 0; i < numbers.Length; i++)
            {
                NUnitAssert.AreEqual(objs[i], (object)numbers[i]);
            }
        }
    }
}
