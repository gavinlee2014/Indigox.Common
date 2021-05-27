using System;
using System.Data;
using Indigox.Common.Logging;
using Indigox.Common.NHibernate.Extension.Serialization;
using Indigox.Common.NHibernate.Extension.Utils;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Type;
using NHibernate.UserTypes;

namespace Indigox.Common.NHibernate.Extension.UserTypes
{
    [Serializable]
    public class AnyType : ICompositeUserType
    {
        private static readonly StringType XmlType = (StringType)TypeFactory.GetStringType( int.MaxValue );
        private static readonly StringType NVarcharMaxType = (StringType)TypeFactory.GetStringType( int.MaxValue );

        public object Assemble( object cached, ISessionImplementor session, object owner )
        {
            return DeepCopy( cached );
        }

        public object DeepCopy( object value )
        {
            return value;
        }

        public object Disassemble( object value, ISessionImplementor session )
        {
            return DeepCopy( value );
        }

        bool ICompositeUserType.Equals( object x, object y )
        {
            if ( x == y ) return true;
            if ( x == null || y == null ) return false;
            return x.Equals( y );
        }

        int ICompositeUserType.GetHashCode( object x )
        {
            if ( x == null ) return 0;
            return x.GetHashCode();
        }

        public object GetPropertyValue( object component, int property )
        {
            if ( component == null )
            {
                return null;
            }

            switch ( property )
            {
                case 0:
                    return GetReturnType( component );

                case 1:
                    return GetSimpleValue( component );

                case 2:
                    return GetXmlValue( component );

                default:
                    throw new OverflowException( "property index must between 0-2" );
            }
        }

        public bool IsMutable
        {
            get { return false; }
        }

        public object NullSafeGet( IDataReader dr, string[] names, ISessionImplementor session, object owner )
        {
            if ( dr == null )
            {
                return null;
            }

            string returnTypeColumn = names[ 0 ];
            string simpleValueColumn = names[ 1 ];
            string xmlValueColumn = names[ 2 ];

            string returnType = (string)NHibernateUtil.String.NullSafeGet( dr, returnTypeColumn, session, owner );
            string simpleValue = (string)NHibernateUtil.String.NullSafeGet( dr, simpleValueColumn, session, owner );
            string xmlValue = (string)NHibernateUtil.String.NullSafeGet( dr, xmlValueColumn, session, owner );

            if ( returnType == null )
            {
                return null;
            }

            Type returnTypeClass = TypeName.Instance.GetType( returnType );

            if ( SimpleType.Instance.IsSimpleType( returnTypeClass ) )
            {
                return SimpleType.Instance.ConvertFromString( simpleValue, returnTypeClass );
            }
            else
            {
                return AnyTypeXmlSerializer.Deserialize( xmlValue );
            }
        }

        public void NullSafeSet( IDbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session )
        {
            string returnType = null;
            string simpleValue = null;
            string xmlValue = null;

            if ( value != null )
            {
                returnType = GetReturnType( value );
                simpleValue = GetSimpleValue( value );
                xmlValue = GetXmlValue( value );
            }

            NHibernateUtil.String.NullSafeSet( cmd, returnType, index, session );
            NHibernateUtil.String.NullSafeSet( cmd, simpleValue, index + 1, session );

            try
            {
                XmlType.NullSafeSet( cmd, xmlValue, index + 2 );
            }
            catch ( Exception ex )
            {
                string msg = string.Format( "XML太长(length:{0}).", xmlValue.Length );
                Log.Error( msg + "\r\n" + xmlValue );
                throw new Exception( msg, ex );
            }
        }

        public string[] PropertyNames
        {
            get { return new string[ 3 ] { "ReturnType", "SimpleValue", "XmlValue" }; }
        }

        public IType[] PropertyTypes
        {
            get
            {
                var xmlType = (StringType)TypeFactory.GetStringType( int.MaxValue );
                return new IType[ 3 ] { NHibernateUtil.String, NVarcharMaxType, XmlType };
            }
        }

        public object Replace( object original, object target, ISessionImplementor session, object owner )
        {
            return DeepCopy( original );
        }

        public Type ReturnedClass
        {
            get { return typeof( object ); }
        }

        public void SetPropertyValue( object component, int property, object value )
        {
            throw new NotImplementedException();
        }

        private string GetReturnType( object component )
        {
            Type type = component.GetType();
            return TypeName.Instance.GetTypeName( type );
        }

        private string GetSimpleValue( object component )
        {
            if ( component != null && SimpleType.Instance.IsSimpleType( component.GetType() ) )
            {
                return SimpleType.Instance.ConvertToString( component );
            }
            return null;
        }

        private string GetXmlValue( object component )
        {
            if ( component != null && !SimpleType.Instance.IsSimpleType( component.GetType() ) )
            {
                return AnyTypeXmlSerializer.Serialize( component );
            }
            return null;
        }
    }
}