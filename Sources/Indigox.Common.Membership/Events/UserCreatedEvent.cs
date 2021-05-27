using System;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class UserCreatedEvent : IEvent
    {
        public IUser User { get; set; }

        public UserCreatedEvent(IUser user)
        {
            this.User = user;
        }
        public override string ToString()
        {
            return string.Format("创建用户 {0}", User.Name);
        }
    }
}