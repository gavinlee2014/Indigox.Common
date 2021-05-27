using System;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalUnitCreatedEvent : IEvent
    {
        public IOrganizationalUnit OrganizationalUnit { get; set; }

        public OrganizationalUnitCreatedEvent(IOrganizationalUnit organizationalUnit)
        {
            this.OrganizationalUnit= organizationalUnit;
        }
        public override string ToString()
        {
            return string.Format("新建部门 {0}", OrganizationalUnit.Name);
        }
    }
}