using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using Indigox.Common.Logging;

namespace Indigox.Common.Message.Service
{
    public class SendAppointment : BaseSend
    {
        public void Send( IList<string> toAddr, string mailSubject, string mailBody, DateTime startTime, DateTime endTime, string location )
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

            AlternateView avCal1 = AlternateView.CreateAlternateViewFromString( mailBody, new ContentType( "text/html" ) );
            mail.AlternateViews.Add( avCal1 );

            AlternateView avCal = BuildAppointment( mailSubject, mailBody, startTime, endTime, location );
            mail.AlternateViews.Add( avCal );

            #endregion build mail object

            #region build smtp client

            SmtpClient client = BuildSmtpClient();

            #endregion build smtp client

            #region smtp send

            try
            {
                client.Send( mail );
            }
            catch ( SmtpException ex )
            {
                Log.Error( "SMTP error.\r\n" + ex.ToString() );
            }
            catch ( Exception ex )
            {
                Log.Error( "Send mail error." + ex.ToString() );
            }

            #endregion smtp send
        }

        public void SendAsync( IList<string> toAddr, string mailSubject, string mailBody, DateTime startTime, DateTime endTime, string location )
        {
            Thread thread = new Thread( delegate()
            {
                Send( toAddr, mailSubject, mailBody, startTime, endTime, location );
            } );
            thread.IsBackground = true;
            thread.Start();
        }

        private AlternateView BuildAppointment( string mailSubject, string mailBody, DateTime startTime, DateTime endTime, string location )
        {
            Appointment appointment = new Appointment();
            appointment.Description = mailBody;
            appointment.StartTime = startTime;
            appointment.EndTime = endTime;
            appointment.Location = location;
            appointment.Subject = mailSubject;
            appointment.AlldayEvent = false;
            appointment.UseAlarm = true;
            ContentType ct = new ContentType( "text/calendar" );
            ct.Parameters.Add( "method", "REQUEST" );
            AlternateView avCal = AlternateView.CreateAlternateViewFromString( appointment.ToString(), ct );

            return avCal;
        }
    }
}