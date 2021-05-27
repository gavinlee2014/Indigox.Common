using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalRoleDeletedEvent : IEvent
    {
        public IOrganizationalRole OrganizationalRole { get; set; }

        public OrganizationalRoleDeletedEvent(IOrganizationalRole organizationalRole)
        {
            this.OrganizationalRole = organizationalRole;
        }
        public override string ToString()
        {
            return string.Format("删除组织角色 {0}", OrganizationalRole.Name);
        }
    }
}
