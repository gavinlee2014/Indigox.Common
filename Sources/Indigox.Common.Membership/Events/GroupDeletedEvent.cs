using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class GroupDeletedEvent : IEvent
    {
        public IGroup Group { get; set; }

        public GroupDeletedEvent(IGroup group)
        {
            this.Group = group;
        }
        public override string ToString()
        {
            return string.Format("删除群组 {0} ",Group.Name);
        }
    }
}
