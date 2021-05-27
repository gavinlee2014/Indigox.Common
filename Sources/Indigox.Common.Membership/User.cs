using System;
using Indigox.Common.EventBus;
using Indigox.Common.Membership.Events;
using Indigox.Common.Membership.Interfaces;
using Indigox.Common.Membership.Providers;

namespace Indigox.Common.Membership
{
    [Serializable]
    public class User : Principal, IUser, IMutableUser
    {
        public static IUser GetUserByID( string id )
        {
            return ProviderFactories.GetFactory().GetUserProvider().GetUserByID( id );
        }

        public static IUser GetUserByAccount( string account )
        {
            return ProviderFactories.GetFactory().GetUserProvider().GetUserByAccount( account );
        }

        private string accountName;
        private string idCard;
        private string title;
        private string mobile;
        private string telephone;
        private string fax;
        private string otherContact;
        private string profile;
        private int level;
             

        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName
        {
            get { return accountName; }
            set
            {
                if ( accountName != value )
                {
                    string oldValue = accountName;
                    accountName = value;
                    TriggerPropertyChangedEvent( "AccountName", oldValue, accountName );
                }
            }
        }
        
        public string IdCard
        {
            get { return idCard; }
            set
            {
                if (idCard != value)
                {
                    idCard = value;
                }
            }
        }

        /// <summary>
        /// 头像
        /// </summary>
        public string Profile
        {
            get { return profile; }
            set
            {
                if (profile != value)
                {
                    string oldValue = profile;
                    profile = value;
                    TriggerPropertyChangedEvent("Profile", oldValue, profile);
                }
            }
        }

        /// <summary>
        /// 职称
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                if ( title != value )
                {
                    string oldValue = title;
                    title = value;
                    TriggerPropertyChangedEvent( "Title", oldValue, title );
                }
            }
        }

        /// <summary>
        /// 职级
        /// </summary>
        public int Level {
            get { return level; }
            set { level = value; }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile
        {
            get { return mobile; }
            set
            {
                if ( mobile != value )
                {
                    string oldValue = mobile;
                    mobile = value;
                    TriggerPropertyChangedEvent( "Mobile", oldValue, mobile );
                }
            }
        }

        /// <summary>
        /// 办公电话
        /// </summary>
        public string Telephone
        {
            get { return telephone; }
            set
            {
                if ( telephone != value )
                {
                    string oldValue = telephone;
                    telephone = value;
                    TriggerPropertyChangedEvent( "Telephone", oldValue, telephone );
                }
            }
        }

        public string Fax 
        {
            get { return fax; }
            set
            {
                if (fax != value)
                {
                    string oldValue = fax;
                    fax = value;
                    TriggerPropertyChangedEvent("Fax", oldValue, fax);
                }
            }
        }

        public string OtherContact 
        {
            get { return otherContact; }
            set
            {
                if (otherContact != value)
                {
                    string oldValue = otherContact;
                    otherContact = value;
                    TriggerPropertyChangedEvent("OtherContact", oldValue, otherContact);
                }
            }
        }

        protected override void TriggerPropertyChangedEvent( string propertyName, object oldValue, object newValue )
        {
            //EventTrigger.Trigger( this, new UserPropertyChangedEvent( this.ID, propertyName, oldValue, newValue ) );
        }

        protected override void TriggerRemovedFromEvent(IContainer container)
        {
            if (container is IOrganizationalUnit)
            {
                EventTrigger.Trigger(this, new UserRemovedFromOrganizationalUnitEvent(this, container as IOrganizationalUnit));
            }
            else if (container is OrganizationalRole)
            {
                EventTrigger.Trigger(this, new UserRemovedFromOrganizationalRoleEvent(this, container as OrganizationalRole));
            }
            else if (container is Group)
            {
                EventTrigger.Trigger(this, new UserRemovedFromGroupEvent(this, container as Group));
            }
        }

        protected override void TriggerAddedToEvent(IContainer container)
        {
            if (container is IOrganizationalUnit)
            {
                EventTrigger.Trigger(this, new UserAddedToOrganizationalUnitEvent(this, container as IOrganizationalUnit));
            }
            else if (container is OrganizationalRole)
            {
                EventTrigger.Trigger(this, new UserAddedToOrganizationaRoleEvent(this, container as OrganizationalRole));
            }
            else if (container is Group)
            {
                EventTrigger.Trigger(this, new UserAddedToGroupEvent(this, container as Group));
            }
        }
    }
}