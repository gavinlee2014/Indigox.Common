using System;
using System.Collections.Generic;
using Indigox.Common.DomainModels.Interface.Entity;

namespace Indigox.Common.Membership.Interfaces
{
    /// <summary>
    /// UUV 中所有实体都是一个 Principal
    /// </summary>
    public interface IPrincipal : IEntity
    {
        string ID { get; }
        string Name { get; }
        string DisplayName { get; }
        string FullName { get; }
        string Email { get; }
        string MailDatabase { get; }
        string Description { get; }
        double OrderNum { get; }
        bool Enabled { get; }
        bool Deleted { get; }
        DateTime CreateTime { get; }
        DateTime ModifyTime { get; }
        IList<IContainer> MemberOf { get; }

        bool IsMemberOf( IContainer container );

        IDictionary<string, string> ExtendProperties { get; }
    }
}