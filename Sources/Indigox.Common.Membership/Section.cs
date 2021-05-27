using System;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership
{
    [Serializable]
    public class Section : OrganizationalUnit, ISection, IMutableSection
    {
        public Section()
        {
        }

        public Section(IOrganizationalUnit organization)
            : base(organization)
        {
        }
    }
}