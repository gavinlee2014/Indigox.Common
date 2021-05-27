using System;
using System.Collections.Generic;
using Indigox.Common.Message.Service;
using NUnit.Framework;

namespace Indigox.Common.Message.Test.Service
{
    [TestFixture]
    public class SendAppointmentTest
    {
        [Test]
        [Category( "UserTest" )]
        public void SendTest()
        {
            DateTime start = DateTime.Now.AddHours( 24 );
            DateTime end = DateTime.Now.AddHours( 25 );
            string mailSubject = "测试邮件";
            string mailBody = @"test message at " + DateTime.Now.ToString();
            string location = "靛蓝科技有限公司";
            List<string> mailto = new List<string>(){
                "xiang.wang@office365.indigox.net",
                "yong.zeng@office365.indigox.net"
            };

            SendAppointment sender = new SendAppointment();
            sender.MailHost = "smtp.office365.com";
            sender.MailPort = "587";
            sender.EnableSSL = "true";
            sender.MailAddress = "admin@office365.indigox.net";
            sender.Account = "admin@office365.indigox.net";
            sender.Password = "!nd!g0x.n1t";

            sender.Send( mailto, mailSubject, mailBody, start, end, location );

            //Assert.Inconclusive( "visit http://indigox.sharepoint.com/ to confirm new mails please." );
        }
    }
}