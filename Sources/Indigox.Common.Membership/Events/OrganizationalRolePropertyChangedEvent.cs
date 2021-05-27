using System;
using System.Collections.Generic;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalRolePropertyChangedEvent : IEvent
    {
        public IOrganizationalRole OrganizationalRole { get; set; }
        public IDictionary<string, object> PropertyChanges { get; set; }

        public OrganizationalRolePropertyChangedEvent(IOrganizationalRole organizationalRole, IDictionary<string, object> propertyChanges)
        {
            this.OrganizationalRole= organizationalRole;
            this.PropertyChanges = propertyChanges;
        }
        public override string ToString()
        {
            return string.Format("修改组织角色 {0} 的属性", OrganizationalRole.Name);
        }
    }
}