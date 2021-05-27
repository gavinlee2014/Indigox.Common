using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Indigox.Common.ADAccessor.ObjectModel;

namespace Indigox.Common.ADAccessor.Tests
{
    [Category("UserTest")]
    public class AccessorTest
    {
        [Test]
        [TestCase("wangxiang")]
        [TestCase("wangx")]
        public void TestIsUserExist(string userName)
        {
            Assert.IsTrue(Accessor.IsUserExist(userName));
        }

        [Test]
        [TestCase("wangxiang", "P@ssw0rd")]
        [TestCase("wangxiang", "!nd!g0x.n1t")]
        public void TestCheckPassword(string userName, string pwd)
        {
            Assert.IsTrue(Accessor.CheckPassword(userName, pwd));
        }

        [Test]
        [TestCase("wangxiang", "P@ssw0rd")]
        [TestCase("wangxiang", "!nd!g0x.n1t")]
        public void TestSetPassword(string userName, string pwd)
        {
            Accessor.SetPassword(userName, pwd);
        }

        [Test]
        [TestCase("wangxiang", "P@ssw0rd", "!nd!g0x.n1t")]
        [TestCase("wangxiang", "!nd!g0x.n1t", "!nd!g0x.n1t")]
        public void TestChangePassword(string userName, string oldPwd, string newPwd)
        {
            Accessor.ChangePassword(userName, oldPwd, newPwd);
        }

        [Test]
        public void TestProfileService()
        {
            string url = "http://hr.excegroup.com/PlatinumHRM/Import/BSHR/Portrait/picture14.jpg";
            ProfileService service = new ProfileService(url);
            byte[] bytes = service.GetThumbnailImageForAD();
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytes);
            stream.Position = 0;
            System.IO.BinaryReader br = new System.IO.BinaryReader(stream);
            Console.WriteLine("格式：" + br.ReadByte().ToString() + br.ReadByte().ToString());

            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
        }

        [Test]
        public void TestCreateUser()
        {
            User user=new User (){
                Account="zhangzb2214",
                Name="李忠兵2214",
                DisplayName = "李忠兵2214",
                Portrait="http://hr.excegroup.com/PlatinumHRM/Import/BSHR/Portrait/picture14.jpg"
            };
            Accessor.CreateUser(null, user);
        }
    }
}
