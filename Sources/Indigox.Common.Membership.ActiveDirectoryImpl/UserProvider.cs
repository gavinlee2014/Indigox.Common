using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Indigox.Common.Membership.ActiveDirectoryImpl.ActiveDirectory;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.ActiveDirectoryImpl
{
    class UserProvider : IUserProvider
    {
        public IUser GetUserByID( string id )
        {
            IMutableUser user = new OrganizationalPerson()
            {
                ID = id,
                Name = id,
                FullName = id
            };
            return user;
        }

        public IUser GetUserByAccount( string account )
        {
            string[] propsToLoad = { "cn", "sAMAccountName" };

            SearchResult result = ADHelper.Instance.GetUserByID( account, propsToLoad );

            IMutableUser user = new OrganizationalPerson();
            user.Name = ADHelper.GetProperty<string>( result, "cn" );
            user.FullName = ADHelper.GetProperty<string>( result, "cn" );
            user.AccountName = account;
            return user;
        }

        public IList<IUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
