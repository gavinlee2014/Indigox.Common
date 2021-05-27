using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class UserRemovedFromOrganizationalUnitEvent : IEvent
    {
        public IUser User { get; set; }
        public IOrganizationalUnit OrganizationalUnit { get; set; }

        public UserRemovedFromOrganizationalUnitEvent(IUser user, IOrganizationalUnit organizationalUnit)
        {
            this.User = user;
            this.OrganizationalUnit = organizationalUnit;
        }
        public override string ToString()
        {
            return string.Format("从部门 {0} 删除用户 {1}",OrganizationalUnit.Name,User.Name);
        }
    }
}
