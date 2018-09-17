﻿using NeoSmart.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Threading.Tasks;

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

        public IEnumerable<string> filePathsInCloudNotOnClientTracked;
        public IEnumerable<string> filePathsOnClientNotInCloud;

        public async void Init()
        {
            //Read the filelist.txt from the local folder
            IEnumerable<string> trackedClientFiles = Enumerable.Empty<string>();//avoid null reference
            DateTime? timeStampClient = null;
            if (File.Exists(fileMappingFile))
            {
                ParseFile(fileMappingFile, out trackedClientFiles, out timeStampClient);
                Logger.instance.logInfo("Loaded local file mapping file");
            }
            else
            {
                Logger.instance.logError("Could not load the file mapping from disk");
            }

            //Download the filelist from the cloud
            IEnumerable<string> trackedCloudFiles = Enumerable.Empty<string>(); //avoid null reference
            DateTime? timeStampCloud = null;
            await FileManager.instance.downloadFile(fileMappingFile, fileMappingFileCloud);
            if (File.Exists(fileMappingFileCloud))
            {
                ParseFile(fileMappingFileCloud, out trackedCloudFiles, out timeStampCloud);
                File.Delete(fileMappingFileCloud);
                Logger.instance.logInfo("Loaded remote file mapping file");
            }
            else
            {
                Logger.instance.logError("Could not load the file mapping from the cloud");
            }

            IEnumerable<string> filesOnClient = FileWatcher.instance.MonitoredFiles;

            IEnumerable<string> filesNeedToBeTracked;

            //client & cloud are there, => use newer tracking and merge file on client into it(just as one of them is null)
            if (timeStampCloud != null && timeStampClient != null)
            {
                DateTime newerTimeStamp;
                bool localIsNewer;
                if (timeStampClient > timeStampCloud)
                {
                    localIsNewer = true;
                    newerTimeStamp = (DateTime)timeStampClient; //can't be null since timstamp of client and cloud must not be null here
                }
                else
                {
                    localIsNewer = false;
                    newerTimeStamp = (DateTime)timeStampCloud; //can't be null since timstamp of client and cloud must not be null here
                }

                Task<IEnumerable<string>> cloudFilesNewserThenClientTimestampTask = FileManager.instance.ListNewerFiles((DateTime)timeStampClient);

                IEnumerable<string> clientFilesNewerThenCloudTimestamp = FileWatcher.instance.MonitoredFilesNewerThen((DateTime)timeStampCloud);

                await cloudFilesNewserThenClientTimestampTask;
                IEnumerable<string> cloudFilesNewserThenClientTimestamp = cloudFilesNewserThenClientTimestampTask.Result;

                //Got a list of files on the server and client which are newer then the counterpart(client/server)

                //TODO FIX BELOW

                filesNeedToBeTracked = filesOnClient.Except(trackedClientFiles);
                filePathsInCloudNotOnClientTracked = Enumerable.Empty<string>(); //TODO
                filePathsOnClientNotInCloud = Enumerable.Empty<string>();//TODO
            }

            //both null -> not yet tracked => make new with all files in folder
            else if (timeStampCloud == null && timeStampClient == null)
            {
                filesNeedToBeTracked = filesOnClient;
                filePathsInCloudNotOnClientTracked = Enumerable.Empty<string>();
                filePathsOnClientNotInCloud = filesOnClient; //ok
            }

            //cloud null -> local tracked => check if some new files appeared and remove not existant files from tracking and upload new tracking to cloud
            else if (timeStampCloud == null && timeStampClient != null)
            {
                filesNeedToBeTracked = filesOnClient.Except(trackedClientFiles);
                filePathsOnClientNotInCloud = filesOnClient;
                filePathsInCloudNotOnClientTracked = Enumerable.Empty<string>(); //ok
            }

            //client null -> client is a new computer => download files and merge files on computer into it
            else if (timeStampCloud != null && timeStampClient == null)
            {
                filesNeedToBeTracked = filesOnClient.Except(trackedClientFiles);
                filePathsOnClientNotInCloud = filesOnClient.Except(trackedCloudFiles);
                filePathsInCloudNotOnClientTracked = trackedCloudFiles.Except(trackedClientFiles);
            }
            else
            {
                throw new Exception("Unreachable statement got reached!");
            }

            foreach (string item in filesNeedToBeTracked)
            {
                hashPath(item);
            }
            SaveMappingToFile();
        }

        private static void ParseFile(string filename, out IEnumerable<string> trackedFiles, out DateTime? timestampFile)
        {
            trackedFiles = File.ReadAllLines(filename);
            timestampFile = DateTime.Parse(trackedFiles.First());
            trackedFiles = trackedFiles.Skip(1);
            trackedFiles = trackedFiles.Select(X => Codec.decrypt(X.Split('>')[1]));
        }

        public void SaveMappingToFileAndCloud()
        {
            SaveMappingToFile();
            saveMappingToCloud();
        }

        public void SaveMappingToFile()
        {
            StreamWriter writer = File.AppendText("_" + fileMappingFile);

            writer.WriteLine(DateTime.UtcNow);
            foreach (var item in pathDict)
            {
                writer.WriteLine(item.Key + ">" + Codec.encrypt(item.Value));
            }
            writer.Flush();
            writer.Close();
            File.Delete(fileMappingFile);
            File.Move("_" + fileMappingFile, fileMappingFile);
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

        internal void removeMappings(IEnumerable<string> names)
        {
            List<string> filesToRemove = new List<string>();
            foreach (var file in pathDict)
            {
                if (names.Contains(file.Value))
                {
                    filesToRemove.Add(file.Key);
                }
            }
            foreach (var file in filesToRemove)
            {
                pathDict.Remove(file);
            }
        }

        internal IEnumerable<string> getFilesWithPrefix(string v)
        {
            List<string> files = new List<string>();
            foreach (var item in pathDict.Values)
            {
                if (item.StartsWith(v))
                {
                    files.Add(item);
                }
            }
            return files;
        }
    }
}
