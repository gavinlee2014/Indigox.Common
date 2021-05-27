using System;
using System.Data;
using Indigox.Common.Membership;
using Indigox.Common.Membership.Interfaces;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Indigox.Common.NHibernate.Extension.UserTypes
{
    [Serializable]
    public class PrincipalType : AbstractCharType
    {
        public PrincipalType()
            : base( new StringFixedLengthSqlType( 12 ) )
        {
        }

        public override string Name
        {
            get { return "PrincipalRef"; }
        }

        public override object Get( IDataReader rs, int index )
        {
            string value = Convert.ToString( rs[ index ] );
            IPrincipal principal = Principal.GetPrincipalByID( value );
            return principal;
        }

        public override object Get( IDataReader rs, string name )
        {
            return Get( rs, rs.GetOrdinal( name ) );
        }

        public override System.Type PrimitiveClass
        {
            get { return typeof( string ); }
        }

        public override System.Type ReturnedClass
        {
            get { return typeof( IPrincipal ); }
        }

        public override void Set( IDbCommand cmd, object value, int index )
        {
            IPrincipal principal = value as IPrincipal;
            ( (IDataParameter)cmd.Parameters[ index ] ).Value = ( ( principal == null ) ? null : principal.ID );
        }
    }
}