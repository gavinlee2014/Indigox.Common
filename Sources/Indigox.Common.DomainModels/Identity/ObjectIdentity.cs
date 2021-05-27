using System;
using Indigox.Common.DomainModels.Interface.Identity;

namespace Indigox.Common.DomainModels.Identity
{
    public class ObjectIdentity : IObjectIdentity
    {
        private string typeName;
        private string identifier;

        protected ObjectIdentity()
        {
        }

        public ObjectIdentity( string typeName, string identifier )
        {
            this.typeName = typeName;
            this.identifier = identifier;
        }

        public string TypeName
        {
            get { return typeName; }
        }

        public string Identifier
        {
            get { return this.identifier; }
        }

        public override bool Equals( object obj )
        {
            if ( obj == null || !( obj is ObjectIdentity ) )
            {
                return false;
            }

            ObjectIdentity compareTo = obj as ObjectIdentity;

            return ( this.identifier.Equals( compareTo.Identifier ) && this.typeName.Equals( compareTo.TypeName ) );
        }

        public override int GetHashCode()
        {
            int code = 31;
            if ( this.Identifier != null )
                code ^= this.Identifier.GetHashCode();
            if ( this.TypeName != null )
                code ^= this.TypeName.GetHashCode();
            return code;
        }

        public override string ToString()
        {
            return string.Format( "{{identifier:{0}, type:{1}}}", this.Identifier, this.TypeName );
        }
    }
}