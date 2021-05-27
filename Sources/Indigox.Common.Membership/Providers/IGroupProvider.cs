using System;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Providers
{
    public interface IGroupProvider
    {
        IGroup GetGroupByID( string id );
    }
}