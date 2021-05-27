using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class UserRemovedFromOrganizationalRoleEvent : IEvent
    {
        public IUser User { get; set; }
        public IOrganizationalRole OrganizationalRole { get; set; }

        public UserRemovedFromOrganizationalRoleEvent(IUser user, IOrganizationalRole organizationalRole)
        {
            this.User = user;
            this.OrganizationalRole = organizationalRole;
        }
        public override string ToString()
        {
            return string.Format("从组织角色 {0} 中删除用户 {1}",OrganizationalRole.Name,User.Name);
        }
    }
}
