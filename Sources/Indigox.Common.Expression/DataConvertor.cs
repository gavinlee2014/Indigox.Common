using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Expression
{
    class DataConvertor
    {
        public static object ConvertToInt( object val )
        {
            if ( val is string )
            {
                try
                {
                    return int.Parse( (string)val );
                }
                catch ( Exception ex )
                {
                    throw new DataConvertException( typeof( string ), typeof( int ), ex );
                }
            }
            throw new DataConvertException( val.GetType(), typeof( int ), "Format error." );
        }
    }
}
