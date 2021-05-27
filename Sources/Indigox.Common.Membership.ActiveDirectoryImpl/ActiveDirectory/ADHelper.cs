using System;
using System.Configuration;
using System.DirectoryServices;

namespace Indigox.Common.Membership.ActiveDirectoryImpl.ActiveDirectory
{
    internal class ADHelper
    {
        private static ADHelper instance = new ADHelper();

        public static ADHelper Instance
        {
            get { return instance; }
        }

        private DirectoryEntry GetRootEntry()
        {
            string rootPath = ConfigurationManager.AppSettings[ "ADRootPath" ];
            string username = ConfigurationManager.AppSettings[ "ADUsername" ];
            string password = ConfigurationManager.AppSettings[ "ADPassword" ];

            return new DirectoryEntry( rootPath, username, password );
        }

        public ADHelper()
        {
        }

        public SearchResultCollection FindUsers( string searchKey, string[] propsToLoad, int start, int limit )
        {
            SearchResultCollection results = null;

            using ( DirectoryEntry rootEntry = this.GetRootEntry() )
            using ( DirectorySearcher searcher = new DirectorySearcher( rootEntry ) )
            {
                if ( string.IsNullOrEmpty( searchKey ) )
                {
                    //searcher.Filter = "(objectClass=organizationalPerson)";
                    searcher.Filter = ADFilter.And(
                            new ADFilter( "objectClass=organizationalPerson" ),
                            ADFilter.Not( new ADFilter( "userAccountControl=514" ) )
                        ).ToString();

                }
                else
                {
                    //searcher.Filter = "(&(objectClass=organizationalPerson)(cn=*" + searchKey + "*))";
                    //searcher.Filter = "(&(objectClass=organizationalPerson)(|(cn=*" + searchKey + "*)(sAMAccountName=*" + searchKey + "*)))";
                    searcher.Filter = ADFilter.And(
                            new ADFilter( "objectClass=organizationalPerson" ),
                            ADFilter.Or(
                                new ADFilter( "cn=*" + searchKey + "*" ),
                                new ADFilter( "sAMAccountName=*" + searchKey + "*" )
                            ),
                            ADFilter.Not( new ADFilter( "userAccountControl=514" ) )
                        ).ToString();
                }
                searcher.PropertiesToLoad.AddRange( propsToLoad );

                searcher.Sort = new SortOption( "displayName", SortDirection.Ascending );

                //searcher.SizeLimit = limit;

                searcher.VirtualListView = new DirectoryVirtualListView( limit - 1, 0, start + limit );

                results = searcher.FindAll();
            }

            return results;
        }

        public int GetSearchResultCount( string searchKey )
        {
            int count = 0;

            using ( DirectoryEntry rootEntry = this.GetRootEntry() )
            using ( DirectorySearcher searcher = new DirectorySearcher( rootEntry ) )
            {
                if ( string.IsNullOrEmpty( searchKey ) )
                {
                    searcher.Filter = "(objectClass=organizationalPerson)";
                }
                else
                {
                    searcher.Filter = "(&(objectClass=organizationalPerson)(cn=*" + searchKey + "*))";

                    //searcher.Filter = "(&(objectClass=organizationalPerson)(|(cn=*" + searchKey + "*)(sAMAccountName=" + searchKey + ")))";
                }

                searcher.Sort = new SortOption( "displayName", SortDirection.Ascending );

                searcher.VirtualListView = new DirectoryVirtualListView( 1, 0, 1 );

                using ( SearchResultCollection results = searcher.FindAll() )
                {
                    //foreach ( SearchResult result in results )
                    //{
                    //    //
                    //}
                }
                count = searcher.VirtualListView.ApproximateTotal;
            }

            return count;
        }

        public SearchResult GetUserByID( string userId, string[] propsToLoad )
        {
            if ( string.IsNullOrEmpty( userId ) )
            {
                throw new ArgumentNullException( "userId" );
            }

            SearchResult result = null;

            using ( DirectoryEntry rootEntry = this.GetRootEntry() )
            using ( DirectorySearcher searcher = new DirectorySearcher( rootEntry ) )
            {
                searcher.Filter = "(&(objectClass=organizationalPerson)(sAMAccountName=" + userId + "))";
                searcher.PropertiesToLoad.AddRange( propsToLoad );

                searcher.SizeLimit = 1;

                using ( SearchResultCollection results = searcher.FindAll() )
                {
                    if ( results.Count > 0 )
                    {
                        result = results[ 0 ];
                    }
                }
            }

            return result;
        }

        public static T GetProperty<T>( SearchResult result, string prop )
        {
            try
            {
                return (T)result.Properties[ prop ][ 0 ];
            }
            catch ( Exception )
            {
                return default( T );
            }
        }
    }
}
