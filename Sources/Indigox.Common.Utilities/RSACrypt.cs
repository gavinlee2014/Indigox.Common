using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Indigox.Common.Utilities
{
    public class RSACrypt
    {
        public static RSACryptoServiceProvider rsa;

        public static void AssignParameter()
        {
            const int PROVIDER_RSA_FULL = 1;
            const string CONTAINER_NAME = "SpiderContainer";
            CspParameters cspParams = new CspParameters(PROVIDER_RSA_FULL);
            cspParams.KeyContainerName = CONTAINER_NAME;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";
            rsa = new RSACryptoServiceProvider(cspParams);
        }

        public static void InitDncryptProvider()
        {
            X509Certificate2 x509 = new X509Certificate2(@"d:\rsa_private.pfx", "indigox");
            rsa = (RSACryptoServiceProvider)x509.PrivateKey;
        }

        public static void InitEncryptProvider()
        {
            X509Certificate2 x509 = new X509Certificate2(@"d:\rsa_public.cer");
            rsa = (RSACryptoServiceProvider)x509.PublicKey.Key;
        }

        public static string Encrypt(string plaintext)
        {
            InitEncryptProvider();
            byte[] plainbytes = System.Text.Encoding.UTF8.GetBytes(plaintext);
            return Convert.ToBase64String(rsa.Encrypt(plainbytes, false));
        }

        public static string Decrypt(string ripherext)
        {
            InitDncryptProvider();
            byte[] ripherexts = Convert.FromBase64String(ripherext);
            return System.Text.Encoding.UTF8.GetString(rsa.Decrypt(ripherexts,false));
        }

        public static string EncryptData(string data2Encrypt)
        {
            AssignParameter();
            StreamReader reader = new StreamReader(@"D:\yuliu\publickey.xml");
            string publicOnlyKeyXML = reader.ReadToEnd();
            rsa.FromXmlString(publicOnlyKeyXML);
            reader.Close();

            byte[] plainbytes = System.Text.Encoding.UTF8.GetBytes(data2Encrypt);
            byte[] cipherbytes = rsa.Encrypt(plainbytes, false);
            return Convert.ToBase64String(cipherbytes);
        }

        public static void AssignNewKey()
        {
            AssignParameter();

            //provide public and private RSA params
            StreamWriter writer = new StreamWriter(@"D:\yuliu\privatekey.xml");
            string publicPrivateKeyXML = rsa.ToXmlString(true);
            writer.Write(publicPrivateKeyXML);
            writer.Close();

            //provide public only RSA params
            writer = new StreamWriter(@"D:\yuliu\publickey.xml");
            string publicOnlyKeyXML = rsa.ToXmlString(false);
            writer.Write(publicOnlyKeyXML);
            writer.Close();

        }

        public static string DecryptData(string data2Decrypt)
        {
            AssignParameter();

            byte[] getpassword = Convert.FromBase64String(data2Decrypt);

            StreamReader reader = new StreamReader(@"D:\yuliu\privatekey.xml");
            string publicPrivateKeyXML = reader.ReadToEnd();
            rsa.FromXmlString(publicPrivateKeyXML);
            reader.Close();

            //read ciphertext, decrypt it to plaintext
            byte[] plain = rsa.Decrypt(getpassword, false);
            return System.Text.Encoding.UTF8.GetString(plain);

        }
    }
}
