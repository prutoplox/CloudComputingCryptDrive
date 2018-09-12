using NeoSmart.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

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

        private FileNameStorage()
        {
            //Read the filelist from the local folder
            //Download the filelist from the cloud

            string[] clientFiles;
            if (File.Exists(fileMappingFile))
            {
                clientFiles = File.ReadAllLines(fileMappingFile);
                LoadFromStringArray(clientFiles);
            }
            else
            {
                Logger.instance.logError("Could not load the file mapping from disk");
            }

            string[] cloudFiles;
            FileManager.instance.downloadFile(fileMappingFile, fileMappingFileCloud);
            if (File.Exists(fileMappingFileCloud))
            {
                cloudFiles = File.ReadAllLines(fileMappingFileCloud);
                LoadFromStringArray(cloudFiles);
                File.Delete(fileMappingFileCloud);
            }
            else
            {
                Logger.instance.logError("Could not load the file mapping from the cloud");
            }
        }

        private void LoadFromStringArray(string[] clientFiles)
        {
            foreach (string item in clientFiles)
            {
                string[] parts = item.Split(new char[] { '>' });
                string decryptedPath = Codec.decrypt(parts[0]);
                string hash = hashPath(decryptedPath, false);
                if (hash != parts[1])
                {
                    Logger.instance.logError("Hashes do not match for " + decryptedPath);
                }
            }
        }

        //Currently files are only added to the dict without a method to remove files from this mapping again
        public void garbageCollectNotExistingFiles()
        {
            throw new NotImplementedException();
        }

        public void saveMappingToCloud()
        {
            FileManager.instance.uploadFileData(fileMappingFile, File.ReadAllBytes(fileMappingFile));
        }

        private Dictionary<string, string> pathDict = new Dictionary<string, string>();

        public string hashPath(string path)
        {
            return hashPath(path, true);
        }

        public string hashPath(string path, bool saveToFile)
        {
            using (SHA256CryptoServiceProvider hashFunction = new SHA256CryptoServiceProvider())
            {
                string hash = UrlBase64.Encode(hashFunction.ComputeHash(System.Text.Encoding.UTF8.GetBytes(path)));
                string lookup = lookupHash(hash);

                if (lookup == String.Empty)
                {
                    pathDict.Add(hash, path);
                    if (saveToFile)
                    {
                        File.AppendAllText(fileMappingFile, Codec.encrypt(path) + ">" + hash + Environment.NewLine);
                    }
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
