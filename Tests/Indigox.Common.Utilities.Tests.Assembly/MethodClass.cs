using System;

namespace Indigox.Common.Utilities.Test.Assembly
{
    public abstract class MethodClassBase
    {
        public virtual void VirtualMethod () { }

        public void NewMethod () { }

        public abstract void AbstractMethod ();
    }

    public class MethodClass : MethodClassBase
    {
        public static void StaticMethodNoArgs ()
        {
            MethodUtil.PrintCallingMethodInfo();
        }

        public static void StaticMethodWithArgs ( string name )
        {
            MethodUtil.PrintCallingMethodInfo();
            Console.WriteLine( "input name is : " + name );
        }

        public void MethodNoArgs ()
        {
            MethodUtil.PrintCallingMethodInfo();
        }

        public void MethodWithArgs ( string name )
        {
            MethodUtil.PrintCallingMethodInfo();
            Console.WriteLine( "input name is : " + name );
        }

        public int MethodWithReturnValue ( int x, int y )
        {
            MethodUtil.PrintCallingMethodInfo();
            return x + y;
        }

        protected internal void ProtectedInternalMethod () { }

        internal protected void InternalProtectedMethod () { }

        internal void InternalMethod () { }

        protected void ProtectedMethod () { }

        private void PrivateMethod () { }

        public new void NewMethod () { }

        public override string ToString ()
        {
            return base.ToString();
        }

        public override void AbstractMethod () { }
    }
}
