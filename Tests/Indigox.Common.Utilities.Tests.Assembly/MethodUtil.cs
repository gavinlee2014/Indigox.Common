using System;
using System.Diagnostics;
using System.Reflection;

namespace Indigox.Common.Utilities.Test.Assembly
{
    public class MethodUtil
    {
        public static void PrintCallingMethodInfo ()
        {
            // get call stack
            StackTrace stackTrace = new StackTrace();
            // get calling method name
            MethodBase callingMethod = stackTrace.GetFrame( 1 ).GetMethod();

            string methodNameAndArgs = GetMethodInfo( callingMethod );
            Console.WriteLine( "call method: " + methodNameAndArgs );
        }

        public static void PrintMethodInfo ( MethodBase method )
        {
            string methodNameAndArgs = GetMethodInfo( method );
            Console.WriteLine( methodNameAndArgs );
        }

        private static string GetMethodInfo ( MethodBase method )
        {
            return string.Format( "{1} {0}", method.ToString(), GetMethodModifier( method ) );
        }

        private static string GetMethodModifier ( MethodBase method )
        {
            string modifier = "?";

            if ( method.IsPublic )
                modifier = "public";
            else if ( method.IsFamilyOrAssembly )
                modifier = "protected internal";
            else if ( method.IsAssembly )
                modifier = "internal";
            else if ( method.IsPrivate )
                modifier = "private";
            else if ( method.IsFamily )
                modifier = "protected";

            if ( method.IsStatic )
                modifier += " static";

            if ( method.IsFinal )
                modifier += " sealed";
            else if ( method.IsVirtual )
                modifier += " virtual";
            else if ( method.IsAbstract )
                modifier += " abstract";

            return modifier;
        }

    }
}
