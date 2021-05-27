using System;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.Providers
{
    public interface IPrincipalProvider
    {
        IPrincipal GetPrincipalByID( string id );
    }
}