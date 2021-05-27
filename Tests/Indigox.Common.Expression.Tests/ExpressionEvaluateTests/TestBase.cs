using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Expression.Test.TestClasses;
using NUnit.Framework;

namespace Indigox.Common.Expression.Test.ExpressionEvaluateTests
{
    public class TestBase
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.context = new ObjectExpressionContext(contextObject);
            this.context.SetFunctions("test", typeof(TestFunctions));
            this.context.SetFunctions("test", new TestFunctionInstance());
        }

        static TestBase()
        {
            contextObject = new TestParentClass()
            {
                Name = "john smith",
                FirstName = "john",
                LastName = "smith",
                Age = 28,
                Assets = 1999.98,
                Gender = Gender.Male,
                Child = new TestChildClass()
                {
                    Name = "child"
                },
                NullChild = null,
                ObjectDictionary = new Dictionary<string, object>(),
                NullObjectDictionary = null,
                StringDictionary = new Dictionary<string, string>(),
                NullStringDictionary = null
            };
            contextObject.ObjectDictionary.Add("name", "simth");
            contextObject.ObjectDictionary.Add("age", 28);
            contextObject.StringDictionary.Add("name", "simth");
        }

        protected static readonly TestParentClass contextObject;

        protected ObjectExpressionContext context;
    }
}
