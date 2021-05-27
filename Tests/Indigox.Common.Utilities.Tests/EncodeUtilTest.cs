using NUnit.Framework;
using NUnitAssert = NUnit.Framework.Assert;

namespace Indigox.Common.Utilities.Test
{
    [TestFixture]
    [Category( "Utilities" )]
    [Category( "Encode" )]
    public class EncodeUtilTest
    {
        [Test]
        public void TestUrlDecodeWithChinese ()
        {
            string url = "http://www.google.com/q=%E4%B8%AD%E5%9B%BD&l=北京";
            //string actual = EncodeUtil.UrlDecode( url );
            string expected = "http://www.google.com/q=中国&l=北京";
            //Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestUrlDecode ()
        {
            string url = "http://www.google.com/q=%E4%B8%AD%E5%9B%BD";
            //string actual = EncodeUtil.UrlDecode( url );
            string expected = "http://www.google.com/q=中国";
            //Assert.AreEqual( expected, actual );
        }

        [Test]
        public void TestUrlEncode ()
        {
            string url = "中国";
            //string actual = EncodeUtil.UrlEncode( url );
            string expected = "%e4%b8%ad%e5%9b%bd";
            //Assert.AreEqual( expected, actual );
        }

    }
}
