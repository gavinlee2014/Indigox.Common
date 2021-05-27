using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IMutableOrganizationalPerson : IOrganizationalPerson, IMutableUser, IMutableOrganizationalHolder
    {
    }
}