
namespace Indigox.Common.Message
{
    public abstract class Message
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string From;
        /// <summary>
        /// 接收者
        /// </summary>
        public string[] To;
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject;
        /// <summary>
        /// 正文
        /// </summary>
        public string Body;
    }
}
