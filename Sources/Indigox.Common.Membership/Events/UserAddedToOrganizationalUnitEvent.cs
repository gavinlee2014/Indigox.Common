using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class UserAddedToOrganizationalUnitEvent : IEvent
    {
        public IUser User { get; set; }
        public IOrganizationalUnit OrganizationalUnit { get; set; }

        public UserAddedToOrganizationalUnitEvent(IUser user, IOrganizationalUnit organizationalUnit)
        {
            this.User = user;
            this.OrganizationalUnit = organizationalUnit;
        }
        public override string ToString()
        {
            return string.Format("添加用户 {0} 到部门 {1}", User.Name, OrganizationalUnit.Name);
        }
    }
}
