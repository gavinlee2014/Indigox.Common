using System;
using System.Collections.Generic;
using Indigox.Common.EventBus;
using Indigox.Common.Membership.Events;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership
{
    [Serializable]
    public class Role : Container, IRole, IMutableRole
    {
        public static IRole GetRoleByID( string id )
        {
            IRole role = ProviderFactories.GetFactory().GetRoleProvider().GetRoleByID( id );
            return role;
        }

        private static IDictionary<RoleLevel, Type> typeMap;

        static Role()
        {
            typeMap = new Dictionary<RoleLevel, Type>();
            typeMap.Add( RoleLevel.Corporation, typeof( Corporation ) );
            typeMap.Add( RoleLevel.Company, typeof( Company ) );
            typeMap.Add( RoleLevel.Department, typeof( Department ) );
            typeMap.Add( RoleLevel.Section, typeof( Section ) );
        }

        public static Type GetRoleLevelType( RoleLevel level )
        {
            return typeMap[ level ];
        }

        public Role()
        {
        }

        private RoleLevel level;

        public RoleLevel Level
        {
            get { return level; }
            set { level = value; }
        }

        public IList<IOrganizationalRole> GetOrganizationalRoles( IOrganizationalHolder holder )
        {
            var provider = ProviderFactories.GetFactory().GetRoleProvider();
            return provider.GetOrganizationalRoleFromRole( holder, this );
        }

        protected override void TriggerPropertyChangedEvent( string propertyName, object oldValue, object newValue )
        {
            //EventTrigger.Trigger( this, new RolePropertyChangedEvent( this.ID, propertyName, oldValue, newValue ) );
        }

        protected override void TriggerRemovedFromEvent(IContainer container)
        {
        }

        protected override void TriggerAddedToEvent(IContainer container)
        {
        }
    }
}