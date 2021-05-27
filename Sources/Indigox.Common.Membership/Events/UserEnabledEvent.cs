using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class UserEnabledEvent : IEvent
    {
        public IUser User { get; set; }

        public UserEnabledEvent(IUser user)
        {
            this.User = user;
        }
        public override string ToString()
        {
            return string.Format("启用用户 {0}", User.Name);
        }
    }
}
