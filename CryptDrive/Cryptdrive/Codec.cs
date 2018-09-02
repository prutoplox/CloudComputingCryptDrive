using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cryptdrive
{
    //https://stackoverflow.com/questions/18485715/how-to-use-public-and-private-key-encryption-technique-in-c-sharp
    //Function 1 : Encoder zur verschlüsslung von File -> Blob (Byte[] -> Byte[])
    //Function 2 : Decoder zur Entschlüsslung von Blob -> File (Byte[] -> Byte[])

    class Codec
    {
        private static bool isInit = false;
        private static string _privateKey;
        private static string _publicKey;
        private static UnicodeEncoding _encoder = new UnicodeEncoding();

        private static void RSA()
        {
            var rsa = new RSACryptoServiceProvider();
            _privateKey = rsa.ToXmlString(true);
            _publicKey = rsa.ToXmlString(false);
        }

        public static byte[] decrypt(byte[] dataByte)
        {
            if (!isInit)
            {
                RSA();
                isInit = true;
            }
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_privateKey);
            return rsa.Decrypt(dataByte, false);
        }

        public static byte[] encrypt(byte[] dataByte)
        {
            if (!isInit)
            {
                RSA();
                isInit = true;
            }
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);
            return rsa.Encrypt(dataByte, false).ToArray();
        }
    }
}
