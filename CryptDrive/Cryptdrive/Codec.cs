using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cryptdrive
{
    //Based on http://pages.infinit.net/ctech/20031101-0151.html
    //Function 1 : Encoder zur verschlüsslung von File -> Blob (Byte[] -> Byte[])
    //Function 2 : Decoder zur Entschlüsslung von Blob -> File (Byte[] -> Byte[])

    class Codec
    {
        private static bool isInit = false;
        private static RSACryptoServiceProvider rsa;

        private static void RSA()
        {
            rsa = new RSACryptoServiceProvider();
        }

        public static byte[] decrypt(byte[] dataBytes)
        {
            if (!isInit)
            {
                RSA();
                isInit = true;
            }/*
            return rsa.Decrypt(dataByte, true);*/

            // by default this will create a 128 bits AES (Rijndael) object
            SymmetricAlgorithm sa = SymmetricAlgorithm.Create();

            byte[] keyex = new byte[rsa.KeySize >> 3];
            Buffer.BlockCopy(dataBytes, 0, keyex, 0, keyex.Length);

            RSAPKCS1KeyExchangeDeformatter def = new RSAPKCS1KeyExchangeDeformatter(rsa);
            byte[] key = def.DecryptKeyExchange(keyex);

            byte[] iv = new byte[sa.IV.Length];
            Buffer.BlockCopy(dataBytes, keyex.Length, iv, 0, iv.Length);

            ICryptoTransform ct = sa.CreateDecryptor(key, iv);
            byte[] decrypt = ct.TransformFinalBlock(dataBytes, keyex.Length + iv.Length, dataBytes.Length - (keyex.Length + iv.Length));
            return decrypt;
        }

        public static byte[] encrypt(byte[] dataBytes)
        {
            if (!isInit)
            {
                RSA();
                isInit = true;
            }
            /*
                        return rsa.Encrypt(dataByte., true);*/

            // by default this will create a 128 bits AES (Rijndael) object
            SymmetricAlgorithm sa = SymmetricAlgorithm.Create();
            ICryptoTransform ct = sa.CreateEncryptor();
            byte[] encrypt = ct.TransformFinalBlock(dataBytes, 0, dataBytes.Length);

            RSAPKCS1KeyExchangeFormatter fmt = new RSAPKCS1KeyExchangeFormatter(rsa);
            byte[] keyex = fmt.CreateKeyExchange(sa.Key);

            // return the key exchange, the IV (public) and encrypted data
            byte[] result = new byte[keyex.Length + sa.IV.Length + encrypt.Length];
            Buffer.BlockCopy(keyex, 0, result, 0, keyex.Length);
            Buffer.BlockCopy(sa.IV, 0, result, keyex.Length, sa.IV.Length);
            Buffer.BlockCopy(encrypt, 0, result, keyex.Length + sa.IV.Length, encrypt.Length);
            return result;
        }
    }
}
