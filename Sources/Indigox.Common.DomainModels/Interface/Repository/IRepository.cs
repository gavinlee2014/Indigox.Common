using System;
using System.Collections;
using System.Collections.Generic;
using Indigox.Common.DomainModels.Interface.Specifications;
using Indigox.Common.DomainModels.Queries;

namespace Indigox.Common.DomainModels.Repository.Interface
{
    public interface IRepository
    {
        Object Get( object key );
        Object First( Query query );
        IList Find( Query query );
        int GetTotalCount( Query query );
        void Add( Object model );
        void Update( Object model );
        void Remove( Object model );
    }
}
