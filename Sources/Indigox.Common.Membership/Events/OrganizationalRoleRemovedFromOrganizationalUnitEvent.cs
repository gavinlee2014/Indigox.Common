using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalRoleRemovedFromOrganizationalUnitEvent : IEvent
    {
        public IOrganizationalRole OrganizationalRole { get; set; }
        public IOrganizationalUnit OrganizationalUnit { get; set; }

        public OrganizationalRoleRemovedFromOrganizationalUnitEvent(IOrganizationalRole organizationalRole, IOrganizationalUnit organizationalUnit)
        {
            this.OrganizationalRole = organizationalRole;
            this.OrganizationalUnit = organizationalUnit;
        }
        public override string ToString()
        {
            return string.Format("从部门 {0} 删除组织角色 {1}", OrganizationalUnit.Name, OrganizationalRole.Name);
        }
    }
}
