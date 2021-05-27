using System;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership
{
    [Serializable]
    public class Company : OrganizationalUnit, ICompany, IMutableCompany
    {
        public Company()
        {
        }

        public Company(IOrganizationalUnit organization)
            : base(organization)
        {
        }

    }
}