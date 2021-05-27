using System;
using System.Collections.Generic;

namespace Indigox.Common.DomainModels.Test.Models
{
    /// <summary>
    /// 爱斯基摩狗
    /// </summary>
    public class Husky : Mammal, IDog
    {
        public string FurColor { get; set; }
    }
}
