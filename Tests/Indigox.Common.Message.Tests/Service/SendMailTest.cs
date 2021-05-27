using System;
using System.Collections.Generic;
using Indigox.Common.Message.Service;
using NUnit.Framework;

namespace Indigox.Common.Message.Test.Service
{
    [TestFixture]
    public class SendMailTest
    {
        [Test]
        [Category( "UserTest" )]
        public void SendTest()
        {
            string mailSubject = "测试邮件";
            string mailBody = @"test message at " + DateTime.Now.ToString();
            List<string> mailto = new List<string>(){
                "xiang.wang@office365.indigox.net",
                "yong.zeng@office365.indigox.net"
            };

            SendMail sender = new SendMail();
            sender.MailHost = "smtp.office365.com";
            sender.MailPort = "587";
            sender.EnableSSL = "true";
            sender.MailAddress = "admin@office365.indigox.net";
            sender.Account = "admin@office365.indigox.net";
            sender.Password = "!nd!g0x.n1t";

            sender.Send( mailto, mailSubject, mailBody );

            //Assert.Inconclusive( "visit http://indigox.sharepoint.com/ to confirm new mails please." );
        }
    }
}