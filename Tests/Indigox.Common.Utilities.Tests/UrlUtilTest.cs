using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using NUnit.Framework;
using NUnitAssert = NUnit.Framework.Assert;

namespace Indigox.Common.Utilities.Test
{
    [TestFixture]
    public class UrlUtilTest
    {
        [Test]
        [TestCase( "page.htm" )]
        [TestCase( "page.htm?name=admin&id=100" )]
        [TestCase( "page.htm?name=admin&nocache&id=100" )]
        [TestCase( "page.htm?name=admin&id=100&nocache" )]
        [TestCase( "page.htm?name=admin&id=100&empty=" )]
        [TestCase( "page.htm?name=admin&empty=&id=100" )]
        public void TestGetQuery( string url )
        {
            NameValueCollection query = UrlUtil.GetQuery( url );
            foreach ( string key in query.Keys )
            {
                Console.WriteLine( "{0} = {1}", key, query[ key ] );
            }
        }

        [Test]
        [TestCase( "page.htm?name=admin&empty=&id=100", "id", "page.htm?name=admin&empty=" )]
        [TestCase( "page.htm?name=admin&empty=&id=100", "xxx", "page.htm?name=admin&empty=&id=100" )]
        public void TestRemoveQuery( string url, string name, string expectedResultUrl )
        {
            string actualResultUrl = UrlUtil.RemoveQuery( url, name );
            NUnitAssert.AreEqual( expectedResultUrl, actualResultUrl );
        }

        [Test]
        [TestCase( "page.htm?name=admin&empty=&id=100", "id", "999", "page.htm?name=admin&empty=&id=999" )]
        [TestCase( "page.htm?name=admin&empty=&id=100", "xxx", "222", "page.htm?name=admin&empty=&id=100&xxx=222" )]
        public void TestSetQuery( string url, string name, string value, string expectedResultUrl )
        {
            string actualResultUrl = UrlUtil.SetQuery( url, name, value );
            NUnitAssert.AreEqual( expectedResultUrl, actualResultUrl );
        }
    }
}
