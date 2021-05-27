using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.TestUtility.Exceptions
{
    class TestFixtureException : System.Exception
    {
        public TestFixtureException( string message ) :
            base( message )
        {
        }

        public TestFixtureException( string message, System.Exception innerEx ) :
            base( message, innerEx )
        {
        }
    }
}
