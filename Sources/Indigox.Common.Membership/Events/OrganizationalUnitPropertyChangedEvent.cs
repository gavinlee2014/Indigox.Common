using System;
using System.Collections.Generic;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalUnitPropertyChangedEvent : IEvent
    {
        public IOrganizationalUnit OrganizationalUnit { get; set; }
        public IDictionary<string, object> PropertyChanges { get; set; }

        public OrganizationalUnitPropertyChangedEvent(IOrganizationalUnit organizationalUnit, IDictionary<string, object> propertyChanges)
        {
            this.OrganizationalUnit = organizationalUnit;
            this.PropertyChanges = propertyChanges;
        }
        public override string ToString()
        {
            return string.Format("修改部门 {0} 的属性", OrganizationalUnit.Name);
        }
    }
}