using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalUnitAddedToGroupEvent : IEvent
    {
        public IOrganizationalUnit OrganizationalUnit { get; set; }
        public IGroup Group { get; set; }

        public OrganizationalUnitAddedToGroupEvent(IOrganizationalUnit organizationalUnit, IGroup group)
        {
            this.OrganizationalUnit = organizationalUnit;
            this.Group = group;
        }
        public override string ToString()
        {
            return string.Format("添加部门 {0} 到群组 {1}", OrganizationalUnit.Name, Group.Name);
        }
    }
}
