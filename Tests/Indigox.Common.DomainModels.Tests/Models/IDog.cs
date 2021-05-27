using System;
using System.Collections.Generic;

namespace Indigox.Common.DomainModels.Test.Models
{
    /// <summary>
    /// 狗
    /// </summary>
    public interface IDog : IMammal
    {
        /// <summary>
        /// 皮毛颜色
        /// </summary>
        string FurColor { get; set; }
    }
}
