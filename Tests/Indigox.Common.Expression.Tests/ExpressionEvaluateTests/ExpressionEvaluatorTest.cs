using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Expression.Test.TestClasses;
using NUnit.Framework;

namespace Indigox.Common.Expression.Test.ExpressionEvaluateTests
{
    [TestFixture]
    public class ExpressionEvaluatorTest : TestBase
    {
        static string[] Expressions = new string[]{
            "Name",
            "Child.Name",
            "1+1",
            "'my name is '+Name",
            "1==1",
            "'1'==1"
        };
        [Test]
        [TestCaseSource( "Expressions" )]
        public void TestCheckSyntax( string input )
        {
            ExpressionEvaluator evaluator = new ExpressionEvaluator( context );
            evaluator.CheckSyntax( input );
        }

        static string[] FunctionExpressions = new string[]{
            "test::concat('hello, ', test::concat(Name, '.'))",
            "test::concat(FirstName, test::concat(' ', LastName))"
        };
        [Test]
        [TestCaseSource( "FunctionExpressions" )]
        public void TestCheckSyntaxWithFunction( string input )
        {
            ExpressionEvaluator evaluator = new ExpressionEvaluator( context );
            evaluator.CheckSyntax( input );
        }

        static object[] ExpressionsAndExpected = { 
            new object[] { "Name", contextObject.Name, false },
            new object[] { "Child.Name", contextObject.Child.Name, false },
            new object[] { "ObjectDictionary.name", contextObject.ObjectDictionary["name"], false },
            new object[] { "ObjectDictionary.age", contextObject.ObjectDictionary["age"], false },
            new object[] { "ObjectDictionary.none", null, true },
            new object[] { "NullObjectDictionary", null, false },
            new object[] { "NullObjectDictionary.none", null, true },
            new object[] { "StringDictionary.name", contextObject.StringDictionary["name"], false },
            new object[] { "StringDictionary.none", null, true },
            new object[] { "NullStringDictionary", null, false },
            new object[] { "NullStringDictionary.none", null, true },
            new object[] { "'my name is ' + Name", "my name is " + contextObject.Name, false },
            new object[] { "1+1", 2, false },
            new object[] { "1==1", true, false },
            new object[] { "Age>18", true, false },
            // compare different types, string vs int
            new object[] { "'1'==1", true, false },
            new object[] { "'2'!=1", true, false },
            new object[] { "'1'<2", true, false },
            new object[] { "'1'<=1", true, false },
            new object[] { "'1'>=1", true, false },
            new object[] { "'2'>1", true, false },
            new object[] { "'x'>1", null, true },
            // compare different types, int vs string
            new object[] { "1=='1'", true, false },
            new object[] { "2!='1'", true, false },
            new object[] { "1<'2'", true, false },
            new object[] { "1<='1'", true, false },
            new object[] { "1>='1'", true, false },
            new object[] { "2>'1'", true, false },
            new object[] { "2>'x'", null, true }
        };
        [Test]
        [TestCaseSource( "ExpressionsAndExpected" )]
        public void TestEvaluate( string input, object expected, bool throwException )
        {
            ExpressionEvaluator evaluator = new ExpressionEvaluator( context );
            object actual = null;
            if ( throwException )
            {
                Assert.Catch( delegate()
                {
                    actual = evaluator.Evaluate( input );
                } );
            }
            else
            {
                actual = evaluator.Evaluate( input );
                Assert.AreEqual( expected, actual );
            }
        }

        static object[] FunctionExpressionsAndExpected = { 
            new object[] { "test::concat('hello, ', test::concat(Name, '.'))", 
                           "hello, " + contextObject.Name + "." },
            new object[] { "test::concat(FirstName, test::concat(' ', LastName))", 
                           contextObject.FirstName + " " + contextObject.LastName },
            new object[] { "test::encount(test::encount(3))", 
                           6 }
        };
        [Test]
        [TestCaseSource( "FunctionExpressionsAndExpected" )]
        public void TestEvaluateWithFunction( string input, object expected )
        {
            ExpressionEvaluator evaluator = new ExpressionEvaluator( context );
            object actual = evaluator.Evaluate( input );
            Assert.AreEqual( expected, actual );
        }
    }
}
