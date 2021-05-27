using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class UserRemovedFromGroupEvent : IEvent
    {
        public IUser User { get; set; }
        public IGroup Group { get; set; }

        public UserRemovedFromGroupEvent(IUser user, IGroup group)
        {
            this.User = user;
            this.Group = group;
        }
        public override string ToString()
        {
            return string.Format("从群组 {0} 删除用户 {1}",Group.Name,User.Name);
        }
    }
}
