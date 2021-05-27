using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.DomainModels.Interface.Identity
{
    public interface IObjectIdentity
    {
        string TypeName { get; }
        string Identifier { get; }
    }
}
