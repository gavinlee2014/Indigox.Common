using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.DomainModels.Interface.Generator
{
    public interface IIdGenerator
    {
        T GetNextID<T>(string name);
    }
}
