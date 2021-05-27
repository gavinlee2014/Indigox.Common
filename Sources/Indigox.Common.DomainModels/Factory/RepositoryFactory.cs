using System;
using System.Collections.Generic;
using Indigox.Common.DomainModels.Repository;
using Indigox.Common.DomainModels.Repository.Interface;

namespace Indigox.Common.DomainModels.Factory
{
    public sealed class RepositoryFactory
    {

        private static RepositoryFactory instance;

        public static RepositoryFactory Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = new RepositoryFactory();
                }
                return instance;

            }
        }
        private RepositoryFactory()
        {
        }

        private Type defaultRepositoryType = typeof( BasicRepository<> );

        private Dictionary<Type, Type> modelRepositoryTypeMapping = new Dictionary<Type, Type>();

        public IRepository<T> CreateRepository<T>()
        {
            Type type = GetDefaultRepositoryType(typeof(T)); 
            if ( modelRepositoryTypeMapping.ContainsKey( typeof( T ) ) )
            {
                type = modelRepositoryTypeMapping[ typeof( T ) ];
            }

            IRepository<T> repository = (IRepository<T>)Activator.CreateInstance( type );

            return repository;
        }

        public IRepository CreateRepository( Type entityType )
        {
            Type type = GetDefaultRepositoryType(entityType);
            if ( modelRepositoryTypeMapping.ContainsKey( entityType ) )
            {
                type = modelRepositoryTypeMapping[ entityType ];
            }

            IRepository repository = (IRepository)Activator.CreateInstance( type );

            return repository;
        }

        public void RegisterDefault( Type type )
        {
            this.defaultRepositoryType = type;
        }

        public void Register<T>( Type repositoryType )
        {
            if ( modelRepositoryTypeMapping.ContainsKey( typeof( T ) ) )
            {
                modelRepositoryTypeMapping[ typeof( T ) ] = repositoryType;
            }
            else
            {
                modelRepositoryTypeMapping.Add( typeof( T ), repositoryType );
            }
        }

        public void Register( Type entityType, Type repositoryType )
        {
            if ( modelRepositoryTypeMapping.ContainsKey( entityType ) )
            {
                modelRepositoryTypeMapping[ entityType ] = repositoryType;
            }
            else
            {
                modelRepositoryTypeMapping.Add( entityType, repositoryType );
            }
        }

        public void Unregister<T>()
        {
            if ( modelRepositoryTypeMapping.ContainsKey( typeof( T ) ) )
            {
                modelRepositoryTypeMapping.Remove( typeof( T ) );
            }
        }

        public void Unregister( Type entityType )
        {
            if ( modelRepositoryTypeMapping.ContainsKey( entityType ) )
            {
                modelRepositoryTypeMapping.Remove( entityType );
            }
        }

        private Type GetDefaultRepositoryType(Type type)
        {
            if (defaultRepositoryType == null)
            {
                return null;
            }
            else
            {
                if (defaultRepositoryType.IsGenericTypeDefinition)
                    return defaultRepositoryType.MakeGenericType(type);
                else
                    return defaultRepositoryType;
            }
        }
    }
}
