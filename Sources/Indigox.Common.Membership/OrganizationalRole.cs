using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;
using Indigox.Common.EventBus;
using Indigox.Common.Membership.Events;

namespace Indigox.Common.Membership
{
    [Serializable]
    public class OrganizationalRole : Container, IOrganizationalRole, IMutableOrganizationalRole
    {
        public OrganizationalRole()
        {
        }

        public OrganizationalRole(IOrganizationalUnit organization)
        {
            this.organization = organization;
            this.memberOf.Add(organization);
            Container container = organization as Container;
            container.InternalAddMember(this);
        }

        [Obsolete("Deprecated. Use GetOrganizationalRoleByID instead.")]
        public static IOrganizationalRole GetPositionByID(string id)
        {
            return GetOrganizationalRoleByID(id);
        }

        public static IOrganizationalRole GetOrganizationalRoleByID(string id)
        {
            return ProviderFactories.GetFactory().GetOrganizationalRoleProvider().GetOrganizationalRoleByID(id);
        }

        public static IList<IOrganizationalRole> GetOrganizationalRoleByOrganizationalPerson(string personId)
        {
            return ProviderFactories.GetFactory().GetOrganizationalRoleProvider().GetOrganizationalRoleByOrganizationalPerson(personId);
        }

        [NonSerialized]
        private IOrganizationalUnit organization;

        [NonSerialized]
        private IRole role;

        [XmlIgnore]
        public IOrganizationalUnit Organization
        {
            get
            {
                return this.organization;
            }
            set
            {
                if (this.organization == value)
                {
                    return;
                }

                if (this.organization != null && this.organization.ContainsMember(this))
                {
                    ((IMutableOrganizationalUnit)this.organization).RemoveMember(this);
                }

                if (value != null && !value.ContainsMember(this))
                {
                    ((IMutableOrganizationalUnit)value).AddMember(this);
                }

                this.organization = value;
            }
        }

        [XmlIgnore]
        public IRole Role
        {
            get
            {
                return this.role;
            }
            set
            {
                if (this.role == value)
                {
                    return;
                }

                if (this.role != null && this.role.ContainsMember(this))
                {
                    ((IMutableRole)this.role).RemoveMember(this);
                }

                if (value != null && !value.ContainsMember(this))
                {
                    ((IMutableRole)value).AddMember(this);
                }

                this.role = value;
            }
        }

        protected override void TriggerPropertyChangedEvent(string propertyName, object oldValue, object newValue)
        {
            //EventTrigger.Trigger(this, new OrganizationalRolePropertyChangedEvent(this.ID, propertyName, oldValue, newValue));
        }

        protected override void TriggerRemovedFromEvent(IContainer container)
        {
            if (container is IOrganizationalUnit)
            {
                EventTrigger.Trigger(this, new OrganizationalRoleRemovedFromOrganizationalUnitEvent(this, container as IOrganizationalUnit));
            }
            else if (container is Group)
            {
                EventTrigger.Trigger(this, new OrganizationalRoleRemovedFromGroupEvent(this, container as Group));
            }
            else if (container is Role)
            {
                EventTrigger.Trigger(this, new OrganizationalRoleRemovedFromRoleEvent(this, container as Role));
            }
        }

        protected override void TriggerAddedToEvent(IContainer container)
        {
            if (container is IOrganizationalUnit)
            {
                EventTrigger.Trigger(this, new OrganizationalRoleAddedToOrganizationalUnitEvent(this, container as IOrganizationalUnit));
            }
            else if (container is Group)
            {
                EventTrigger.Trigger(this, new OrganizationalRoleAddedToGroupEvent(this, container as Group));
            }
            else if (container is Role)
            {
                EventTrigger.Trigger(this, new OrganizationalRoleAddedToRoleEvent(this, container as Role));
            }
        }
    }
}