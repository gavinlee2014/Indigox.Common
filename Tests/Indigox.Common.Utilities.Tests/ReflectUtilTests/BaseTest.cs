using System;
using System.Reflection;
using Indigox.Common.Utilities.Test.Assembly;
using NUnit.Framework;

namespace Indigox.Common.Utilities.Test.ReflectUtilTests
{
    [Category( "Utilities" )]
    [Category( "Reflect" )]
    public class BaseTest
    {

        protected void PrintAllMethods()
        {
            Type type = typeof( MethodClass );
            do
            {
                Console.WriteLine( "\r\n************************\r\n{0}\r\n************************", type.AssemblyQualifiedName );
                MethodInfo[] methods = type.GetMethods( ReflectUtil.BindingFlags_All | BindingFlags.DeclaredOnly );
                foreach ( MethodInfo method in methods )
                {
                    MethodUtil.PrintMethodInfo( method );
                }

                type = type.BaseType;
            }
            while ( type != typeof( object ) );
        }
    }
}
