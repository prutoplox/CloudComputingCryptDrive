using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<bool> addFiles(IEnumerable<string> files)
        {
            //Same as SyncFiles. Azure replaces old files with new files automatically
            return await syncFiles(files);
        }

        public async Task<bool> addFile(string path)
        {
            return await syncFile(path);
        }

        public async Task<bool> syncFiles(IEnumerable<string> files)
        {
            bool allUploaded = true;
            foreach (string path in files)
            {
                allUploaded |= await syncFile(path);
            }
            return allUploaded;
        }

        public async Task<bool> syncFile(string path)
        {
            byte[] encrpytedAndCompressedByteArray = encryptAndCompressFile(path);
            string cryptDriveFilePath = convertPathToCryptPath(path);
            return await uploadFileDataHashedName(cryptDriveFilePath, encrpytedAndCompressedByteArray);
        }

        public async Task<bool> renameFiles(List<string> oldFileNames, List<string> newFileNames)
        {
            if (oldFileNames.Count != newFileNames.Count)
            {
                throw new Exception("Number of filenames must match!");
            }

            bool allRenamed = true;
            for (int i = 0; i < oldFileNames.Count; i++)
            {
                allRenamed |= await renameFileHashedNames(oldFileNames[i], newFileNames[i]);
            }
            return allRenamed;
        }

        public async Task<bool> renameFileHashedNames(string oldPath, string newPath)
        {
            string cryptOld = convertPathToCryptPath(oldPath);
            string hashOldCrypt = FileNameStorage.instance.hashPath(cryptOld);
            string cryptNew = convertPathToCryptPath(newPath);
            string hashNewCrypt = FileNameStorage.instance.hashPath(cryptNew);
            bool result = await renameFileAsync(hashOldCrypt, hashNewCrypt);
            if (result)
            {
                FileNameStorage.instance.removeMapping(cryptOld);
                FileNameStorage.instance.AddToTracking(cryptNew, hashNewCrypt);
            }
            return result;
        }

        public async Task<bool> renameFileAsync(string oldPath, string newPath)
        {
            try
            {
                string fullURL = AzureLinkStringStorage.BLOB_RENAME_AZURE_STRING + AzureLinkStringStorage.LINKING_INITALCHARACTER +
                    "container=" + containerName + "&filenameOld=" + oldPath + "&filenameNew=" + newPath;
                var response = await AzureConnectionManager.client.PostAsync(fullURL, null);
                var responseString = await response.Content.ReadAsStringAsync();
                Logger.instance.logInfo("Renamed the file " + oldPath + " to " + newPath + " in the cloud");
                return true;
            }
            catch (HttpRequestException e)
            {
                Logger.instance.logError(e.Message);
                return false;
            }
        }

        public async Task<bool> deleteFile(string file)
        {
            return await deleteFiles(new List<string>() { file });
        }

        public async Task<bool> deleteCryptFile(string file)
        {
            return await deleteFiles(new List<string>() { convertPathToCryptPath(file) });
        }

        public async Task<bool> deleteCryptFiles(IEnumerable<string> files)
        {
            return await deleteFiles(files.Select(X => convertPathToCryptPath(X)));
        }

        public async Task<bool> deleteFiles(IEnumerable<string> files)
        {
            try
            {
                if (!files.Any())
                {
                    return false;
                }
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
                Logger.instance.logInfo("Deleted following file on the server:" + responseString);
                FileNameStorage.instance.removeMappings(files);
                return true;
            }
            catch (HttpRequestException e)
            {
                Logger.instance.logError(e.Message);
                return false;
            }
        }

        public async Task<bool> uploadFileDataHashedName(string path, byte[] data)
        {
            string blobname = FileNameStorage.instance.hashPath(path, true);
            return await uploadFileData(blobname, data);
        }

        public async Task<bool> uploadFileData(string path, byte[] data)
        {
            try
            {
                var content = new ByteArrayContent(data);
                string username = containerName;
                string fullURL = AzureLinkStringStorage.BLOB_ADD_AZURE_STRING + AzureLinkStringStorage.LINKING_INITALCHARACTER + "username=" + username + "&filename=" + path;
                var response = await AzureConnectionManager.client.PostAsync(fullURL, content);
                var responseString = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        Logger.instance.logInfo("Uplaoded " + path + " with following answer from the server:" + responseString);
                        break;

                    case HttpStatusCode.Unauthorized:
                        Logger.instance.logError("Not enough free storage space in the cloud on this account to save/update the file");
                        break;

                    default:
                        Logger.instance.logError("Uploading failed for some reason, the reason is:" + responseString);
                        break;
                }
                return true;
            }
            catch (HttpRequestException e)
            {
                Logger.instance.logError(e.Message);
                return false;
            }
        }

        public async Task<string> getURL(string blobname)
        {
            string fullURL = AzureLinkStringStorage.BLOB_GET_AZURE_STRING + AzureLinkStringStorage.LINKING_INITALCHARACTER + "containername=" + containerName + "&blobname=" + blobname;
            var response = await AzureConnectionManager.client.PostAsync(fullURL, null);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Logger.instance.logError("File " + blobname + " does not exist in Cloud");
                return String.Empty;
            }
            return responseString;
        }

        public async Task<bool> downloadFile(string blobname, string path)
        {
            try
            {
                string responseString = await getURL(blobname);

                try
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(responseString, path);
                        return true;
                    }
                }
                catch (WebException e)
                {
                    Logger.instance.logError("Could not dowload the file " + blobname + "!");
                    Logger.instance.logError(e.Message);
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                Logger.instance.logError(e.Message);
                return false;
            }
        }

        public async Task<IEnumerable<string>> ListNewerFiles(DateTime timestamp)
        {
            try
            {
                string fullURL = AzureLinkStringStorage.BLOB_LIST_NEWER + AzureLinkStringStorage.LINKING_INITALCHARACTER + "containername=" + FileManager.instance.containerName + "&timestamp=" + timestamp;
                var response = await AzureConnectionManager.client.PostAsync(fullURL, null);
                var responseString = await response.Content.ReadAsStringAsync();
                Logger.instance.logInfo("Loaded a list of files which are newer then " + timestamp);
                return responseString.Split('>');
            }
            catch (HttpRequestException e)
            {
                Logger.instance.logError(e.Message);
                return Enumerable.Empty<string>();
            }
        }

        private static byte[] encryptAndCompressFile(string path)
        {
            byte[] fileAsByteArray = File.ReadAllBytes(path);
            byte[] compressedByteArray = Compressor.compress(fileAsByteArray);
            byte[] encrpytedAndCompressedByteArray = Codec.encrypt(compressedByteArray);
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

        public static string convertCryptPathToPath(string cryptPath)
        {
            string cryptFolderName = cryptPath.Split('>')[0];
            string relativePath = cryptPath.Split('>')[1];

            //Prepends the virtual path of the folder in the cryptdrive where it's stored and removes unneeded parts of the full path on the client
            return FileWatcher.instance.getFoldernameOfCryptFolder(cryptFolderName) + "//" + relativePath;
        }
    }
}
