using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

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
        public static HttpClient client = new HttpClient();

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

                    uploadFileData(path, encrpytedAndCompressedByteArray);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private async void uploadFileData(string path, byte[] data)
        {
            var content = new ByteArrayContent(data);

            string filename = Path.GetFileNameWithoutExtension(path);

            //TODO convert full path into a single string which will be used on the storage to represent the folder structure
            string relativePath = "";
            string blobname = relativePath + filename;

            //TODO retrieve username of this client which will be used as container name on the server, container must be created in SotrageCreate first.
            string username = "testcontainer";

            string fullURL = "https://functionapp20180905122323.azurewebsites.net/api/AddBlob?code=yA/tjdgIFG2x6VaICxFaBssdZLZMsi/7tYBKEaor9QQEZkPaJWRGnQ==&username=" + username + "&filename=" + blobname;
            var response = await client.PostAsync(fullURL,
                content);

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("RESPONSE:" + responseString);
        }

        public void deleteFiles(List<string> files)
        {
            // Todo Call Azure BlobDelete
        }
    }
}
