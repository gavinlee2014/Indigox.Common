using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Indigox.Common.Logging;
using Indigox.Common.Membership.Interfaces;

namespace Indigox.Common.Membership
{
    [Serializable]
    public abstract class Container : Principal, IContainer, IMutableContainer
    {
        public static IContainer GetContainerByID( string id )
        {
            IContainer container = Principal.GetPrincipalByID( id ) as IContainer;
            return container;
        }

        public Container()
        {
            this.members = new List<IPrincipal>();
        }

        [NonSerialized]
        protected IList<IPrincipal> members;

        [NonSerialized]
        private IPrincipal owner;

        [XmlIgnore]
        public IPrincipal Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public IList<IPrincipal> Members
        {
            get
            {
                return new ReadOnlyCollection<IPrincipal>( this.members );
            }
        }

        private IList<T> GetMembers<T>()
        {
            List<T> members = new List<T>();
            foreach ( IPrincipal principal in this.members )
            {
                if ( principal is T )
                {
                    members.Add( (T)principal );
                }
            }
            return members.AsReadOnly();
        }

        public virtual void AddMember( IPrincipal member )
        {
            ( (IMutablePrincipal)member ).AddMemberOf( this );
        }

        public virtual void RemoveMember( IPrincipal member )
        {
            ( (IMutablePrincipal)member ).RemoveMemberOf( this );
        }

        internal void InternalAddMember( IPrincipal member )
        {
            this.members.Add( member );
        }

        internal void InternalRemoveMember( IPrincipal member )
        {
            this.members.Remove( member );
        }

        public virtual void ClearMembers()
        {
            List<IPrincipal> members = new List<IPrincipal>( this.members );
            foreach ( IPrincipal member in members )
            {
                this.RemoveMember( member );
            }
        }

        public bool ContainsMember( IPrincipal principal )
        {
            return members.Contains( principal );
        }

        public IList<IPrincipal> GetAllMembers()
        {
            List<IPrincipal> allMembers = new List<IPrincipal>();
            allMembers.AddRange( this.Members );

            IList<IContainer> units = GetMembers<IContainer>();
            foreach ( IContainer unit in units )
            {
                allMembers.AddRange( unit.GetAllMembers() );
            }

            return allMembers;
        }

        public IList<IUser> GetAllUsers()
        {
            DateTime starttime = DateTime.Now;

            List<IUser> allUserMembers = new List<IUser>();

            IList<IUser> users = GetMembers<IUser>();
            allUserMembers.AddRange( users );

            IList<IContainer> units = GetMembers<IContainer>();
            foreach ( IContainer unit in units )
            {
                foreach ( IUser user in unit.GetAllUsers() )
                {
                    if ( !allUserMembers.Contains( user ) )
                        allUserMembers.Add( user );
                }
            }

            Log.Debug( string.Format( "Group[{1}].GetAllUserMembers spend : {0} ms.",
                                      ( DateTime.Now - starttime ).TotalMilliseconds,
                                      this.ID ) );

            return allUserMembers;
        }
    }
}