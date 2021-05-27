using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.UserTypes;
using NHibernate;
using NHibernate.Type;
using NHibernate.Engine;
using Indigox.Common.DomainModels.Interface.Identity;
using System.Data;
using Indigox.Common.Utilities;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.UserTypes
{
    public class KeyType : ICompositeUserType, IParameterizedType
    {
        private string connectinName;

        public object Assemble(object cached, ISessionImplementor session, object owner)
        {
            return DeepCopy(cached);
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value, ISessionImplementor session)
        {
            return DeepCopy(value);
        }

        bool ICompositeUserType.Equals(object x, object y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;
            return x.Equals(y);
        }

        int ICompositeUserType.GetHashCode(object x)
        {
            if (x == null) return 0;
            return x.GetHashCode();
        }

        public object GetPropertyValue(object component, int property)
        {
            if (component == null)
            {
                return null;
            }

            IKey key = (IKey)component;

            switch (property)
            {
                case 0:
                    return ClassNameFactory.GetClassID(key.GetType(), this.connectinName);

                case 1:
                    return key.Identifier;

                default:
                    throw new OverflowException("property index must between 0-1");
            }
        }

        public bool IsMutable
        {
            get { return false; }
        }

        public object NullSafeGet(IDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            if (dr == null)
            {
                return null;
            }

            string keyTypeColumn = names[0];
            string keyIdentifierColumn = names[1];

            if (dr.IsDBNull(dr.GetOrdinal(keyTypeColumn)))
            {
                return null;
            }
            int keyTypeID = (int)NHibernateUtil.Int32.NullSafeGet(dr, keyTypeColumn, session, owner);
            string objectIdentifier = (string)NHibernateUtil.String.NullSafeGet(dr, keyIdentifierColumn, session, owner);

            IKey key = (IKey)ReflectUtil.CreateInstance(ClassNameFactory.GetClassName(keyTypeID, this.connectinName));
            key.Identifier = objectIdentifier;

            return key;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            if (value == null)
            {
                NHibernateUtil.Int32.NullSafeSet(cmd, null, index, session);
                NHibernateUtil.Int32.NullSafeSet(cmd, null, index + 1, session);
            }
            else
            {
                IKey key = (IKey)value;
                NHibernateUtil.Int32.NullSafeSet(cmd, ClassNameFactory.GetClassID(key.GetType(), this.connectinName), index + 0, session);
                NHibernateUtil.Int32.NullSafeSet(cmd, key.Identifier, index + 1, session);
            }
        }

        public string[] PropertyNames
        {
            get { return new string[2] { "KeyType", "KeyIdentifier" }; }
        }

        public IType[] PropertyTypes
        {
            get
            {
                return new IType[2] { NHibernateUtil.Int32, NHibernatePropertyType.IdentifierPropertyType };
            }
        }

        public object Replace(object original, object target, ISessionImplementor session, object owner)
        {
            return DeepCopy(original);
        }

        public Type ReturnedClass
        {
            get { return typeof(object); }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new NotImplementedException();
        }

        public void SetParameterValues(IDictionary<string, string> parameters)
        {
            if (parameters != null)
            {
                if (parameters.ContainsKey("ConnectionName"))
                {
                    this.connectinName = parameters["ConnectionName"];
                }
            }
        }
    }
}
