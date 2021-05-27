using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.DomainModels.Interface.Identity;

namespace Indigox.Common.DomainModels.Interface.Entity
{
    public interface IEntity
    {
        IObjectIdentity GetObjectIdentity();
    }
}
