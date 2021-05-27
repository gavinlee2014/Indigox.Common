using System;
using System.Collections.Generic;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class RolePropertyChangedEvent : IEvent
    {
        public IRole Role { get; set; }
        public IDictionary<string, object> PropertyChanges { get; set; }

        public RolePropertyChangedEvent(IRole role, IDictionary<string, object> propertyChanges)
        {
            this.Role = role;
            this.PropertyChanges = propertyChanges;
        }
        public override string ToString()
        {
            return string.Format("修改角色 {0} 的属性", Role.Name);
        }
    }
}