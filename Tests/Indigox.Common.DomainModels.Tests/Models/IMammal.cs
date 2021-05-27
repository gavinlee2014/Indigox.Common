using System;
using System.Collections.Generic;

namespace Indigox.Common.DomainModels.Test.Models
{
    /// <summary>
    /// 哺乳动物
    /// </summary>
    public interface IMammal
    {
        /// <summary>
        /// 是否有牙根
        /// </summary>
        bool HasFang { get; set; }
    }
}
