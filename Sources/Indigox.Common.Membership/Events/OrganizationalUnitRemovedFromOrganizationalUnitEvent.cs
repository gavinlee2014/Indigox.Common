using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalUnitRemovedFromOrganizationalUnitEvent : IEvent
    {
        public IOrganizationalUnit OrganizationalUnit { get; set; }
        public IOrganizationalUnit ParentOrganizationalUnit { get; set; }

        public OrganizationalUnitRemovedFromOrganizationalUnitEvent(IOrganizationalUnit organizationalUnit, IOrganizationalUnit parentOrganizationalUnit)
        {
            this.OrganizationalUnit = organizationalUnit;
            this.ParentOrganizationalUnit = parentOrganizationalUnit;
        }
        public override string ToString()
        {
            return string.Format("从部门 {0} 移除子部门 {1}", ParentOrganizationalUnit.Name, OrganizationalUnit.Name );
        }
    }
}
