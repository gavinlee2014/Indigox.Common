using System;
using System.Collections.Generic;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class GroupPropertyChangedEvent : IEvent
    {
        public IGroup Group { get; set; }
        public IDictionary<string, object> PropertyChanges { get; set; }

        public GroupPropertyChangedEvent(IGroup group, IDictionary<string, object> propertyChanges)
        {
            this.Group = group;
            this.PropertyChanges = propertyChanges;
        }
        public override string ToString()
        {
            return string.Format("修改群组 {0} 的属性",Group.Name);
        }
    }
}