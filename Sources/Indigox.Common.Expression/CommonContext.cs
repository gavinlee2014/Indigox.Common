using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.Expression.Interface;

namespace Indigox.Common.Expression
{
    public class CommonContext : IContext
    {
        public CommonContext()
        {
            this.properties = new CommonPropertySet();
        }

        private CommonPropertySet properties;

        public virtual IPropertySet Properties
        {
            get { return properties; }
        }
    }
}
