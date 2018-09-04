using System;
using System.Collections.Generic;
using System.IO;

namespace Cryptdrive
{
    // Function 1 : Neue Dateien hinzufügen (String[] files / List<String> files)
    // Function 1b : Sync mit vorhandenen Dateien (String[] files / List<String> files)
    // Function 2: Löscht vorhandene Dateien (String[] files/ List<String> files)
    // Function 3: Create Static Filewatcher ()
    // Function 4: GetFiles from GUI ()

    class FileManager
    {
        private FileManager()
        {
        }

        public static FileManager instance = new FileManager();

        List<String> clientFiles;
        List<String> cloudFiles;

        public List<String> getClientFiles()
        {
            return new List<String>();
        }

        public List<String> getCloudFiles()
        {
            return new List<String>();
        }

        public void addFiles(List<string> files)
        {
            // Todo Call Azure BlobAdd
        }

        public void syncFiles(List<string> files)
        {
            foreach (string path in files)
            {
                try
                {
                    //This if clause is just here to test that files were able to be decrypted after a program restart
                    if (File.Exists(path + "_encrypted"))
                    {
                        byte[] rawData = File.ReadAllBytes(path + "_encrypted");
                        byte[] rawDatadecrypted = Codec.decrypt(rawData);
                        byte[] rawDatauncompressed = Compressor.decompress(rawDatadecrypted);
                        File.WriteAllBytes(path + "_decrypted2", rawDatauncompressed);
                    }

                    byte[] fileAsByteArray = File.ReadAllBytes(path);
                    Logger.instance.logInfo(fileAsByteArray.ToString());
                    byte[] compressedByteArray = Compressor.compress(fileAsByteArray);
                    Logger.instance.logInfo(compressedByteArray.ToString());
                    byte[] encrpytedAndCompressedByteArray = Codec.encrypt(compressedByteArray);
                    Logger.instance.logInfo(encrpytedAndCompressedByteArray.ToString());

                    File.WriteAllBytes(path + "_encrypted", encrpytedAndCompressedByteArray);

                    byte[] decrypted = Codec.decrypt(encrpytedAndCompressedByteArray);
                    byte[] uncompressed = Compressor.decompress(decrypted);

                    File.WriteAllBytes(path + "_decrypted", uncompressed);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }

            // Todo Call Azure BlobSync
        }

        public void deleteFiles(List<string> files)
        {
            // Todo Call Azure BlobDelete
        }
    }
}
