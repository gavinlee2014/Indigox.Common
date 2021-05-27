using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class RoleDeletedEvent : IEvent
    {
        public IRole Role { get; set; }

        public RoleDeletedEvent(IRole role)
        {
            this.Role = role;
        }
        public override string ToString()
        {
            return string.Format("删除角色 {0}", Role.Name);
        }
    }
}
