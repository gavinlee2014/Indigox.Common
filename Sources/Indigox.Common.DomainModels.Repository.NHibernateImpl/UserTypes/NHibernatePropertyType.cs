using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Type;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.UserTypes
{
    internal class NHibernatePropertyType
    {
        public static readonly StringType IdentifierPropertyType = (StringType)TypeFactory.GetStringType(800);
        public static readonly StringType XmlType = (StringType)TypeFactory.GetStringType(int.MaxValue);
        public static readonly StringType NVarcharMaxType = (StringType)TypeFactory.GetStringType(int.MaxValue);
    }
}
