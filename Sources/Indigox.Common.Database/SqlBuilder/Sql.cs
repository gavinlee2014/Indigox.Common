using System;
using System.Collections.Generic;

namespace Indigox.Common.Data.SqlBuilder
{
    public class Sql
    {
        public static readonly int InfiniteLimit = 0;

        public static SqlSelectCommand Select( params string[] fields )
        {
            return new SqlSelectCommand().Select( fields );
        }

        public static SqlInsertCommand Insert( string tableName )
        {
            return new SqlInsertCommand().InsertInto( tableName );
        }

        public static SqlUpdateCommand Update( string tableName )
        {
            return new SqlUpdateCommand().Update( tableName );
        }

        public static SqlDeleteCommand Delete()
        {
            return new SqlDeleteCommand();
        }

        public static SqlWhere Where
        {
            get
            {
                return new SqlWhere();
            }
        }

        public static SqlWhere And( params SqlWhere[] wheres )
        {
            List<string> whereClauses = new List<string>();
            for ( int i = 0; i < wheres.Length; i++ )
            {
                string whereClause = wheres[ i ].ToString();
                if ( !string.IsNullOrEmpty( whereClause ) )
                {
                    whereClauses.Add( whereClause );
                }
            }
            if ( whereClauses.Count == 0 )
            {
                return null;
            }
            else if ( whereClauses.Count == 1 )
            {
                return new SqlWhere( whereClauses[ 0 ] );
            }
            else
            {
                return new SqlWhere( "(" + string.Join( " and ", whereClauses.ToArray() ) + ")" );
            }
        }

        public static SqlWhere Or( params SqlWhere[] wheres )
        {
            List<string> whereClauses = new List<string>();
            for ( int i = 0; i < wheres.Length; i++ )
            {
                string whereClause = wheres[ i ].ToString();
                if ( !string.IsNullOrEmpty( whereClause ) )
                {
                    whereClauses.Add( whereClause );
                }
            }
            if ( whereClauses.Count == 0 )
            {
                return null;
            }
            else if ( whereClauses.Count == 1 )
            {
                return new SqlWhere( whereClauses[ 0 ] );
            }
            else
            {
                return new SqlWhere( "(" + string.Join( " or ", whereClauses.ToArray() ) + ")" );
            }
        }

        public static SqlWhere Not( SqlWhere where )
        {
            string whereClause = where.ToString();
            if ( !string.IsNullOrEmpty( whereClause ) )
            {
                return null;
            }
            else
            {
                return new SqlWhere( "( not (" + whereClause + ") )" );
            }
        }

        public static SqlParam Param( string paramName )
        {
            return new SqlParam( paramName );
        }

        public static SqlValue Value( object value )
        {
            return new SqlValue( value );
        }

        public static SqlExpression Expression( string exp )
        {
            return new SqlExpression( exp );
        }

        public static SqlCte Cte( string cteName )
        {
            return new SqlCte( cteName );
        }
    }
}