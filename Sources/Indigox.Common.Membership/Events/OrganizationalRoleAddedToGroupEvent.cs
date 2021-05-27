using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalRoleAddedToGroupEvent : IEvent
    {
        public IOrganizationalRole OrganizationalRole { get; set; }
        public IGroup Group { get; set; }

        public OrganizationalRoleAddedToGroupEvent(IOrganizationalRole organizationalRole, IGroup group)
        {
            this.OrganizationalRole = organizationalRole;
            this.Group = group;
        }
        public override string ToString()
        {
            return string.Format("添加组织角色 {0} 到群组 {1}", OrganizationalRole.Name, Group.Name);
        }
    }
}
