using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalUnitAddedToOrganizationalUnitEvent : IEvent
    {
        public IOrganizationalUnit OrganizationalUnit { get; set; }
        public IOrganizationalUnit ParentOrganizationalUnit { get; set; }

        public OrganizationalUnitAddedToOrganizationalUnitEvent(IOrganizationalUnit organizationalUnit, IOrganizationalUnit parentOrganizationalUnit)
        {
            this.OrganizationalUnit = organizationalUnit;
            this.ParentOrganizationalUnit = parentOrganizationalUnit;
        }
        public override string ToString()
        {
            return string.Format("添加子部门 {0} 到 {1} 部门", OrganizationalUnit.Name, ParentOrganizationalUnit.Name);
        }
    }
}
