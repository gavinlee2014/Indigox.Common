using System;
using NUnit.Framework;

namespace Indigox.Common.Logging.Test
{
    [TestFixture]
    class LogFormatTest
    {
        [Test]
        public void TestErrorException()
        {
            try
            {
                ThrowException();
            }
            catch ( Exception e )
            {
                Log.Error( e.ToString() );
            }
        }

        private static void ThrowException()
        {
            Exception e = new NullReferenceException( "user is null" );
            throw e;
        }
    }
}
