using System;

namespace Indigox.Common.Configuration.Web
{
    public interface IWarmUp
    {
        void OnApplicationStart();
    }
}