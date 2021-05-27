using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.EventBus.Interface.Event;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Events
{
    public class OrganizationalRoleAddedToRoleEvent : IEvent
    {
        public IOrganizationalRole OrganizationalRole { get; set; }
        public IRole Role { get; set; }

        public OrganizationalRoleAddedToRoleEvent(IOrganizationalRole organizationalRole, IRole role)
        {
            this.OrganizationalRole = organizationalRole;
            this.Role = role;
        }
        public override string ToString()
        {
            return string.Format("添加组织角色 {0} 到角色 {1}", OrganizationalRole.Name, Role.Name);
        }
    }
}
