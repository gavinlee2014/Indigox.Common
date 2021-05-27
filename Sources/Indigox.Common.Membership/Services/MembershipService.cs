using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;
using Indigox.Common.Utilities;

namespace Indigox.Common.Membership.Services
{
    public class MembershipService
    {
        /// <summary>
        /// 获取机构负责人
        /// </summary>
        public IPrincipal GetOrganizationManager( IOrganizationalHolder organizationalHolder )
        {
            ArgumentAssert.NotNull( organizationalHolder, "organizationalHolder" );
            return organizationalHolder.Organization.GetManager();
        }

        /// <summary>
        /// 获取机构领导
        /// </summary>
        public IPrincipal GetOrganizationDirector( IOrganizationalHolder organizationalHolder )
        {
            ArgumentAssert.NotNull( organizationalHolder, "organizationalHolder" );
            return organizationalHolder.Organization.GetDirector();
        }

        [Obsolete( "Renamed to GetReportingManager" )]
        public IOrganizationalHolder GetUserManager( IOrganizationalHolder organizationalHolder, IReportingHierarchy reportingHierarchy )
        {
            return GetReportingManager( organizationalHolder, reportingHierarchy );
        }

        /// <summary>
        /// 获取用户的直接上级经理（汇报关系树）
        /// </summary>
        public IOrganizationalHolder GetReportingManager( IOrganizationalHolder organizationalHolder, IReportingHierarchy reportingHierarchy )
        {
            ArgumentAssert.NotNull( organizationalHolder, "organizationalHolder" );
            ArgumentAssert.NotNull( reportingHierarchy, "reportingHierarchy" );
            IOrganizationalHolder manager = reportingHierarchy.GetManager( organizationalHolder );
            return manager;
        }

        [Obsolete( "Renamed to GetOrganizationalRolesFromRole" )]
        public IList<IOrganizationalRole> GetPositionsFromRelativePosition( IOrganizationalHolder organizationalHolder, IRole role )
        {
            return GetOrganizationalRolesFromRole( organizationalHolder, role );
        }

        /// <summary>
        /// 获取用户的组织角色
        /// </summary>
        public IList<IOrganizationalRole> GetOrganizationalRolesFromRole( IOrganizationalHolder organizationalHolder, IRole role )
        {
            ArgumentAssert.NotNull( organizationalHolder, "organizationalHolder" );
            return role.GetOrganizationalRoles( organizationalHolder );
        }

        /// <summary>
        /// 判断 Group 是否包含 Principal（不递归查询）
        /// </summary>
        public bool InGroup( string unitId, IPrincipal principal )
        {
            ArgumentAssert.NotEmpty( unitId, "groupId" );
            ArgumentAssert.NotNull( principal, "principal" );
            IContainer unit = Container.GetContainerByID( unitId );
            return InGroupEx2( unit, principal, false );
        }

        /// <summary>
        /// 判断 Group 是否包含 Principal（可递归查询）
        /// </summary>
        public bool InGroupEx( string unitId, IPrincipal principal, bool recrusive )
        {
            ArgumentAssert.NotEmpty( unitId, "groupId" );
            ArgumentAssert.NotNull( principal, "principal" );
            IContainer unit = Container.GetContainerByID( unitId );
            return InGroupEx2( unit, principal, recrusive );
        }

        public IList<IUser> GetAllUsers()
        {
            IUserProvider provider = ProviderFactories.GetFactory().GetUserProvider();
            return provider.GetAllUsers();
        }

        public IList<IContainer> GetUserAllGroups( IUser user )
        {
            List<IContainer> containers = new List<IContainer>( user.MemberOf );

            int cur = 0;

            while ( cur < containers.Count )
            {
                IContainer container = containers[ cur ];

                foreach ( IContainer upperContainer in container.MemberOf )
                {
                    if ( !containers.Contains( upperContainer ) )
                    {
                        containers.Add( upperContainer );
                    }
                }

                cur++;
            }

            return containers;
        }

        private bool InGroupEx2( IContainer unit, IPrincipal principal, bool recrusive )
        {
            if ( unit.ContainsMember( principal ) )
            {
                return true;
            }
            if ( recrusive )
            {
                foreach ( IPrincipal sub in unit.Members )
                {
                    if ( sub is IContainer )
                    {
                        IContainer subUnit = (IContainer)sub;
                        if ( InGroupEx2( subUnit, principal, recrusive ) )
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}