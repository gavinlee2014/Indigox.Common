using System;
using System.Text;

namespace Indigox.Common.Message
{
    public class Appointment : Message
    {
        private const string _DATEFORMAT = "yyyyMMdd\\THHmmss\\Z";

        #region property

        /// <summary>
        /// 约会开始时间
        /// </summary>
        public DateTime StartTime;
        /// <summary>
        /// 约会结束时间
        /// </summary>
        public DateTime EndTime;
        /// <summary>
        /// 参与者
        /// </summary>
        public string[] Attendees;
        /// <summary>
        /// 组织者
        /// </summary>
        public string Organizer;
        /// <summary>
        /// 约会地点
        /// </summary>
        public string Location;
        /// <summary>
        /// 是否提醒
        /// </summary>
        public bool UseAlarm;
        /// <summary>
        /// 约会内容
        /// </summary>
        public string Description;
        /// <summary>
        /// 是否全天事件
        /// </summary>
        public bool AlldayEvent;

        #endregion

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            //str.AppendLine( "BEGIN:VCALENDAR" );
            //str.AppendLine( "PRODID:-//Microsoft Corporation//Outlook 14.0 MIMEDIR//EN" );
            //str.AppendLine( "VERSION:2.0" );
            //str.AppendLine( "METHOD:REQUEST" );
            //str.AppendLine( "X-MS-OLK-FORCEINSPECTOROPEN:TRUE" );

            str.AppendLine( "BEGIN:VCALENDAR" );
            str.AppendLine( "METHOD:REQUEST" );
            str.AppendLine( "PRODID:Microsoft Exchange Server 2010" );
            str.AppendLine( "VERSION:2.0" );

            str.AppendLine( "BEGIN:VTIMEZONE" );
            str.AppendLine( "TZID:China Standard Time" );
            str.AppendLine( "BEGIN:STANDARD" );
            str.AppendLine( "DTSTART:16010101T000000" );
            str.AppendLine( "TZOFFSETFROM:+0800" );
            str.AppendLine( "TZOFFSETTO:+0800" );
            str.AppendLine( "END:STANDARD" );
            str.AppendLine( "BEGIN:DAYLIGHT" );
            str.AppendLine( "DTSTART:16010101T000000" );
            str.AppendLine( "TZOFFSETFROM:+0800" );
            str.AppendLine( "TZOFFSETTO:+0800" );
            str.AppendLine( "END:DAYLIGHT" );
            str.AppendLine( "END:VTIMEZONE" );

            str.AppendLine( "BEGIN:VEVENT" );
            str.AppendLine( "CLASS:PUBLIC" );

            str.AppendLine( string.Format( "DTSTART;TZID=China Standard Time:{0}", StartTime.ToUniversalTime().ToString( _DATEFORMAT ) ) );
            str.AppendLine( string.Format( "DTSTAMP:{0}", DateTime.Now.ToUniversalTime().ToString( _DATEFORMAT ) ) );
            str.AppendLine( string.Format( "DTEND;TZID=China Standard Time:{0}", EndTime.ToUniversalTime().ToString( _DATEFORMAT ) ) );
            
            str.AppendLine( string.Format( "UID:{0}", Guid.NewGuid() ) );
            str.AppendLine( string.Format( "DESCRIPTION;LANGUAGE=zh-CN:{0}", Description ) );
            //str.AppendLine( string.Format( "X-ALT-DESC;FMTTYPE=text/html:<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 3.2//EN\"><HTML><head><meta name=\"Generator\" content=\"Microsoft Exchange Server\"></head><BODY>{0}</body><Html>", Description ) );
            str.AppendLine( string.Format( "SUMMARY:{0}", Subject ) );
            str.AppendLine( string.Format( "LOCATION:{0}", Location ) );

            str.AppendLine( "PRIORITY:5" );
            str.AppendLine( "TRANSP:OPAQUE" );
            str.AppendLine( "STATUS:CONFIRMED" );
            str.AppendLine( "SEQUENCE:0" );

            str.AppendLine( "X-MICROSOFT-CDO-APPT-SEQUENCE:0" );
            str.AppendLine( "X-MICROSOFT-CDO-OWNERAPPTID:1137985500" );
            str.AppendLine( "X-MICROSOFT-CDO-BUSYSTATUS:TENTATIVE" );
            str.AppendLine( "X-MICROSOFT-CDO-INTENDEDSTATUS:BUSY" );
            str.AppendLine( "X-MICROSOFT-CDO-ALLDAYEVENT:FALSE" );
            str.AppendLine( "X-MICROSOFT-CDO-IMPORTANCE:1" );
            str.AppendLine( "X-MICROSOFT-CDO-INSTTYPE:0" );
            str.AppendLine( "X-MICROSOFT-DISALLOW-COUNTER:FALSE" );

            //if ( AlldayEvent )
            //{
            //    str.AppendLine( "X-MICROSOFT-CDO-ALLDAYEVENT:TRUE" );
            //    str.AppendLine( "X-MICROSOFT-CDO-BUSYSTATUS:FREE" );
            //    str.AppendLine( "X-MICROSOFT-CDO-IMPORTANCE:1" );
            //    str.AppendLine( "X-MICROSOFT-DISALLOW-COUNTER:FALSE" );
            //    str.AppendLine( "X-MICROSOFT-CDO-INTENDEDSTATUS:FREE" );
            //    str.AppendLine( "X-MS-OLK-APPTSEQTIME:20120327T142904Z" );
            //    str.AppendLine( "X-MS-OLK-AUTOFILLLOCATION:FALSE" );
            //    str.AppendLine( "X-MS-OLK-CONFTYPE:0" );
            //}

            str.AppendLine( "BEGIN:VALARM" );
            str.AppendLine( "TRIGGER:-PT30M" );
            str.AppendLine( "ACTION:DISPLAY" );
            str.AppendLine( "DESCRIPTION:Reminder" );
            str.AppendLine( "END:VALARM" );
            str.AppendLine( "END:VEVENT" );
            str.AppendLine( "END:VCALENDAR" );

            Console.Write( str.ToString() );
            return str.ToString();
        }

    }
}
