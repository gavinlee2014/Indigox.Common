
namespace Indigox.Common.Utilities.Test.Assembly
{
    public class PropertyClass
    {
        public string property;

        public string PropertyGetterAndSetter
        {
            get
            {
                MethodUtil.PrintCallingMethodInfo();
                return property;
            }
            set
            {
                MethodUtil.PrintCallingMethodInfo();
                property = value;
            }
        }

        public string PropertyGetter
        {
            get
            {
                MethodUtil.PrintCallingMethodInfo();
                return property;
            }
        }

        public string PropertyGetterAndPrivateSetter
        {
            get
            {
                MethodUtil.PrintCallingMethodInfo();
                return property;
            }
            private set
            {
                MethodUtil.PrintCallingMethodInfo();
                property = value;
            }
        }


        public static string staticProperty;

        public static string StaticPropertyGetterAndSetter
        {
            get
            {
                MethodUtil.PrintCallingMethodInfo();
                return staticProperty;
            }
            set
            {
                MethodUtil.PrintCallingMethodInfo();
                staticProperty = value;
            }
        }
    }
}
