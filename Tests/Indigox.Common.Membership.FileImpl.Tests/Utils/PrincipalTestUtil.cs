using System;
using System.Collections.Generic;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership.FileImpl.NUnitTest.Utils
{
    internal class PrincipalTestUtil
    {
        public static readonly string PrincipalSplitString = ";";

        private static readonly char[] PrincipalSplitChars = new char[] { ';' };

        public static T GetPrincipal<T>( string token )
            where T : IPrincipal
        {
            try
            {
                return (T)(IPrincipal)Principal.GetPrincipalByID( token );
            }
            catch ( Exception )
            {
                return (T)(IPrincipal)OrganizationalPerson.GetOrganizationalPersonByAccount( token );
            }
        }

        public static IList<T> GetPrincipals<T>( string tokens )
            where T : IPrincipal
        {
            if ( tokens == null )
            {
                return null;
            }

            List<T> list = new List<T>();
            if ( tokens == string.Empty )
            {
                return list;
            }

            string[] tokensArray = tokens.Split( PrincipalSplitChars );
            foreach ( string token in tokensArray )
            {
                list.Add( GetPrincipal<T>( token ) );
            }
            return list;
        }

        public static void Sort<T>( IList<T> list )
            where T : IPrincipal
        {
            if ( list is List<T> )
            {
                ( (List<T>)list ).Sort( new PrincipalComparer<T>() );
            }
            else
            {
                throw new NotSupportedException( "Only support List<T>." );
            }
        }
    }
}