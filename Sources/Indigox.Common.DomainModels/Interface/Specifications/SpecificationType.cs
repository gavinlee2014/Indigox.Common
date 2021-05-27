using System;

namespace Indigox.Common.DomainModels.Interface.Specifications
{
    public enum SpecificationType
    {
        /// <summary>
        /// 逻辑与
        /// </summary>
        And,

        /// <summary>
        /// 逻辑或
        /// </summary>
        Or,

        /// <summary>
        /// 等于
        /// </summary>
        Equal,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan,

        /// <summary>
        /// 包含
        /// </summary>
        Contains,

        /// <summary>
        /// 包含任意一个
        /// </summary>
        ContainsAny,

        /// <summary>
        /// 被包含
        /// </summary>
        In,

        /// <summary>
        /// Like
        /// </summary>
        Like,

        /// <summary>
        /// 大于或等于
        /// </summary>
        GraterOrEqual,

        /// <summary>
        /// 小于或等于
        /// </summary>
        LessOrEqual,

        /// <summary>
        /// 逻辑否
        /// </summary>
        Not,

        /// <summary>
        /// 全部
        /// </summary>
        All,
    }
}