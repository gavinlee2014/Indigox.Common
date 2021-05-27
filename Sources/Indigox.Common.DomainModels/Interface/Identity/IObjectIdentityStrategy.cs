using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.DomainModels.Interface.Identity
{
    public interface IObjectIdentityStrategy
    {
        IObjectIdentity GetObjectIdentity(object domainObject);
        IObjectIdentity CreateObjectIdentify(string typeName, object identifer);
        IObjectIdentity CreateObjectIdentify(object domainObject, object identifer);
    }
}
