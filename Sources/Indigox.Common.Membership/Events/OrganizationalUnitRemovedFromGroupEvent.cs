using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalUnitRemovedFromGroupEvent : IEvent
    {
        public IOrganizationalUnit OrganizationalUnit { get; set; }
        public IGroup Group { get; set; }

        public OrganizationalUnitRemovedFromGroupEvent(IOrganizationalUnit organizationalUnit, IGroup group)
        {
            this.OrganizationalUnit = organizationalUnit;
            this.Group = group;
        }
        public override string ToString()
        {
            return string.Format("从群组 {0} 删除部门 {1}", Group.Name, OrganizationalUnit.Name);
        }
    }
}
