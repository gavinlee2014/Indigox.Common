using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace Indigox.Common.Utilities
{
    public static class UrlUtil
    {
        private static string BasicUrl
        {
            get
            {
                return HttpContext.Current.Request.Url.AbsoluteUri;
            }
        }

        /// <summary>
        /// Gets the absolute URL, start from http or https, like "http://localhost/default.aspx".
        /// </summary>
        /// <param name="url">The URL, both relative and absolute url are allowed.</param>
        /// <returns></returns>
        public static string GetAbsoluteUrl( string url )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the absolute URL, start after hostname, the first char is '/', like "/default.aspx".
        /// </summary>
        /// <param name="url">The URL, both relative and absolute url are allowed.</param>
        /// <returns></returns>
        public static string GetAbsolutePath( string url )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the relative URL to the basic URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="basicUrl">The basic URL.</param>
        /// <returns></returns>
        public static string GetRelativeUrl( string url, string basicUrl )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the query. Ignore case.
        /// </summary>
        /// <param name="url">The URL. Contains or start with '?', otherwise, return null.</param>
        /// <returns></returns>
        public static NameValueCollection GetQuery( string url )
        {
            SimpleUri uri = new SimpleUri( url );
            NameValueCollection originQuery = QueryStringParser.GetQuery( uri.QueryString );
            return originQuery;
        }

        public static string RemoveQuery( string url, string name )
        {
            SimpleUri uri = new SimpleUri( url );
            NameValueCollection originQuery = QueryStringParser.GetQuery( uri.QueryString );
            originQuery.Remove( name );
            uri.QueryString = QueryStringParser.GetQueryString( originQuery );
            return uri.Url;
        }

        public static string SetQuery( string url, string name, string value )
        {
            SimpleUri uri = new SimpleUri( url );
            NameValueCollection originQuery = QueryStringParser.GetQuery( uri.QueryString );
            originQuery[ name ] = value;
            uri.QueryString = QueryStringParser.GetQueryString( originQuery );
            return uri.Url;
        }

        public static string SetQuery( string url, IDictionary<string, string> query )
        {
            if ( query == null || query.Count == 0 )
            {
                return url;
            }

            SimpleUri uri = new SimpleUri( url );
            NameValueCollection originQuery = QueryStringParser.GetQuery( uri.QueryString );
            foreach ( string key in query.Keys )
            {
                originQuery[ key ] = query[ key ];
            }
            uri.QueryString = QueryStringParser.GetQueryString( originQuery );
            return uri.Url;
        }
    }
}
