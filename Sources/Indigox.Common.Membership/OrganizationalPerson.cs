using System;
using System.Xml.Serialization;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;
using Indigox.Common.EventBus;
using Indigox.Common.Membership.Events;
using System.Collections.Generic;

namespace Indigox.Common.Membership
{
    [Serializable]
    public class OrganizationalPerson : User, IOrganizationalPerson, IMutableOrganizationalPerson
    {
        public OrganizationalPerson()
        {
        }

        public OrganizationalPerson(IOrganizationalUnit organization)
        {
            this.organization = organization;
            this.memberOf.Add(organization);
            Container container = organization as Container;
            container.InternalAddMember(this);
        }

        public static IOrganizationalPerson GetOrganizationalPersonByID(string id)
        {
            IOrganizationalPerson user = ProviderFactories.GetFactory().GetUserProvider().GetUserByID(id) as OrganizationalPerson;
            return user;
        }

        public static IOrganizationalPerson GetOrganizationalPersonByAccount(string account)
        {
            IOrganizationalPerson user = ProviderFactories.GetFactory().GetUserProvider().GetUserByAccount(account) as OrganizationalPerson;
            return user;
        }

        [NonSerialized]
        private IOrganizationalUnit organization;

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


    }
}