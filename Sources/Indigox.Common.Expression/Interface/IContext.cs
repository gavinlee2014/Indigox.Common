using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Expression.Interface
{
    public interface IContext
    {
        IPropertySet Properties { get; }
    }
}
