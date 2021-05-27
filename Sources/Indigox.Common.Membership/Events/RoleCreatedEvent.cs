using System;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class RoleCreatedEvent : IEvent
    {
        public IRole Role { get; set; }

        public RoleCreatedEvent(IRole role)
        {
            this.Role = role;
        }
        public override string ToString()
        {
            return string.Format("创建角色 {0}", Role.Name);
        }
    }
}