using System;
using Indigox.Common.Membership.Exceptions;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;
using Indigox.Common.NHibernateFactories;
using NHibernate;

namespace Indigox.Common.Membership.NHibernateImpl
{
    public class PrincipalProvider : IPrincipalProvider
    {
        public IPrincipal GetPrincipalByID( string id )
        {
            if ( String.IsNullOrEmpty( id ) )
            {
                return null;
            }

            if ( !id.Contains( "&" ) )
            {
                ISession session = SessionFactories.Instance.Get( typeof( IPrincipal ).Assembly ).GetCurrentSession();
                {
                    Principal user = session.Get<Principal>( id );
                    if ( user == null )
                    {
                        throw new MemberNotFoundException( id, MemberNotFoundException.TYPE_ID );
                    }
                    return user;
                }
            }
            else
            {
                // 只有角色ID中会包含 & 符号，& 符号后的数字表示角色的 level
                return new RoleProvider().GetRoleByID( id );
            }
        }
    }
}