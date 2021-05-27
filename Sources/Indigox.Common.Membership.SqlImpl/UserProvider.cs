using System;
using System.Collections.Generic;
using Indigox.Common.Data.Interface;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership.SqlImpl
{
    public class UserProvider : IUserProvider
    {
        public IUser GetUserByAccount( string account )
        {
            IRecordSet recordSet = Module.Db.QueryText(
                "select * from member where account=@account",
                "@account varchar",
                account
            );
            if ( recordSet.Records.Count == 1 )
            {
                IRecord record = recordSet.Records[ 0 ];
                OrganizationalPerson user = new OrganizationalPerson();
                FillPrincipal( record, user );
                return user;
            }
            else if ( recordSet.Records.Count > 1 )
            {
                throw new Exception( "multi result finded by account : " + account );
            }
            else
            {
                return null;
            }
        }

        public IUser GetUserByID( string id )
        {
            IRecordSet recordSet = Module.Db.QueryText(
                "select * from member where [ID]=@id",
                "@id varchar",
                id
            );
            if ( recordSet.Records.Count == 1 )
            {
                IRecord record = recordSet.Records[ 0 ];
                OrganizationalPerson user = new OrganizationalPerson();
                FillPrincipal( record, user );
                return user;
            }
            else if ( recordSet.Records.Count > 1 )
            {
                throw new Exception( "multi result finded by id : " + id );
            }
            else
            {
                return null;
            }
        }

        public IList<IUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        protected void FillPrincipal( IRecord record, OrganizationalPerson user )
        {
            user.ID = record.GetString( "ID" );
            user.AccountName = record.GetString( "Account" );
            user.Name = record.GetString( "Name" );
            user.FullName = record.GetString( "FullName" );
            user.Email = record.GetString( "Email" );
        }
    }
}
