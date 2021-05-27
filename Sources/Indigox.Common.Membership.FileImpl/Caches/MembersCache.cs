using System;
using System.Collections.Generic;
using Indigox.Common.Logging;
using Indigox.Common.Membership.Exceptions;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.FileImpl.Caches
{
    internal class MembersCache
    {
        private static Dictionary<string, Principal> ix_id = new Dictionary<string, Principal>( StringComparer.CurrentCultureIgnoreCase );
        private static Dictionary<string, Principal> ix_account = new Dictionary<string, Principal>( StringComparer.CurrentCultureIgnoreCase );
        private static Dictionary<string, Principal> ix_type_id = new Dictionary<string, Principal>( StringComparer.CurrentCultureIgnoreCase );
        private static Dictionary<string, Principal> ix_type_account = new Dictionary<string, Principal>( StringComparer.CurrentCultureIgnoreCase );
        private static Dictionary<string, List<Container>> units_cache = new Dictionary<string, List<Container>>();
        private static Dictionary<string, string> delayed_set_organization = new Dictionary<string, string>();

        public const int type_organizationalUnit = 100;
        public const int type_corparation = 101;
        public const int type_company = 102;
        public const int type_department = 103;
        public const int type_section = 104;
        public const int type_user = 200;
        public const int type_organizationalPerson = 201;
        public const int type_group = 300;
        public const int type_organizationalRole = 400;
        public const int type_role = 500;

        public const string seperator = "#";

        public static OrganizationalPerson GetUserById( string id )
        {
            if ( !ix_type_id.ContainsKey( type_user + seperator + id ) )
            {
                throw new MemberNotFoundException( id, MemberNotFoundException.TYPE_ID );
            }
            return (OrganizationalPerson)ix_type_id[ type_user + seperator + id ];
        }

        public static OrganizationalPerson GetUserByAccount( string account )
        {
            if ( !ix_type_account.ContainsKey( type_user + seperator + account ) )
            {
                throw new MemberNotFoundException( account, MemberNotFoundException.TYPE_Account );
            }
            return (OrganizationalPerson)ix_type_account[ type_user + seperator + account ];
        }

        public static Principal GetPrincipalById( string id )
        {
            if ( !ix_id.ContainsKey( id ) )
            {
                throw new MemberNotFoundException( id, MemberNotFoundException.TYPE_ID );
            }
            return ix_id[ id ];
        }

        public static Principal GetPrincipalByTypeAndId( int type, string id )
        {
            if ( !ix_type_id.ContainsKey( type + seperator + id ) )
            {
                throw new MemberNotFoundException( id, MemberNotFoundException.TYPE_ID );
            }
            return ix_type_id[ type + seperator + id ];
        }

        public static IList<IUser> GetAllUsers()
        {
            List<IUser> list = new List<IUser>();
            foreach ( var item in ix_id.Values )
            {
                if ( item is IUser )
                {
                    list.Add( (IUser)item );
                }
            }
            return list;
        }

        public static IList<Container> GetAllUnits( string id )
        {
            if ( units_cache.ContainsKey( id ) )
            {
                return units_cache[ id ];
            }
            else
            {
                return new List<Container>();
            }
        }

        static MembersCache()
        {
            AddMembers();
            AddMembership();
        }

        private static void AddMembers()
        {
            using ( CsvFileReader reader = new CsvFileReader( @".\data\member.csv" ) )
            {
                string[] cols = reader.Read();

                while ( cols != null )
                {
                    string type = cols[ reader.GetOrdinal( "Type" ) ];
                    string id = cols[ reader.GetOrdinal( "ID" ) ];
                    string account = cols[ reader.GetOrdinal( "AccountName" ) ];
                    string name = cols[ reader.GetOrdinal( "Name" ) ];
                    string fullname = cols[ reader.GetOrdinal( "FullName" ) ];
                    string email = cols[ reader.GetOrdinal( "Email" ) ];
                    string org = cols[ reader.GetOrdinal( "Organization" ) ];
                    string enabled = cols[ reader.GetOrdinal( "IsEnabled" ) ];
                    string deleted = cols[ reader.GetOrdinal( "IsDeleted" ) ];

                    Principal principal = NewPrincipal( type, account );
                    principal.ID = id;
                    principal.Name = name;
                    principal.FullName = fullname;
                    principal.Email = email;
                    principal.Enabled = ( enabled == "1" );
                    principal.Deleted = ( deleted == "1" );
                    ix_id.Add( id, principal );
                    ix_type_id.Add( type + seperator + id, principal );
                    if ( type[ type.Length - 1 ] != '0' )
                    {
                        ix_type_id.Add( type.Substring( 0, type.Length - 1 ) + '0' + seperator + id, principal );
                    }

                    if ( !string.IsNullOrEmpty( account ) && !principal.Deleted )
                    {
                        if ( !ix_account.ContainsKey( account ) )
                        {
                            ix_account.Add( account, principal );
                            ix_type_account.Add( type + seperator + account, principal );
                            if ( type[ type.Length - 1 ] != '0' )
                            {
                                ix_type_account.Add( type.Substring( 0, type.Length - 1 ) + '0' + seperator + account, principal );
                            }
                        }
                        else
                        {
                            Log.Debug( "exists account: " + account );
                        }
                    }
                    if ( !string.IsNullOrEmpty( org ) && principal is IOrganizationalObject )
                    {
                        //( (IOrganizationalHolder)principal ).Organization = (OrganizationalUnit)ix_id[ org ];
                        delayed_set_organization.Add( principal.ID, org );
                    }

                    cols = reader.Read();
                }
            }

            foreach ( KeyValuePair<string, string> item in delayed_set_organization )
            {
                IOrganizationalObject organizationalObject = ( (IOrganizationalObject)GetPrincipalById( item.Key ) );
                IOrganizationalUnit organization = ( (IOrganizationalUnit)GetPrincipalById( item.Value ) );

                IMutableOrganizationalObject mutableOrganizationalObject = organizationalObject as IMutableOrganizationalObject;
                mutableOrganizationalObject.Organization = organization;
            }
        }

        private static void AddMembership()
        {
            using ( CsvFileReader reader = new CsvFileReader( @".\data\membership.csv" ) )
            {
                string[] cols = reader.Read();

                while ( cols != null )
                {
                    string parentID = cols[ reader.GetOrdinal( "Parent" ) ];
                    string childID = cols[ reader.GetOrdinal( "Child" ) ];
                    IMutableContainer parent = (IMutableContainer)ix_id[ parentID ];
                    IMutablePrincipal child = ix_id[ childID ];

                    // add child
                    try
                    {
                        parent.AddMember( child );
                    }
                    catch ( Exception )
                    {
                        Log.Debug( "parent[" + child.ID + "] is " + parent.GetType().Name );
                        Log.Debug( "child[" + child.ID + "] is " + child.GetType().Name );
                    }

                    // set groups cache
                    if ( !units_cache.ContainsKey( childID ) )
                    {
                        units_cache.Add( childID, new List<Container>() );
                    }
                    units_cache[ childID ].Add( (Container)parent );

                    cols = reader.Read();
                }
            }
        }

        private static Principal NewPrincipal( string type, string account )
        {
            Principal principal = null;
            switch ( int.Parse( type ) )
            {
                case type_user:
                    principal = new User();
                    ( (User)principal ).AccountName = account;
                    break;

                case type_organizationalPerson:
                    principal = new OrganizationalPerson();
                    ( (OrganizationalPerson)principal ).AccountName = account;
                    break;

                case type_organizationalUnit:
                    principal = new OrganizationalUnit();
                    break;

                case type_corparation:
                    principal = new Corporation();
                    break;

                case type_company:
                    principal = new Company();
                    break;

                case type_department:
                    principal = new Department();
                    break;

                case type_section:
                    principal = new Section();
                    break;

                case type_group:
                    principal = new Group();
                    break;

                case type_organizationalRole:
                    principal = new OrganizationalRole();
                    break;

                case type_role:
                    principal = new Role();
                    break;

                default:
                    throw new Exception( "Unkown type: " + type );
            }
            return principal;
        }
    }
}