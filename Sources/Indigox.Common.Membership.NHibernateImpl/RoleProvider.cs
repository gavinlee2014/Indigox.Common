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
    public class RoleProvider : IRoleProvider
    {
        private static readonly Dictionary<string, IRole> levelRoleCache = new Dictionary<string, IRole>();
        private static object levelRoleCacheLocker = new object();

        public IRole GetRoleByID( string id )
        {
            if ( String.IsNullOrEmpty( id ) )
            {
                return null;
            }

            // & 符号后的数字表示角色的 level，不同 level 的角色返回的 reference 不一样
            if ( !id.Contains( "&" ) )
            {
                ISession session = SessionFactories.Instance.Get( typeof( IPrincipal ).Assembly ).GetCurrentSession();
                {
                    Role role = session.Get<Role>( id );
                    return role;
                }
            }
            else
            {
                if ( !levelRoleCache.ContainsKey( id ) )
                {
                    lock ( levelRoleCacheLocker )
                    {
                        if ( !levelRoleCache.ContainsKey( id ) )
                        {
                            string roleId = id.Substring( 0, id.IndexOf( "&" ) );
                            string level = id.Substring( id.IndexOf( "&" ) + 1 );

                            ISession session = SessionFactories.Instance.Get( typeof( IPrincipal ).Assembly ).GetCurrentSession();
                            {
                                Role role = session.Get<Role>( roleId );
                                if ( role == null )
                                {
                                    throw new MemberNotFoundException( id, MemberNotFoundException.TYPE_ID );
                                }

                                Role levelRole = new Role();
                                levelRole.ID = id;
                                levelRole.Name = role.Name + "[L" + level + "]";
                                levelRole.Email = role.Email;
                                levelRole.FullName = role.FullName + "[L" + level + "]";
                                levelRoleCache[ id ] = levelRole;
                            }
                        }
                    }
                }

                return levelRoleCache[ id ];
            }
        }

        public IList<IOrganizationalRole> GetOrganizationalRoleFromRole( IOrganizationalHolder holder, IRole role )
        {
            if ( !role.ID.Contains( "&" ) )
            {
                throw new ApplicationException( "角色[" + role.ID + "] 不带 level 信息，无法获取其组织角色！" );
            }

            string roleId = role.ID.Substring( 0, role.ID.IndexOf( "&" ) );
            string level = role.ID.Substring( role.ID.IndexOf( "&" ) + 1 );

            IRecordSet recordSet = Module.UUV_DB.QueryText(
                SQL_GetOrganizationalRoleFromRole,
                "@id nvarchar(50), @genposiid nvarchar(50), @level int",
                holder.ID, roleId, level
            );

            List<string> organizationalRoleIds = new List<string>();
            foreach ( IRecord record in recordSet.Records )
            {
                organizationalRoleIds.Add( record.GetString( "PositionID" ) );
            }
            List<IOrganizationalRole> organizationalRoles = new List<IOrganizationalRole>();
            foreach ( string organizationalRoleId in organizationalRoleIds )
            {
                organizationalRoles.Add( ProviderFactories.GetFactory().GetOrganizationalRoleProvider().GetOrganizationalRoleByID( organizationalRoleId ) );
            }
            return organizationalRoles;
        }

        private static readonly string SQL_GetOrganizationalRoleFromRole = @"
declare @orgid nvarchar(50);
select @orgid=UpObjID from dbo.UV_OBJ_CON where SubObjID=@id;
if @orgid is null
begin
select @orgid=OrgID from dbo.UV_DETAIL_POSITION where PositionID=@id;
end

;with descendent as
(
  select UpObjID,SubObjID from dbo.UV_OBJ_CON where SubObjID = @orgid
  union all
  select a.UpObjID,a.SubObjID from dbo.UV_OBJ_CON a join descendent b
    on a.UpObjID = b.SubObjID
),
ancestor as
(
  select UpObjID,SubObjID from dbo.UV_OBJ_CON where SubObjID = @orgid
  union all
  select a.UpObjID,a.SubObjID from dbo.UV_OBJ_CON a join ancestor b
    on a.SubObjID = b.UpObjID
)
select @orgid=OrgID from
(
    select DynGrpNum,OrgID from dbo.UV_ORG org join ancestor on (ancestor.SubObjID=org.OrgID or ancestor.UpObjID=org.OrgID)
    union
    select DynGrpNum,OrgID from  dbo.UV_ORG org1 join descendent on (descendent.SubObjID=org1.OrgID or descendent.UpObjID=org1.OrgID)
) al
where (len(al.DynGrpNum)/3-1)=@level
;with allchild as
(
  select UpObjID,SubObjID from dbo.UV_OBJ_CON where SubObjID = @orgid
  union all
  select a.UpObjID,a.SubObjID from dbo.UV_OBJ_CON a join allchild b
    on a.UpObjID = b.SubObjID
)
select PositionID from dbo.UV_DETAIL_POSITION where GenPositionID=@genposiid and OrgID in
(
    select SubObjID from allchild
)";
    }
}