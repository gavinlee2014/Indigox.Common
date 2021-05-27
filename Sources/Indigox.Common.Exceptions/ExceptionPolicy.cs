using System;

namespace Indigox.Common.Exceptions
{
    public static class ExceptionPolicy
    {
        public static void Handle(Exception ex)
        {
        }

        public static void Handle(Exception ex, string policyName)
        {
        }

        public static void Handle(Exception ex, Type type)
        {
        }
    }
}
