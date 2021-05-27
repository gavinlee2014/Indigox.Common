using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Indigox.Common.Utilities
{
    class QueryStringParser
    {
        public const char QueryStringStartChar = '?';
        public const char QueryStringItemSeperator = '&';
        public const char QueryStringNameValueConnector = '=';

        public static NameValueCollection GetQuery( string queryString )
        {
            NameValueCollection query = new SimpleQueryString();
            if ( !string.IsNullOrEmpty( queryString ) )
            {
                string[] temp = queryString.Split( QueryStringItemSeperator );
                foreach ( string argItem in temp )
                {
                    if ( !string.IsNullOrEmpty( argItem ) )
                    {
                        int argNameEndPosition = argItem.IndexOf( QueryStringNameValueConnector );
                        string argName = argItem;
                        string argValue = null;
                        if ( argNameEndPosition > 0 )
                        {
                            argName = argItem.Substring( 0, argNameEndPosition );
                            argValue = argItem.Substring( argNameEndPosition + 1 );
                        }
                        query.Add( argName, argValue );
                    }
                }
            }
            return query;
        }

        public static string GetQueryString( NameValueCollection arguments )
        {
            if ( arguments == null || arguments.Count == 0 )
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            foreach ( string key in arguments.Keys )
            {
                if ( builder.Length != 0 )
                {
                    builder.Append( QueryStringItemSeperator );
                }
                builder.Append( key );
                if ( arguments[ key ] != null )
                {
                    builder.Append( QueryStringNameValueConnector + arguments[ key ] );
                }
            }
            return builder.ToString();
        }
    }
}
