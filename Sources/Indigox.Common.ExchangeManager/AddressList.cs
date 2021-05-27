using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Indigox.Common.ExchangeManager
{
    public class AddressList : BaseEntity
    {
        public AddressList()
            : base(new Hashtable())
        {
        }

        public AddressList(Hashtable properties)
            : base(properties)
        {
        }
    }
}
