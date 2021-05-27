using System;

namespace Indigox.Common.Membership.Interfaces
{
    public interface IMutableUser : IUser, IMutablePrincipal
    {
        new string AccountName { get; set; }
        new string IdCard { get; set; }
        new string Title { get; set; }
        new string Mobile { get; set; }
        new string Telephone { get; set; }
        new string Fax { get; set; }
        new string OtherContact { get; set; }
        new string Profile { get; set; }
        new int Level { get; set; }
    }
}