using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Indigox.Common.DomainModels.Identity;
using Indigox.Common.DomainModels.Interface.Identity;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;
using Indigox.Common.Membership.Services;

namespace Indigox.Common.Membership
{
    [Serializable]
    public abstract class Principal : IPrincipal, IMutablePrincipal
    {
        public static IPrincipal GetPrincipalByID( string id )
        {
            IPrincipal principal = ProviderFactories.GetFactory().GetPrincipalProvider().GetPrincipalByID( id );
            return principal;
        }

        [Obsolete( "Rename to GetOrganizationalHoldersByID" )]
        public static List<IPrincipal> GetPrincipalByID( string id, IOrganizationalHolder holder )
        {
            return GetPrincipalsByOrganizationalHolder( id, holder );
        }

        public static List<IPrincipal> GetPrincipalsByOrganizationalHolder( string id, IOrganizationalHolder holder )
        {
            List<IPrincipal> list = new List<IPrincipal>();
            if ( id.StartsWith( "PS" ) )
            {
                MembershipService service = new MembershipService();
                IRole role = Role.GetRoleByID( id );
                IList<IOrganizationalRole> organizationalRoles = service.GetOrganizationalRolesFromRole( holder, role );
                foreach ( IOrganizationalRole organizationalRole in organizationalRoles )
                {
                    list.Add( organizationalRole );
                }
            }
            else
            {
                list.Add( GetPrincipalByID( id ) );
            }
            return list;
        }

        private string id;
        private string name;
        private string displayName;
        private string fullName;
        private string email;
        private string mailDatabase;
        private string description;
        private bool enabled = true;
        private bool deleted = false;
        private DateTime createTime = DateTime.Now;
        private DateTime modifyTime = DateTime.Now;
        private double orderNum = 1.001;
        private IDictionary<string, string> extendProperties = new Dictionary<string, string>();

        [NonSerialized]
        protected IList<IContainer> memberOf = new List<IContainer>();

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string MailDatabase
        {
            get { return mailDatabase; }
            set { mailDatabase = value; }
        }

        public IDictionary<string, string> ExtendProperties
        {
            get { return extendProperties; }
            set { extendProperties = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public double OrderNum
        {
            get { return orderNum; }
            set { orderNum = value; }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        public DateTime ModifyTime
        {
            get { return modifyTime; }
            set { modifyTime = value; }
        }

        public bool IsMemberOf( IContainer unit )
        {
            return unit.ContainsMember( this );
        }

        public override bool Equals( object obj )
        {
            Principal el = obj as Principal;
            if ( el == null )
            {
                return false;
            }
            return this.ID == el.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format( "{2}[{0}]:{1}", this.ID, this.GetType().Name, this.Name );
        }

        public IList<IContainer> MemberOf
        {
            get
            {
                return new ReadOnlyCollection<IContainer>( this.memberOf );
            }
        }

        public void AddMemberOf( IContainer container )
        {
            Container concreateContainer = container as Container;
            concreateContainer.InternalAddMember( this );

            if ( !this.memberOf.Contains( container ) )
            {
                this.memberOf.Add( container );
                this.TriggerAddedToEvent( container );
            }
        }

        public void RemoveMemberOf( IContainer container )
        {
            Container concreateContainer = container as Container;
            concreateContainer.InternalRemoveMember( this );

            if ( this.memberOf.Contains( container ) )
            {
                this.memberOf.Remove( container );
                this.TriggerRemovedFromEvent( container );
            }
        }

        public void ClearMemberOf()
        {
            List<IContainer> memberOf = new List<IContainer>( this.memberOf );
            foreach ( IContainer container in memberOf )
            {
                this.RemoveMemberOf( container );
            }
        }

        public IObjectIdentity GetObjectIdentity()
        {
            return new ObjectIdentity( typeof( IPrincipal ).FullName, this.ID );
        }

        protected abstract void TriggerRemovedFromEvent( IContainer container );

        protected abstract void TriggerAddedToEvent( IContainer container );

        protected abstract void TriggerPropertyChangedEvent( string propertyName, object oldValue, object newValue );
    }
}