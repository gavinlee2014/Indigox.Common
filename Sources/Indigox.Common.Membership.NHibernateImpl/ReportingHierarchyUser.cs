using System;

namespace Indigox.Common.Membership.NHibernateImpl
{
    public class ReportingHierarchyUser
    {
        public int ReportingHierarchyID { get; set; }
        public string UserID { get; set; }
        public string ManagerID { get; set; }

        public override bool Equals( object obj )
        {
            if ( ( obj == null ) || ( !( obj is ReportingHierarchyUser ) ) )
                return false;
            ReportingHierarchyUser other = (ReportingHierarchyUser)obj;
            return ( this.ReportingHierarchyID == other.ReportingHierarchyID ) &&
                   ( this.UserID == other.UserID );
        }

        public override int GetHashCode()
        {
            int code = 31;
            code ^= this.ReportingHierarchyID.GetHashCode();
            code ^= this.UserID.GetHashCode();
            return code;
        }
    }
}