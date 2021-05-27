using System.Reflection;

namespace Indigox.Common.Utilities.Test.Assembly
{
    public class ClassWithArgumentConstructor
    {
        ConstructorType _constructorType = ConstructorType.NonArgument;

        public enum ConstructorType
        {
            NonArgument,
            WithArgument_String_Int,
            WithArgument_Object_Int,
            WithArgument_Object,
        }

        public ConstructorType CreatedBy
        {
            get { return _constructorType; }
        }

        public ClassWithArgumentConstructor ()
        {
            MethodUtil.PrintCallingMethodInfo();
            _constructorType = ConstructorType.NonArgument;
        }

        public ClassWithArgumentConstructor ( string s1, int i1 )
        {
            MethodUtil.PrintCallingMethodInfo();
            _constructorType = ConstructorType.WithArgument_String_Int;
        }

        public ClassWithArgumentConstructor ( object o1, int i1 )
        {
            MethodUtil.PrintCallingMethodInfo();
            _constructorType = ConstructorType.WithArgument_Object_Int;
        }

        public ClassWithArgumentConstructor ( object o1 )
        {
            MethodUtil.PrintCallingMethodInfo();
            _constructorType = ConstructorType.WithArgument_Object;
        }
    }
}
