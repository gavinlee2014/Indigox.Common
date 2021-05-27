using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using Indigox.Common.Logging;
namespace Indigox.Common.Message.Service
{
    public class SendMail : BaseSend
    {
        public void Send( IList<string> toAddr, string mailSubject, string mailBody )
        {
            #region validation

            if ( !IsSmtpEnabled() )
            {
                return;
            }

            if ( toAddr.Count == 0 )
            {
                throw new ArgumentException( "收件人为空。" );
            }

            #endregion validation

            #region build mail object

            MailMessage mail = BuildMailObject( toAddr, mailSubject, mailBody );
            if ( mail == null )
            {
                return;
            }

            #endregion build mail object

            #region build smtp client

            SmtpClient client = BuildSmtpClient();

            #endregion build smtp client

            #region smtp send

            try
            {
                client.Send( mail );
                string sendList = "";
                for (int i = 0; i < toAddr.Count; i++)
                {
                    sendList += toAddr[i] + ",";
                }
                Log.Info(String.Format("Send mail to {0}\r\nSubject: {1}\r\nBody: {2}",
                                        sendList,
                                        mailSubject,
                                        mailBody));
            }
            catch ( SmtpException ex )
            {
                Log.Error( "SMTP error.\r\n" + ex.ToString() );
            }
            catch ( Exception ex )
            {
                Log.Error( "Send mail error.\r\n" + ex.ToString() );
            }

            #endregion smtp send
        }

        public void SendAsync( IList<string> toAddr, string mailSubject, string mailBody )
        {
            Thread thread = new Thread( delegate()
            {
                Send( toAddr, mailSubject, mailBody );
            } );
            thread.IsBackground = true;
            thread.Start();
        }
    }
}