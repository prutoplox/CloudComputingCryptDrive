using NeoSmart.Utils;
using System;
using System.IO;
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

        private static string encryptionParametersFileName = "EncryptionParameters.txt";

        private static void RSA()
        {
            if (File.Exists(encryptionParametersFileName))
            {
                try
                {
                    string parametersString = File.ReadAllText(encryptionParametersFileName);
                    rsa = new RSACryptoServiceProvider();
                    rsa.FromXmlString(parametersString);
                    Logger.instance.logInfo("Loaded the existing encrytion key from the file " + encryptionParametersFileName);
                    return;
                }
                catch (CryptographicException e)
                {
                    Logger.instance.logError("Could not load the encryption key from the file, creating a new key for this session.");
                }
            }

            rsa = new RSACryptoServiceProvider();
            var withprivate = rsa.ToXmlString(true);
            File.WriteAllText(encryptionParametersFileName, withprivate);
            Logger.instance.logInfo("Created a new encrytion key and saved it in the file " + encryptionParametersFileName);
        }

        public static byte[] decrypt(byte[] dataBytes)
        {
            if (!isInit)
            {
                RSA();
                isInit = true;
            }

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

        public static string encrypt(string input)
        {
            byte[] encryptedString = encrypt(System.Text.Encoding.UTF8.GetBytes(input));
            string returnString = UrlBase64.Encode(encryptedString);
            return returnString;
        }

        public static string decrypt(string input)
        {
            byte[] decryptedString = decrypt(UrlBase64.Decode(input));
            string returnString = System.Text.Encoding.UTF8.GetString(decryptedString);
            return returnString;
        }
    }
}
