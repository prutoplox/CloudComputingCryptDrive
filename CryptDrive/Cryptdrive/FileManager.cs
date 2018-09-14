using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Linq;

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
            throw new NotImplementedException();
        }

        public List<String> getCloudFiles()
        {
            throw new NotImplementedException();
        }

        public void addFiles(List<string> files)
        {
            //Same as SyncFiles. Azure replaces old files with new files automatically
            syncFiles(files);
        }

        public void addFile(string path)
        {
            syncFile(path);
        }

        public void syncFiles(IEnumerable<string> files)
        {
            foreach (string path in files)
            {
                syncFile(path);
            }
        }

        public void syncFile(string path)
        {
            try
            {
                byte[] encrpytedAndCompressedByteArray = encryptAndCompressFile(path);
                string cryptDriveFilePath = convertPathToCryptPath(path);
                uploadFileDataHashedName(cryptDriveFilePath, encrpytedAndCompressedByteArray);
            }
            catch (Exception e)
            {
                Logger.instance.logError("The file could not be read: " + path);
                Logger.instance.logError(e.Message);
                Logger.instance.logError(e.StackTrace);
            }
        }

        public void renameFiles(List<string> oldFileNames, List<string> newFileNames)
        {
            if (oldFileNames.Count != newFileNames.Count)
            {
                throw new Exception("Number of filenames must match!");
            }

            for (int i = 0; i < oldFileNames.Count; i++)
            {
                renameFileHashedNames(oldFileNames[i], newFileNames[i]);
            }
        }

        public void renameFileHashedNames(string oldPath, string newPath)
        {
            renameFileAsync(FileNameStorage.instance.hashPath(oldPath), FileNameStorage.instance.hashPath(newPath));
        }

        public async void renameFileAsync(string oldPath, string newPath)
        {
            string fullURL = AzureLinkStringStorage.BLOB_RENAME_AZURE_STRING + AzureLinkStringStorage.LINKING_INITALCHARACTER +
                "container=" + containerName + "&filenameOld=" + oldPath + "&filenameNew=" + newPath;
            var response = await AzureConnectionManager.client.PostAsync(fullURL, null);
            var responseString = await response.Content.ReadAsStringAsync();
            Logger.instance.logInfo("RESPONSE:" + responseString);
        }

        public void deleteFile(string file)
        {
            deleteFiles(new List<string>() { file });
        }

        public void deleteCryptFile(string file)
        {
            deleteFiles(new List<string>() { convertPathToCryptPath(file) });
        }

        public void deleteCryptFiles(IEnumerable<string> files)
        {
            deleteFiles(files.Select(X => convertPathToCryptPath(X)));
        }

        public async void deleteFiles(IEnumerable<string> files)
        {
            string containername = containerName;
            string fullURL = AzureLinkStringStorage.DELETE_AZURE_STRING + AzureLinkStringStorage.LINKING_INITALCHARACTER + "containername=" + containername;
            var content = new MultipartFormDataContent();
            int i = 0;
            foreach (string path in files)
            {
                var stringContent = new StringContent(FileNameStorage.instance.hashPath(path));
                content.Add(stringContent, i++.ToString());
            }
            var response = await AzureConnectionManager.client.PostAsync(fullURL, content);
            var responseString = await response.Content.ReadAsStringAsync();
            Logger.instance.logInfo("RESPONSE:" + responseString);
        }

        public void uploadFileDataHashedName(string path, byte[] data)
        {
            string blobname = FileNameStorage.instance.hashPath(path);
            uploadFileData(blobname, data);
        }

        public async void uploadFileData(string path, byte[] data)
        {
            var content = new ByteArrayContent(data);
            string username = containerName;
            string fullURL = AzureLinkStringStorage.BLOB_ADD_AZURE_STRING + AzureLinkStringStorage.LINKING_INITALCHARACTER + "username=" + username + "&filename=" + path;
            var response = await AzureConnectionManager.client.PostAsync(fullURL, content);
            var responseString = await response.Content.ReadAsStringAsync();
            Logger.instance.logInfo("RESPONSE:" + responseString);
        }

        public async void downloadFile(string blobname, string path)
        {
            string fullURL = AzureLinkStringStorage.BLOB_GET_AZURE_STRING + AzureLinkStringStorage.LINKING_INITALCHARACTER + "containername=" + containerName + "&blobname=" + blobname;
            var response = await AzureConnectionManager.client.PostAsync(fullURL, null);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Logger.instance.logError("File " + blobname + " does not exist in Cloud");
                return;
            }

            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(responseString, path);
                }
            }
            catch (WebException e)
            {
                Logger.instance.logError("Could not dowload the file " + blobname + "!");
            }
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
    }
}
