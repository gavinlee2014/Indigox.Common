using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalRoleRemovedFromGroupEvent : IEvent
    {
        public IOrganizationalRole OrganizationalRole { get; set; }
        public IGroup Group { get; set; }

        public OrganizationalRoleRemovedFromGroupEvent(IOrganizationalRole organizationalRole, IGroup group)
        {
            this.OrganizationalRole = organizationalRole;
            this.Group = group;
        }
        public override string ToString()
        {
            return string.Format("从群组 {0} 删除组织角色 {1}", Group.Name, OrganizationalRole.Name);
        }
    }
}
