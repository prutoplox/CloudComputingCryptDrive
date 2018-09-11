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

        public string containerName { get; set; }
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
            //Same as SyncFiles. Azure replaces old files with new files automatically
            syncFiles(files);
        }

        public void syncFiles(List<string> files)
        {
            foreach (string path in files)
            {
                try
                {
                    byte[] encrpytedAndCompressedByteArray = encryptAndCompressFile(path);
                    string cryptDriveFilePath = convertPathToCryptPath(path);
                    uploadFileData(cryptDriveFilePath, encrpytedAndCompressedByteArray);
                }
                catch (Exception e)
                {
                    Logger.instance.logError("The file could not be read: " + path);
                    Logger.instance.logError(e.Message);
                    Logger.instance.logError(e.StackTrace);
                }
            }
        }

        private async void uploadFileData(string path, byte[] data)
        {
            var content = new ByteArrayContent(data);
            string blobname = Codec.encrypt(path);
            string username = containerName;
            string fullURL = AzureLinkStringStorage.BLOB_ADD_AZURE_STRING + "&username=" + username + "&filename=" + blobname;
            var response = await AzureConnectionManager.client.PostAsync(fullURL, content);
            var responseString = await response.Content.ReadAsStringAsync();
            Logger.instance.logInfo("RESPONSE:" + responseString);
        }

        private static byte[] encryptAndCompressFile(string path)
        {
            byte[] fileAsByteArray = File.ReadAllBytes(path);
            Logger.instance.logInfo(fileAsByteArray.ToString());
            byte[] compressedByteArray = Compressor.compress(fileAsByteArray);
            Logger.instance.logInfo(compressedByteArray.ToString());
            byte[] encrpytedAndCompressedByteArray = Codec.encrypt(compressedByteArray);
            Logger.instance.logInfo(encrpytedAndCompressedByteArray.ToString());
            return encrpytedAndCompressedByteArray;
        }

        public static string convertPathToCryptPath(string fullPath)
        {
            string cryptFolderName = FileWatcher.instance.getMonitoredCryptFolderName(fullPath);
            string localFolderPath = FileWatcher.instance.getMonitoredFolderName(fullPath);
            string pathRelativeToMonitored = fullPath.Substring(localFolderPath.Length);

            //Prepends the virtual path of the folder in the cryptdrive where it's stored and removes unneeded parts of the full path on the client
            return cryptFolderName + ">" + pathRelativeToMonitored;
        }

        public void deleteFiles(List<string> files)
        {
            // Todo Call Azure BlobDelete
        }
    }
}
