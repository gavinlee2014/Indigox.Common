using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Indigox.Common.Expression.Test.TestClasses
{
    public class TestParentClass
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public double Assets { get; set; }
        public Gender Gender { get; set; }
        public TestChildClass Child { get; set; }
        public TestChildClass NullChild { get; set; }
        public Dictionary<string, object> ObjectDictionary { get; set; }
        public Dictionary<string, object> NullObjectDictionary { get; set; }
        public Dictionary<string, string> StringDictionary { get; set; }
        public Dictionary<string, string> NullStringDictionary { get; set; }
        public Hashtable Hashtable { get; set; }
        public Hashtable NullHashtable { get; set; }
    }

    public class TestChildClass
    {
        public string Name { get; set; }
    }
}
