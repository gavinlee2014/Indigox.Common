using System;
using System.Text;
#if WEB
using System.Web;
#endif

namespace Indigox.Common.Utilities
{
    public static class EncodeUtil
    {

        private static Encoding DefaultEncoding = Encoding.UTF8;

        #region Base64 Encode

        /// <summary>
        /// Base64s the encode, default use utf-8 encoding.
        /// </summary>
        /// <param name="originalText">The original text.</param>
        /// <returns></returns>
        public static string Base64Encode(string originalText)
        {
            return Convert.ToBase64String(DefaultEncoding.GetBytes(originalText));
        }

        /// <summary>
        /// Base64s the decode, default use utf-8 encoding.
        /// </summary>
        /// <param name="encodedText">The encoded text.</param>
        /// <returns></returns>
        public static string Base64Decode(string encodedText)
        {
            return DefaultEncoding.GetString(Convert.FromBase64String(encodedText));
        }

        /// <summary>
        /// Base64s the encode.
        /// </summary>
        /// <param name="originalText">The original text.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string Base64Encode(string originalText, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(originalText));
        }

        /// <summary>
        /// Base64s the decode.
        /// </summary>
        /// <param name="encodedText">The encoded text.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string Base64Decode(string encodedText, Encoding encoding)
        {
            return encoding.GetString(Convert.FromBase64String(encodedText));
        }

        /// <summary>
        /// Base64s the bytes encode.
        /// </summary>
        /// <param name="originalText">The original text.</param>
        /// <returns></returns>
        public static byte[] Base64BytesEncode(string originalText)
        {
            return Convert.FromBase64String(originalText);
        }

        /// <summary>
        /// Base64s the bytes decode.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static string Base64BytesDecode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        #endregion

#if WEB
        #region Url Encode

        /// <summary>
        /// 对字符串进行 URL 编码
        /// </summary>
        /// <param name="originalText">The original text.</param>
        /// <remarks>不可对整个 URL 编码，应该只对参数部分的文字进行编码</remarks>
        public static string UrlEncode(string originalText)
        {
            return HttpUtility.UrlEncode(originalText);
        }

        /// <summary>
        /// 对字符串进行 URL 反编码，支持带中文的URL，默认使用 utf-8 编码，指定编码请使用 EncodeUtil.UrlDecode(string encodedText, Encoding encoding)。
        /// 不要去反编码 Request.Url，因为它本身已经使用 Request.Encoding 解码过一次了，应该反编码 Request.RawUrl。
        /// </summary>
        /// <param name="encodedText">The encoded text.</param>
        /// <returns></returns>
        public static string UrlDecode(string encodedText)
        {
            //--TODO: Support contains chinese char urls.
            //return HttpUtility.UrlDecode(encodedText);

            return UrlDecode(encodedText, DefaultEncoding);
        }

        /// <summary>
        /// 对字符串进行 URL 编码
        /// </summary>
        /// <param name="originalText">The original text.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        /// <remarks>不可对整个 URL 编码，应该只对参数部分的文字进行编码</remarks>
        public static string UrlEncode(string originalText, Encoding encoding)
        {
            return HttpUtility.UrlEncode(originalText, encoding);
        }

        /// <summary>
        /// 对字符串进行 URL 反编码，支持带中文的URL。
        /// 不要去反编码 Request.Url，因为它本身已经使用 Request.Encoding 解码过一次了，应该反编码 Request.RawUrl。
        /// </summary>
        /// <param name="encodedText">The encoded text.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        /// <example>string decodedUrl = EncodeUtil.UrlDecode(Request.RawUrl, Request.Encoding);</example>
        public static string UrlDecode(string encodedText, Encoding encoding)
        {
            //--TODO: Support contains chinese char urls.
            //return HttpUtility.UrlDecode(encodedText, encoding);

            if (encodedText == null || encodedText == "")
            {
                return "";
            }

            string str1 = encodedText, str2;
            char[] buff = new char[str1.Length];
            int decodedLength = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                char ch = str1[i];
                int valueLen = str1.Length;
                if (ch == '%')
                {
                    int j = 0;
                    // URL中编码的字符总是占3个字符，它的格式是%XX，XX是一个Byte的16进制表示方式，
                    // 我们将所有这样连续的3个字符串一起一次解码，这样就不会造成一个字符被拆掉的危险。
                    for (; i + j <= valueLen; j += 3)
                    {
                        if (i + j == valueLen)
                        {
                            break;
                        }
                        ch = str1[i + j];
                        if (ch != '%')
                        {
                            break;
                        }
                    }
                    str2 = HttpUtility.UrlDecode(str1.Substring(i, j), encoding);
                    for (int n = 0; n < str2.Length; n++)
                    {
                        buff[decodedLength++] = str2[n];
                    }
                    i += j - 1;
                }
                else
                {
                    buff[decodedLength++] = ch;
                }
            }
            return new string(buff, 0, decodedLength);
        }

        #endregion
#endif
    }
}
