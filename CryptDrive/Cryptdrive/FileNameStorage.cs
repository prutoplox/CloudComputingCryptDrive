using NeoSmart.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Linq;

namespace Cryptdrive
{
    //Verwalten eines Mapping Files (Datenfeld im FileStorge? Datentyp?)
    // Function 1 : Übersetzen von Dateinamen in Blobname (String -> String)
    // Function 2 : Übersetzen von Blobname in Dateinamen (String -> String)

    class FileNameStorage
    {
        private const string fileMappingFile = "filelist.txt";
        private const string fileMappingFileCloudPrefix = "Cloud";
        private const string fileMappingFileCloud = fileMappingFileCloudPrefix + fileMappingFile;

        public static FileNameStorage instance = new FileNameStorage();
        private Dictionary<string, string> pathDict = new Dictionary<string, string>();

        public void Init()
        {
            //Read the filelist.txt from the local folder
            IEnumerable<string> trackedClientFiles = Enumerable.Empty<string>();//avoid null reference
            DateTime? timeStampClient;
            if (File.Exists(fileMappingFile))
            {
                trackedClientFiles = File.ReadAllLines(fileMappingFile);
                timeStampClient = DateTime.FromFileTime(Int64.Parse(trackedClientFiles.First()));
            }
            else
            {
                Logger.instance.logError("Could not load the file mapping from disk");
            }

            //Download the filelist from the cloud
            IEnumerable<string> trackedCloudFiles = Enumerable.Empty<string>(); //avoid null reference
            DateTime? timeStampCloud;
            FileManager.instance.downloadFile(fileMappingFile, fileMappingFileCloud);
            if (File.Exists(fileMappingFileCloud))
            {
                trackedCloudFiles = File.ReadAllLines(fileMappingFileCloud);
                timeStampCloud = DateTime.FromFileTime(Int64.Parse(trackedCloudFiles.First()));

                File.Delete(fileMappingFileCloud);
            }
            else
            {
                Logger.instance.logError("Could not load the file mapping from the cloud");
            }

            //Get a list of files which are currently on the file system
            foreach (var file in FileWatcher.instance.MonitoredFiles)
            {
            }

            IEnumerable<string> untrackedClientFiles = FileWatcher.instance.MonitoredFiles;

            //both null -> not yet tracked => make new with all files in folder
            //cloud null -> local tracked => check if some new files appeared and upload new tracking to cloud
            //client null -> client is a new computer => download files and merge files on computer into it
            //client & cloud are there, => use newer tracking and merge file on client into it
        }

        private void LoadFromStrings(IEnumerable<string> clientFiles)
        {
            foreach (string item in clientFiles)
            {
                string[] parts = item.Split(new char[] { '>' });
                string decryptedPath = Codec.decrypt(parts[0]);
                string hash = hashPath(decryptedPath);
                if (hash != parts[1])
                {
                    Logger.instance.logError("Hashes do not match for " + decryptedPath);
                }
            }
        }

        public void saveMappingToCloud()
        {
            FileManager.instance.uploadFileData(fileMappingFile, File.ReadAllBytes(fileMappingFile));
        }

        public string hashPath(string path)
        {
            using (SHA256CryptoServiceProvider hashFunction = new SHA256CryptoServiceProvider())
            {
                string hash = UrlBase64.Encode(hashFunction.ComputeHash(System.Text.Encoding.UTF8.GetBytes(path)));
                string lookup = lookupHash(hash);

                if (lookup == String.Empty)
                {
                    pathDict.Add(hash, path);
                }

                return hash;
            }
        }

        public string lookupHash(string hash)
        {
            string value;
            if (pathDict.TryGetValue(hash, out value))
            {
                return value;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
