using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Utilities
{
    class SimpleUri
    {
        private string path;
        private string queryString;

        public SimpleUri( string url )
        {
            int pathEndPosition = url.IndexOf( QueryStringParser.QueryStringStartChar );
            if ( pathEndPosition > 0 )
            {
                this.path = url.Substring( 0, pathEndPosition );
                this.queryString = url.Substring( pathEndPosition + 1 );
            }
            else
            {
                this.path = url;
            }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public string QueryString
        {
            get { return queryString; }
            set { queryString = value; }
        }

        public string Url
        {
            get
            {
                if ( !string.IsNullOrEmpty( this.queryString ) )
                {
                    return this.path + QueryStringParser.QueryStringStartChar + this.queryString;
                }
                else
                {
                    return this.path;
                };
            }
        }

        public override string ToString()
        {
            return this.Url;
        }
    }
}
