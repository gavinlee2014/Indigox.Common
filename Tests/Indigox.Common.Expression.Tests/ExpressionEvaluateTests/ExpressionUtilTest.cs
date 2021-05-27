using NUnit.Framework;

namespace Indigox.Common.Expression.Test.ExpressionEvaluateTests
{
    [TestFixture]
    public class ExpressionUtilTest : TestBase
    {
        static object[] TestReplaceCaseSource = 
        {
            new object[] { "${Name}", contextObject.Name, false },
            new object[] { "${test::concat(Name, '......' )}", contextObject.Name + "......", false },
            new object[] { "${Name} is ${Age}", contextObject.Name + " is " + contextObject.Age, false },
            new object[] { "${ObjectDictionary.name}", contextObject.ObjectDictionary["name"], false },
            new object[] { "${ObjectDictionary.age}", contextObject.ObjectDictionary["age"].ToString(), false },
            new object[] { "${ObjectDictionary.none}", null, true },
            new object[] { "${NullObjectDictionary}", "", false },
            new object[] { "${NullObjectDictionary.none}", null, true },
            new object[] { "${StringDictionary.name}", contextObject.StringDictionary["name"], false },
            new object[] { "${StringDictionary.none}", null, true },
            new object[] { "${NullStringDictionary}", "", false },
            new object[] { "${NullStringDictionary.none}", null, true }
        };
        [Test]
        [TestCaseSource("TestReplaceCaseSource")]
        public void TestReplace(string plainText, string expectedText, bool throwException)
        {
            ExpressionUtil util = new ExpressionUtil(context);
            string replacedText = null;
            if (throwException)
            {
                Assert.Catch(delegate()
                {
                    replacedText = util.Replace(plainText);
                });
            }
            else
            {
                replacedText = util.Replace(plainText);
                Assert.AreEqual(expectedText, replacedText);
            }
        }
        [Test]
        [TestCaseSource("TestReplaceCaseSource")]
        public void TestTryReplace(string plainText, string expectedText, bool throwException)
        {
            ExpressionUtil util = new ExpressionUtil(context);
            string replacedText = null;
            bool succeed = util.TryReplace(plainText, ref replacedText);
            Assert.AreEqual(!throwException, succeed);
            Assert.AreEqual(expectedText, replacedText);
        }

        static object[] TestEvaluateCaseSource = 
        {
            new object[] { "${Name}", contextObject.Name, false },
            new object[] { "${Age}", contextObject.Age, false },
            new object[] { "${test::concat(Name, '......' )}", contextObject.Name + "......", false },
            new object[] { "${NullChild.Name}", null, true },
            new object[] { "${Name} is ${Age}", contextObject.Name + " is " + contextObject.Age, false },
            new object[] { "${ObjectDictionary.name}", contextObject.ObjectDictionary["name"], false },
            new object[] { "${ObjectDictionary.age}", contextObject.ObjectDictionary["age"], false },
            new object[] { "${ObjectDictionary.none}", null, true },
            new object[] { "${NullObjectDictionary}", null, false },
            new object[] { "${NullObjectDictionary.none}", null, true },
            new object[] { "${StringDictionary.name}", contextObject.StringDictionary["name"], false },
            new object[] { "${StringDictionary.none}", null, true },
            new object[] { "${NullStringDictionary}", null, false },
            new object[] { "${NullStringDictionary.none}", null, true },
            new object[] { "${Age>18}", true, false },
            new object[] { "${not(Age>18)}", false, false },
            new object[] { "${Assets>100}", true, false }
        };
        [Test]
        [TestCaseSource("TestEvaluateCaseSource")]
        public void TestEvaluate(string plainText, object expectedValue, bool throwException)
        {
            ExpressionUtil util = new ExpressionUtil(context);
            object evaluatedValue = null;
            if (throwException)
            {
                Assert.Catch(delegate()
                {
                    evaluatedValue = util.Evaluate(plainText);
                });
            }
            else
            {
                evaluatedValue = util.Evaluate(plainText);
                Assert.AreEqual(expectedValue, evaluatedValue);
            }
        }
        [Test]
        [TestCaseSource("TestEvaluateCaseSource")]
        public void TestTryEvaluate(string plainText, object expectedValue, bool throwException)
        {
            ExpressionUtil util = new ExpressionUtil(context);
            object evaluatedValue = null;
            bool succeed = util.TryEvaluate(plainText, ref evaluatedValue);
            Assert.AreEqual(!throwException, succeed);
            Assert.AreEqual(expectedValue, evaluatedValue);
        }
    }
}
