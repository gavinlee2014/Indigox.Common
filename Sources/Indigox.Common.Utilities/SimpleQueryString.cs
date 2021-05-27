using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Indigox.Common.Utilities
{
    class SimpleQueryString : NameValueCollection
    {

        public override string ToString()
        {
            return QueryStringParser.GetQueryString( this );
        }
    }
}
