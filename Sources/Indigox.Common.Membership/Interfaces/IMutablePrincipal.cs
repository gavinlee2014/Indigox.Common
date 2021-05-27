using System;
using System.Collections.Generic;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IMutablePrincipal : IPrincipal
    {
        new string ID { get; set; }
        new string Name { get; set; }
        new string DisplayName { get; set; }
        new string FullName { get; set; }
        new string Email { get; set; }
        new string Description { get; set; }
        new double OrderNum { get; set; }
        new bool Enabled { get; set; }
        new bool Deleted { get; set; }
        new DateTime CreateTime { get; set; }
        new DateTime ModifyTime { get; set; }
        new string MailDatabase { get; set; }

        new IDictionary<String, String> ExtendProperties { get; set; }

        void AddMemberOf( IContainer container );

        void RemoveMemberOf( IContainer container );

        void ClearMemberOf();
    }
}