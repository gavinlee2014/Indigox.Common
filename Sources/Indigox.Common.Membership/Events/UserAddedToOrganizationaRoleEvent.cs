using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class UserAddedToOrganizationaRoleEvent : IEvent
    {
        public IUser User { get; set; }
        public IOrganizationalRole OrganizationalRole { get; set; }

        public UserAddedToOrganizationaRoleEvent(IUser user, IOrganizationalRole organizationalRole)
        {
            this.User = user;
            this.OrganizationalRole = organizationalRole;
        }
        public override string ToString()
        {
            return string.Format("添加用户 {0} 到组织角色 {1}", User.Name, OrganizationalRole.Name);
        }
    }
}
