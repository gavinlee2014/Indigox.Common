using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalUnitDeletedEvent : IEvent
    {
        public IOrganizationalUnit OrganizationalUnit { get; set; }

        public OrganizationalUnitDeletedEvent(IOrganizationalUnit organizationalUnit)
        {
            this.OrganizationalUnit = organizationalUnit;
        }
        public override string ToString()
        {
            return string.Format("删除部门 {0}", OrganizationalUnit.Name);
        }
    }
}
