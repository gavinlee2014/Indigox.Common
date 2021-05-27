using System;
using Indigox.Common.Membership.Exceptions;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;
using Indigox.Common.NHibernateFactories;
using Indigox.Common.Utilities;
using NHibernate;

namespace Indigox.Common.Membership.NHibernateImpl
{
    public class OrganizationalUnitProvider : IOrganizationalUnitProvider
    {
        public IOrganizationalUnit GetOrganizationalUnitByID( string id )
        {
            if ( String.IsNullOrEmpty( id ) )
            {
                return null;
            }

            ISession session = SessionFactories.Instance.Get( typeof( IPrincipal ).Assembly ).GetCurrentSession();
            {
                OrganizationalUnit org = session.Get<OrganizationalUnit>( id );
                if ( org == null )
                {
                    throw new MemberNotFoundException( id, MemberNotFoundException.TYPE_ID );
                }
                return org;
            }
        }

        public IPrincipal GetManager( IOrganizationalUnit organization )
        {
            ArgumentAssert.NotNull( organization, "organization" );

            string sql = @"SELECT UpObjID FROM UV_MANAGER WHERE SubOrgID = @id and IsDirectManager = 1";

            string organizationManagerId = (string)Module.UUV_DB.ScalarText(
                sql,
                "@id varchar",
                organization.ID
            );

            return Principal.GetPrincipalByID( organizationManagerId );
        }

        public IPrincipal GetDirector( IOrganizationalUnit organization )
        {
            ArgumentAssert.NotNull( organization, "organization" );

            string sql = @"SELECT UpObjID FROM UV_MANAGER WHERE SubOrgID = @id and IsDirectManager = 0";

            string organizationDirectorId = (string)Module.UUV_DB.ScalarText(
                sql,
                "@id varchar",
                organization.ID
            );

            return Principal.GetPrincipalByID( organizationDirectorId );
        }

        public ICorporation GetCorporation()
        {
            return (ICorporation)GetOrganizationalUnitByID( "OR1000000000" );
        }
    }
}