using System;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalRoleCreatedEvent : IEvent
    {
        public IOrganizationalRole OrganizationalRole { get; set; }

        public OrganizationalRoleCreatedEvent(IOrganizationalRole organizationalRole)
        {
            this.OrganizationalRole = organizationalRole;
        }
        public override string ToString()
        {
            return string.Format("创建组织角色 {0}", OrganizationalRole.Name);
        }
    }
}