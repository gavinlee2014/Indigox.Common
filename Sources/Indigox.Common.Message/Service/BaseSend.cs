using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using Indigox.Common.Logging;

namespace Indigox.Common.Message.Service
{
    public class BaseSend
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string MailAddress { get; set; }
        public string MailPort { get; set; }
        public string MailHost { get; set; }
        public string EnableSSL { get; set; }

        protected SmtpClient BuildSmtpClient()
        {
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential( this.Account , this.Password );
            if ( !String.IsNullOrEmpty( this.MailPort ) )
            {
                client.Port = Convert.ToInt32(this.MailPort);
            }
            client.Host = this.MailHost;
            if ( !String.IsNullOrEmpty( this.EnableSSL ) )
            {
                client.EnableSsl = Convert.ToBoolean(this.EnableSSL);
            }

            return client;
        }

        protected MailMessage BuildMailObject( IList<string> toAddr, string mailSubject, string mailBody )
        {
            if ( string.IsNullOrEmpty( this.MailAddress ) )
            {
                Log.Error( "发件人为空，无法创建邮件对象，请检查 appSettings/MailNotifySenderAddress 配置节。" );
                return null;
            }

            try
            {
                MailMessage mail = new MailMessage();

                foreach ( string to in toAddr )
                {
                    mail.To.Add( to );
                }
                mail.From = new MailAddress( this.MailAddress, "", System.Text.Encoding.UTF8 );

                mail.Subject = mailSubject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = mailBody;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true; //是否是HTML邮件
                mail.Priority = MailPriority.Normal; //邮件优先级

                return mail;
            }
            catch ( Exception ex )
            {
                Log.Error( "创建邮件对象失败。\r\n" + ex.ToString() );
                return null;
            }
        }

        protected bool IsSmtpEnabled()
        {
            return ( !string.IsNullOrEmpty( this.MailHost ) && !string.IsNullOrEmpty( this.MailAddress ) );
        }
    }
}