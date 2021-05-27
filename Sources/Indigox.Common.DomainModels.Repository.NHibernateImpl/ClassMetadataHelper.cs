using System;
using Indigox.Common.NHibernateFactories;
using NHibernate;
using NHibernate.Metadata;
using NHibernate.Persister.Collection;
using NHibernate.Type;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl
{
    internal class ClassMetadataHelper
    {
        public static string GetCollectionKeyColumn( Type type, string prop )
        {
            Type instanceType = InstanceTypeMapping.GetMappedClass( type );

            ISessionFactory factory = SessionFactories.Instance.Get( instanceType.Assembly ).CurrentSessionFactory;
            IClassMetadata metadata = factory.GetClassMetadata( instanceType );
            ICollectionMetadata collectionMetadata = factory.GetCollectionMetadata( metadata.EntityName + "." + prop );

            BasicCollectionPersister persister = (BasicCollectionPersister)collectionMetadata;

            return persister.KeyColumnNames[ 0 ];
        }

        public static string GetCollectionElementColumn( Type type, string prop )
        {
            Type instanceType = InstanceTypeMapping.GetMappedClass( type );

            ISessionFactory factory = SessionFactories.Instance.Get( instanceType.Assembly ).CurrentSessionFactory;
            IClassMetadata metadata = factory.GetClassMetadata( instanceType );
            ICollectionMetadata collectionMetadata = factory.GetCollectionMetadata( metadata.EntityName + "." + prop );

            BasicCollectionPersister persister = (BasicCollectionPersister)collectionMetadata;

            return persister.ElementColumnNames[ 0 ];
        }

        public static IType GetCollectionElementType( Type type, string prop )
        {
            Type instanceType = InstanceTypeMapping.GetMappedClass( type );

            ISessionFactory factory = SessionFactories.Instance.Get( instanceType.Assembly ).CurrentSessionFactory;
            IClassMetadata metadata = factory.GetClassMetadata( instanceType );
            ICollectionMetadata collectionMetadata = factory.GetCollectionMetadata( metadata.EntityName + "." + prop );

            BasicCollectionPersister persister = (BasicCollectionPersister)collectionMetadata;
            return persister.ElementType;
        }

        public static string GetIdentityProperty( Type type )
        {
            Type instanceType = InstanceTypeMapping.GetMappedClass( type );

            ISessionFactory factory = SessionFactories.Instance.Get( instanceType.Assembly ).CurrentSessionFactory;
            IClassMetadata metadata = factory.GetClassMetadata( instanceType );
            return metadata.IdentifierPropertyName;
        }

        public static object GetIdentityValue( Type type, object val )
        {
            Type instanceType = InstanceTypeMapping.GetMappedClass( type );

            ISessionFactory factory = SessionFactories.Instance.Get( instanceType.Assembly ).CurrentSessionFactory;
            IClassMetadata metadata = factory.GetClassMetadata( instanceType );
            return metadata.GetIdentifier( val, EntityMode.Map );
        }
    }
}
