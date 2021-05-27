using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Expression.Test.TestClasses
{
    public class TestFunctions
    {
        public static string concat( object str1, object str2 )
        {
            return string.Concat( str1, str2 );
        }
    }

    public class TestFunctionInstance
    {
        private int seed = 0;

        public int encount( int seed )
        {
            this.seed += seed;
            return this.seed;
        }
    }
}
