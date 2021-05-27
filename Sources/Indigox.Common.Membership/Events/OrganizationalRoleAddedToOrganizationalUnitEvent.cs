using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalRoleAddedToOrganizationalUnitEvent : IEvent
    {
        public IOrganizationalRole OrganizationalRole { get; set; }
        public IOrganizationalUnit OrganizationalUnit { get; set; }

        public OrganizationalRoleAddedToOrganizationalUnitEvent(IOrganizationalRole organizationalRole, IOrganizationalUnit organizationalUnit)
        {
            this.OrganizationalRole = organizationalRole;
            this.OrganizationalUnit = organizationalUnit;
        }
        public override string ToString()
        {
            return string.Format("添加组织角色 {0} 到部门 {1}",OrganizationalRole.Name,OrganizationalUnit.Name);
        }
    }
}
