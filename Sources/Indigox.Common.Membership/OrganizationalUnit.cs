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
    public class OrganizationalUnit : Container, IOrganizationalUnit, IMutableOrganizationalUnit
    {
        public OrganizationalUnit()
        {
        }

        public OrganizationalUnit(IOrganizationalUnit organization)
        {
            this.organization = organization;
            this.memberOf.Add(organization);
            Container container = organization as Container;
            container.InternalAddMember(this);
        }

        public static IOrganizationalUnit GetOrganizationByID(string id)
        {
            return ProviderFactories.GetFactory().GetOrganizationalUnitProvider().GetOrganizationalUnitByID(id);
        }

        [NonSerialized]
        private IPrincipal manager;

        [NonSerialized]
        private IOrganizationalUnit organization;

        [XmlIgnore]
        public IPrincipal Manager
        {
            get { return manager; }
            set { manager = value; }
        }

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

        public IPrincipal GetManager()
        {
            return ProviderFactories.GetFactory().GetOrganizationalUnitProvider().GetManager(this);
        }

        public IPrincipal GetDirector()
        {
            return ProviderFactories.GetFactory().GetOrganizationalUnitProvider().GetDirector(this);
        }

        public new IList<IOrganizationalPerson> GetAllUsers()
        {
            IList<IUser> userList = base.GetAllUsers();
            IList<IOrganizationalPerson> list = new List<IOrganizationalPerson>();
            foreach (IUser user in userList)
            {
                if (user is IOrganizationalPerson)
                {
                    list.Add(user as IOrganizationalPerson);
                }
            }
            return list;
        }

        public new IList<IOrganizationalObject> GetAllMembers()
        {
            IList<IPrincipal> principalList = base.GetAllMembers();
            IList<IOrganizationalObject> list = new List<IOrganizationalObject>();
            foreach (IPrincipal organizationalObj in principalList)
            {
                if (organizationalObj is IOrganizationalObject)
                {
                    list.Add(organizationalObj as IOrganizationalObject);
                }
            }
            return list;
        }

        protected override void TriggerPropertyChangedEvent(string propertyName, object oldValue, object newValue)
        {
            //EventTrigger.Trigger(this, new OrganizationalUnitPropertyChangedEvent(this.ID, propertyName, oldValue, newValue));
        }

        protected override void TriggerRemovedFromEvent(IContainer container)
        {
            if (container is IOrganizationalUnit)
            {
                EventTrigger.Trigger(this, new OrganizationalUnitRemovedFromOrganizationalUnitEvent(this, container as IOrganizationalUnit));
            }
            else if (container is Group)
            {
                EventTrigger.Trigger(this, new OrganizationalUnitRemovedFromGroupEvent(this, container as Group));
            }
        }

        protected override void TriggerAddedToEvent(IContainer container)
        {
            if (container is IOrganizationalUnit)
            {
                EventTrigger.Trigger(this, new OrganizationalUnitAddedToOrganizationalUnitEvent(this, container as IOrganizationalUnit));
            }
            else if (container is Group)
            {
                EventTrigger.Trigger(this, new OrganizationalUnitAddedToGroupEvent(this, container as Group));
            }
        }

    }
}