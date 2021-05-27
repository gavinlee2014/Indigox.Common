using System;
using Indigox.Common.Membership.Configuration;
using Indigox.Common.Utilities;

namespace Indigox.Common.Membership.Providers
{
    public sealed class ProviderFactories
    {
        private static IProviderFactory _ProviderFactory;

        public static IProviderFactory GetFactory()
        {
            if ( _ProviderFactory == null )
            {
                _ProviderFactory = (IProviderFactory)ReflectUtil.CreateInstance(
                    MembershipSection.Default.ProviderFactory.ProviderFactoryType );
            }
            return _ProviderFactory;
        }
    }
}