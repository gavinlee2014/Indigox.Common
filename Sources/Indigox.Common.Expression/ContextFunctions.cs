using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Indigox.Common.Utilities;

namespace Indigox.Common.Expression
{
    internal class ContextFunctions
    {
        private Dictionary<string, MethodInfo> functions = new Dictionary<string, MethodInfo>();
        private Dictionary<string, object> instances = new Dictionary<string, object>();

        private void SetFunction( string prefix, string name, object instance, MethodInfo method )
        {
            string functionFullName = prefix + "::" + name;
            if ( functions.ContainsKey( functionFullName ) )
            {
                throw new Exception( "Duplicate function registe." );
            }
            functions.Add( functionFullName, method );
            instances.Add( functionFullName, instance );
        }

        public void SetFunction( string prefix, string name, Type type, string methodName )
        {
            MethodInfo method = ReflectUtil.GetMethod( type, methodName, null, BindingFlags.Public | BindingFlags.Static );
            SetFunction( prefix, name, null, method );
        }

        public void SetFunction( string prefix, string name, object instance, string methodName )
        {
            MethodInfo method = ReflectUtil.GetMethod( instance.GetType(), methodName, null, BindingFlags.Public | BindingFlags.Instance );
            SetFunction( prefix, name, instance, method );
        }

        public void SetFunctions( string prefix, Type type )
        {
            MethodInfo[] methods = type.GetMethods( BindingFlags.Public | BindingFlags.Static );
            foreach ( MethodInfo method in methods )
            {
                SetFunction( prefix, method.Name, null, method );
            }
        }

        public void SetFunctions( string prefix, object instance )
        {
            MethodInfo[] methods = instance.GetType().GetMethods( BindingFlags.Public | BindingFlags.Instance );
            foreach ( MethodInfo method in methods )
            {
                SetFunction( prefix, method.Name, instance, method );
            }
        }

        public MethodInfo GetFunction( string name )
        {
            if ( functions.ContainsKey( name ) )
            {
                return functions[ name ];
            }
            else
            {
                throw new Exception( "can't find function '" + name + "'" );
            }
        }

        public object GetInstance( string name )
        {
            if ( functions.ContainsKey( name ) )
            {
                return instances[ name ];
            }
            else
            {
                throw new Exception( "can't find function '" + name + "'" );
            }
        }
    }
}
