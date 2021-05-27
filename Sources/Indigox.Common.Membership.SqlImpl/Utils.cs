using Indigox.Common.Database;
using Indigox.Common.Database.Configuration;
using Indigox.Common.Membership.Configuration;

namespace Indigox.Common.Membership.SqlImpl
{
    class Utils
    {
        public static SqlHelper GetSqlHelper()
        {
            string connStr = MembershipSection.Default.UserProvider.ConnectionString;
            if ( connStr.StartsWith( "$" ) )
            {
                connStr = DatabaseSection.Default.Connections[ connStr.Substring( 1 ) ].ConnectionString;
            }
            SqlHelper sqlHelper = new SqlHelper( connStr );
            return sqlHelper;
        }
    }
}
