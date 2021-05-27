using System;
using System.Collections.Generic;
using Indigox.Common.Data.Interface;
using Indigox.Common.Membership.Exceptions;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;
using Indigox.Common.NHibernateFactories;
using NHibernate;

namespace Indigox.Common.Membership.NHibernateImpl
{
    public class OrganizationalRoleProvider : IOrganizationalRoleProvider
    {
        public IOrganizationalRole GetOrganizationalRoleByID( string id )
        {
            if ( String.IsNullOrEmpty( id ) )
            {
                return null;
            }

            ISession session = SessionFactories.Instance.Get( typeof( IPrincipal ).Assembly ).GetCurrentSession();
            {
                OrganizationalRole position = session.Get<OrganizationalRole>( id );
                if ( position == null )
                {
                    throw new MemberNotFoundException( id, MemberNotFoundException.TYPE_ID );
                }
                return position;
            }
        }

        public void SavePosition( OrganizationalRole position )
        {
            ISession session = SessionFactories.Instance.Get( typeof( IPrincipal ).Assembly ).GetCurrentSession();
            {
                session.Save( position );
                session.Flush();
            }
        }

        public void UpdataPosition( OrganizationalRole position )
        {
            ISession session = SessionFactories.Instance.Get( typeof( IPrincipal ).Assembly ).GetCurrentSession();
            {
                session.Update( position );
                session.Flush();
            }
        }

        public IList<IOrganizationalRole> GetOrganizationalRoleByOrganizationalPerson( string personId )
        {
            if ( String.IsNullOrEmpty( personId ) )
            {
                throw new ArgumentNullException( "OrganizationalPerson ID cannot be null or empty" );
            }
            string sql = "select PositionID from dbo.UV_USER_POSITION where UserID='" + personId + "'";
            IRecordSet recordSet = Module.UUV_DB.QueryText( sql );

            List<string> posiIds = new List<string>();
            foreach ( IRecord record in recordSet.Records )
            {
                posiIds.Add( record.GetString( "PositionID" ) );
            }
            List<IOrganizationalRole> positions = new List<IOrganizationalRole>();
            foreach ( string roleId in posiIds )
            {
                positions.Add( this.GetOrganizationalRoleByID( roleId ) );
            }
            return positions;
        }
    }
}