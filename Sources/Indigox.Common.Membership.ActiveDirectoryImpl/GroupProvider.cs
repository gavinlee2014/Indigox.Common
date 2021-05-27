using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.ActiveDirectoryImpl
{
    class GroupProvider : IGroupProvider
    {
        public IGroup GetGroupByID( string id )
        {
            throw new NotImplementedException();
        }
    }
}
