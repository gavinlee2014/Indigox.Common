using System;
using System.Collections.Generic;

namespace Indigox.Common.DomainModels.Queries
{
    public class OrderBy
    {
        private string orderByField;

        public string OrderByField
        {
            get { return orderByField; }
            set { orderByField = value; }
        }
        private OrderType sortOrder;

        public OrderType SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        public static OrderBy NewOrder( string fieldName, OrderType sort )
        {
            OrderBy orderBy = new OrderBy();
            orderBy.orderByField = fieldName;
            orderBy.sortOrder = sort;
            return orderBy;
        }
    }

    public enum OrderType
    {
        /// <summary>
        /// 顺序
        /// </summary>
        Asc,
        /// <summary>
        /// 逆序
        /// </summary>
        Desc,
    }
}
