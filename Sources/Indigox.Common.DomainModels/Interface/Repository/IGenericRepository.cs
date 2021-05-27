using System;
using System.Collections;
using System.Collections.Generic;
using Indigox.Common.DomainModels.Interface.Specifications;
using Indigox.Common.DomainModels.Queries;

namespace Indigox.Common.DomainModels.Repository.Interface
{
    public interface IRepository<T> : IRepository
    {
        new T Get( object key );
        new T First( Query query );
        new IList<T> Find( Query query );
        void Add( T model );
        void Update( T model );
        void Remove( T model );
    }
}
