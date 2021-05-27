using System;
using System.Threading;

namespace Indigox.Common.Data.Utils
{
    internal class Counter
    {
        private int num = 0;

        public Counter()
        {
        }

        public void Increment()
        {
            Interlocked.Increment( ref num );
        }

        public void Decrement()
        {
            if ( this.num == 0 )
            {
                throw new OverflowException( "Counter 必须大于或等于 0 。" );
            }
            Interlocked.Decrement( ref num );
        }

        public void Reset()
        {
            this.num = 0;
        }

        public int Value
        {
            get
            {
                return this.num;
            }
        }
    }
}