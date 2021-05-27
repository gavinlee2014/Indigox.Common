using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class UserAddedToGroupEvent : IEvent
    {
        public IUser User { get; set; }
        public IGroup Group  { get; set; }

        public UserAddedToGroupEvent(IUser user, IGroup group)
        {
            this.User = user;
            this.Group = group;
        }
        public override string ToString()
        {
            return string.Format("添加用户 {0} 到群组 {1}", User.Name, Group.Name);
        }
    }
}
