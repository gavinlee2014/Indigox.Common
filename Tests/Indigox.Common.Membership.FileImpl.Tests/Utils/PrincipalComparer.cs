using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.FileImpl.NUnitTest.Utils
{
    internal class PrincipalComparer<T> : IComparer<T>
        where T : IPrincipal
    {
        public int Compare( T x, T y )
        {
            return StringComparer.CurrentCultureIgnoreCase.Compare( x.ID, y.ID );
        }
    }
}