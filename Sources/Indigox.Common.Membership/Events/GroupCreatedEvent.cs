using System;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class GroupCreatedEvent : IEvent
    {
        public IGroup Group { get; set; }

        public GroupCreatedEvent(IGroup group)
        {
            this.Group = group;
        }
        public override string ToString()
        {
            return string.Format("创建群组 {0} ",Group.Name);
        }
    }
}