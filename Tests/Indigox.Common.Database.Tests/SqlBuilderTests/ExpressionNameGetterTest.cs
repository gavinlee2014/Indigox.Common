using System;
using Indigox.Common.Data.SqlBuilder;
using NUnit.Framework;

namespace Indigox.Common.Database.Tests.SqlBuilderTests
{
    [TestFixture]
    public class ExpressionNameGetterTest
    {
        [Test]
        [TestCase( "t1.name", new string[] { "t1.name" } )]
        [TestCase( "  t1.name  ", new string[] { "t1.name" } )]
        [TestCase( "t1.name as username", new string[] { "t1.name", "as", "username" } )]
        [TestCase( "t1.name    username", new string[] { "t1.name", "username" } )]
        [TestCase( "t1.name+'xxxxx' username", new string[] { "t1.name", "+", "'xxxxx'", "username" } )]
        [TestCase( "newid() as id", new string[] { "newid", "()", "as", "id" } )]
        [TestCase( "(amount * 6.7) as localAmount", new string[] { "(amount * 6.7)", "as", "localAmount" } )]
        [TestCase( "[localAmount] as [bb]", new string[] { "[localAmount]", "as", "[bb]" } )]
        [TestCase( "case when (amount>=100) then [amount] else 100 end as [bb]", new string[] { "case", "when", "(amount>=100)", "then", "[amount]", "else", "100", "end", "as", "[bb]" } )]
        [TestCase( "case when amount>1000 then '1000+' when amount>=500 then '500+' else '...' end [bb]", new string[] { "case", "when", "amount", ">", "1000", "then", "'1000+'", "when", "amount", ">=", "500", "then", "'500+'", "else", "'...'", "end", "[bb]" } )]
        public void TestParseTokens( string input, string[] expected )
        {
            string[] actual = SqlParser.ParseTokens( input );
            CollectionAssert.AreEqual( expected, actual );
        }

        [TestCase( "(1*10/100+9() as name" )]
        public void TestParseErrorSql( string input )
        {
            ApplicationException ex = Assert.Catch<ApplicationException>( () =>
            {
                string[] actual = SqlParser.ParseTokens( input );
            } );
            Console.WriteLine( ex.ToString() );
        }

        //public void TestGetName( string input, string expected )
        //{
        //    string actual = SqlParser.GetName( input );
        //    Assert.AreEqual( expected, actual );
        //}
    }
}