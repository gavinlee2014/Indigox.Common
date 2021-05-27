using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.StateContainer
{
    public interface IApplicationState
    {
        /// <summary>
        /// 获取或设置属性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[ string key ]
        {
            get;
            set;
        }
    }
}
