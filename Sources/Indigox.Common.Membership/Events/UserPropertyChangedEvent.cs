using System;
using System.Collections.Generic;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class UserPropertyChangedEvent : IEvent
    {
        public IUser User { get; set; }
        public IDictionary<string, object> PropertyChanges { get; set; }

        public UserPropertyChangedEvent( IUser user, IDictionary<string, object> propertyChanges )
        {
            this.User = user;
            this.PropertyChanges = propertyChanges;
        }
        public override string ToString()
        {
            return string.Format("修改用户 {0} 的属性", User.Name);
        }
    }
}