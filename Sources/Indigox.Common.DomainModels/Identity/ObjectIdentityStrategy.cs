using System;
using Indigox.Common.DomainModels.Interface.Entity;
using Indigox.Common.DomainModels.Interface.Identity;
using Indigox.Common.Utilities;

namespace Indigox.Common.DomainModels.Identity
{
    public class ObjectIdentityStrategy : IObjectIdentityStrategy
    {
        public IObjectIdentity GetObjectIdentity( object domainObject )
        {
            ArgumentAssert.NotNull( domainObject, "domainObject" );

            if ( domainObject is IEntity )
            {
                return ( (IEntity)domainObject ).GetObjectIdentity();
            }

            object identifer = ReflectUtil.GetProperty( domainObject, "ID" );

            if ( identifer == null )
            {
                throw new Exception( "Can not find property \"ID\"." );
            }

            return new ObjectIdentity( domainObject.GetType().FullName, identifer.ToString() );
        }

        public IObjectIdentity CreateObjectIdentify( string typeName, object identifer )
        {
            return new ObjectIdentity( typeName, identifer.ToString() );
        }

        public IObjectIdentity CreateObjectIdentify( object domainObject, object identifer )
        {
            return new ObjectIdentity( domainObject.GetType().FullName, identifer.ToString() );
        }
    }
}