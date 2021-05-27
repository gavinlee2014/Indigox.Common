using System;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership
{
    [Serializable]
    public class Corporation : OrganizationalUnit, ICorporation, IMutableCorporation
    {
        public Corporation()
        {
        }

        public Corporation(IOrganizationalUnit organization)
            : base(organization)
        {
        }

        public static ICorporation GetCorporation()
        {
            // only one corporation
            return ProviderFactories.GetFactory().GetOrganizationalUnitProvider().GetCorporation();
        }
    }
}