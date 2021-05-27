using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IUser : IPrincipal
    {
        string AccountName { get; }
        string IdCard { get; }
        string Title { get; }
        string Mobile { get; }
        string Telephone { get; }
        string Fax { get; }
        string OtherContact { get; }
        string Profile { get; }
        int Level { get; }
    }
}