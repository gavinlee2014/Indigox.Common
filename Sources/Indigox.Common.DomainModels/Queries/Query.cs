using System.Collections.Generic;
using Indigox.Common.DomainModels.Interface.Specifications;

namespace Indigox.Common.DomainModels.Queries
{
    public class Query
    {
        private ISpecification specifications;
        private int start = 0;
        private int limit = -1;
        private IList<OrderBy> sortOrder;

        public ISpecification Specifications
        {
            get { return specifications; }
            set { specifications = value; }
        }

        public int Start
        {
            get { return start; }
            set { start = value; }
        }

        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public IList<OrderBy> SortOrder
        {
            get
            {
                EnsureSortOrderCreated();
                return sortOrder;
            }
            set
            {
                sortOrder = value;
            }
        }

        private void EnsureSortOrderCreated()
        {
            if ( sortOrder == null )
            {
                sortOrder = new List<OrderBy>();
            }
        }

        public static Query NewQuery
        {
            get { return new Query(); }
        }

        public Query FindByCondition( ISpecification speci )
        {
            this.specifications = speci;
            return this;
        }

        public Query StartFrom( int startIndex )
        {
            this.start = startIndex;
            return this;
        }

        public Query LimitTo( int limit )
        {
            this.limit = limit;
            return this;
        }

        public Query OrderByAsc( params string[] fieldNames )
        {
            EnsureSortOrderCreated();
            foreach ( string fieldName in fieldNames )
            {
                this.sortOrder.Add( OrderBy.NewOrder( fieldName, OrderType.Asc ) );
            }
            return this;
        }

        public Query OrderByDesc( params string[] fieldNames )
        {
            EnsureSortOrderCreated();
            foreach ( string fieldName in fieldNames )
            {
                this.sortOrder.Add( OrderBy.NewOrder( fieldName, OrderType.Desc ) );
            }
            return this;
        }
    }
}
