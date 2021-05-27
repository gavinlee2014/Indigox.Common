using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalRoleRemovedFromRoleEvent : IEvent
    {
        public IOrganizationalRole OrganizationalRole { get; set; }
        public IRole Role { get; set; }

        public OrganizationalRoleRemovedFromRoleEvent(IOrganizationalRole organizationalRole, IRole role)
        {
            this.OrganizationalRole = organizationalRole;
            this.Role = role;
        }
        public override string ToString()
        {
            return string.Format("从角色 {0} 删除组织角色 {1}", Role.Name, OrganizationalRole.Name);
        }
    }
}
