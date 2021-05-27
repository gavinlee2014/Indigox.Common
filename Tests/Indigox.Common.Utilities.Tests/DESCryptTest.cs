using System;
using NUnit.Framework;

namespace Indigox.Common.Utilities.Tests
{
    [TestFixture]
    public class DESCryptTest
    {
        [Test]
        public void TestGenerateKey()
        {
            Console.WriteLine(DESCrypt.GenerateKey());
        }
        [Test]
        public void TestEncrypt()
        {
            string secureKey = "P@ssw0rd";
            string plainText = @"{""ID"":""4d31a7ce-01d6-4ce5-b23e-642353fa1639"",""IPAddress"":null,""UserName"":""yanglh"",""ServiceTicket"":""bJ6dIsIFnrDjtm27MEmylJWi5djk1jxKFCfIJro1LKKH2LYQiG9fPA=="",""CreateTime"":""\/Date(1378264537195+0800)\/"",""TicketGrantingTicketID"":""78e90598-1378-4f0a-9d5c-1f1927ebf982""}";
            string actural = DESCrypt.Encrypt( plainText, secureKey );
            Console.WriteLine( actural );
        }

        [Test]
        public void TestDecrypt()
        {
            string secureKey = "P@ssw0rd";
            string cipherText = @"3lRunO8bEx0IyJa7YfGkqHnLCv0TbdGOftDxopQ/s3LSAjaGQf9lc8tngALPoEXdpbzQpoh9zzPV1ociBaNpUP5Y4PIv4P7f/VE3xYoBpt6zhqrAtHIoXoKzLFgU5P43bM/plh3obVf21dJ5tXJfSTYf2qJpOnEMQ3FWH9I9gMZA+Ven9GJSiMn9G8pTK0Qh/i2oIx8ahnp2oBF66KXl/nzGopc0AACrnyf0GdKZ+CqYAX+FpIWpez0vabvHV8e++uZDHUcYDeQDxAYJn+MA0enDDEsQBh/3U1Oim0F0gtrc15QZBP8WVl2PE1VuoWTQHjh7mCg1fhYDS8Fg4N+eMUdFR2oD84y1seuuhO2HDlU=";
            string expected = @"{""ID"":""4d31a7ce-01d6-4ce5-b23e-642353fa1639"",""IPAddress"":null,""UserName"":""yanglh"",""ServiceTicket"":""bJ6dIsIFnrDjtm27MEmylJWi5djk1jxKFCfIJro1LKKH2LYQiG9fPA=="",""CreateTime"":""\/Date(1378264537195+0800)\/"",""TicketGrantingTicketID"":""78e90598-1378-4f0a-9d5c-1f1927ebf982""}";
            string actural = DESCrypt.Dncrypt( cipherText, secureKey );
            Console.WriteLine( actural );
            Assert.AreEqual( expected, actural );
        }

        [Test]
        public void Test1()
        {
            string cipherText = "44gQ7T9iD7uk0FtKXEfAWxg1uCX9GuDXLECur51tjhc7gQ+Ti5rzrriEoQQ/p1axY+ZPvf6QhnU=";
            string key = "P@ssw0rd";
            Console.WriteLine(DESCrypt.Dncrypt(cipherText, key));
        }
    }
}