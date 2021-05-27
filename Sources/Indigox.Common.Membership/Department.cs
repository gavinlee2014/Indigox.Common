using System;
using System.Xml.Serialization;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership
{
    [Serializable]
    public class Department : OrganizationalUnit, IDepartment, IMutableDepartment
    {
        public Department()
        {
        }

        public Department(IOrganizationalUnit organization)
            : base(organization)
        {
        }

        [NonSerialized]
        private IPrincipal director;

        [XmlIgnore]
        public IPrincipal Director
        {
            get { return director; }
            set { director = value; }
        }
    }
}