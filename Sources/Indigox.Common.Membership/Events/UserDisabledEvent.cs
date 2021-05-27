using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class UserDisabledEvent : IEvent
    {
        public IUser User { get; set; }

        public UserDisabledEvent(IUser user)
        {
            this.User = user;
        }
        public override string ToString()
        {
            return string.Format("禁用用户 {0}", User.Name);
        }
    }
}
