using System;
using System.Collections.Generic;

namespace Indigox.Common.DomainModels.Test.Models
{
    public abstract class Mammal : IMammal
    {
        public bool HasFang { get; set; }
    }
}
