using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Expression
{
    public struct Position
    {
        private int _charIndex;

        public Position( int charIndex )
        {
            _charIndex = charIndex;
        }

        public int CharIndex
        {
            get { return _charIndex; }
        }
    }
}
