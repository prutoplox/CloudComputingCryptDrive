using NeoSmart.Utils;
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
        public const string fileMappingFile = "filelist.txt";
        public const string fileMappingFileCloudPrefix = "Cloud";
        public const string fileMappingFileCloud = fileMappingFileCloudPrefix + fileMappingFile;

        public static FileNameStorage instance = new FileNameStorage();

        /// <summary>
        /// [hash] = cryptpath
        /// </summary>
        private Dictionary<string, string> pathDict;

        public DateTimeOffset lastSave { get; private set; }

        public bool updateCloudFile { get; set; }

        /// <summary>
        /// Only updated after Init is called!
        /// </summary>
        public IEnumerable<string> filePathsInCloudNotOnClientTracked;

        /// <summary>
        /// Only updated after Init is called!
        /// </summary>
        public IEnumerable<string> filePathsOnClientNotInCloud;

        /// <summary>
        /// Only updated after Init is called!
        /// </summary>
        public IEnumerable<string> cloudFilesNewserThenClientTimestamp;

        /// <summary>
        /// Only updated after Init is called!
        /// </summary>
        public IEnumerable<string> clientFilesNewerThenCloudTimestamp;

        public async void Init()
        {
            pathDict = new Dictionary<string, string>();

            //Read the filelist.txt from the local folder
            IEnumerable<string> trackedClientFiles = Enumerable.Empty<string>(); //avoid null reference
            DateTimeOffset? timeStampClient = null;
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
            DateTimeOffset? timeStampCloud = null;
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
                lastSave = (DateTimeOffset)timeStampClient;
                Task<IEnumerable<string>> cloudFilesNewserThenClientTimestampTask = FileManager.instance.ListNewerFiles((DateTimeOffset)timeStampClient);

                clientFilesNewerThenCloudTimestamp = FileWatcher.instance.MonitoredFilesNewerThen((DateTimeOffset)timeStampCloud);

                await cloudFilesNewserThenClientTimestampTask;
                cloudFilesNewserThenClientTimestamp = cloudFilesNewserThenClientTimestampTask.Result;

                //Got a list of files on the server and client which are newer then the counterpart(client/server)

                filesNeedToBeTracked = filesOnClient.Except(trackedClientFiles);
                filePathsInCloudNotOnClientTracked = trackedCloudFiles.Except(filesOnClient);
                filePathsOnClientNotInCloud = filesOnClient.Except(trackedCloudFiles);
            }

            //both null -> not yet tracked => make new with all files in folder
            else if (timeStampCloud == null && timeStampClient == null)
            {
                lastSave = DateTimeOffset.UtcNow;
                filesNeedToBeTracked = filesOnClient;
                filePathsInCloudNotOnClientTracked = Enumerable.Empty<string>();
                filePathsOnClientNotInCloud = filesOnClient; //ok
            }

            //cloud null -> local tracked => check if some new files appeared and remove not existant files from tracking and upload new tracking to cloud
            else if (timeStampCloud == null && timeStampClient != null)
            {
                lastSave = (DateTimeOffset)timeStampClient;
                filesNeedToBeTracked = filesOnClient.Except(trackedClientFiles);
                filePathsOnClientNotInCloud = filesOnClient;
                filePathsInCloudNotOnClientTracked = Enumerable.Empty<string>(); //ok
            }

            //client null -> client is a new computer => download files and merge files on computer into it
            else if (timeStampCloud != null && timeStampClient == null)
            {
                lastSave = DateTimeOffset.UtcNow;
                filesNeedToBeTracked = filesOnClient.Except(trackedClientFiles);
                filePathsOnClientNotInCloud = filesOnClient.Except(trackedCloudFiles);
                filePathsInCloudNotOnClientTracked = trackedCloudFiles.Except(trackedClientFiles);
            }
            else
            {
                throw new Exception("Unreachable statement got reached!");
            }

            foreach (string item in filesOnClient.Union(trackedClientFiles))
            {
                hashPath(item, true);
            }
            SaveMappingToFile();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="trackedCloud">Lines which hold the encrypted filename and the hash of the filename</param>
        /// <param name="changedFiles">hashes of filenames which changed </param>
        public void AddCloudTrackingFiles(IEnumerable<string> trackedCloud, IEnumerable<string> changedFiles)
        {
            List<string> filesInCloud = new List<string>();

            //Add all cloud files to the tracking
            foreach (var line in trackedCloud)
            {
                string[] parths = line.Split('>');
                string cryptPath = Codec.decrypt(parths[1]);
                filesInCloud.Add(cryptPath);
            }
            filePathsOnClientNotInCloud = pathDict.Values.Except(filesInCloud);
            filePathsInCloudNotOnClientTracked = filesInCloud.Except(pathDict.Values);

            //Make a lookupa aviable for looking which file changed
            foreach (var file in filesInCloud)
            {
                hashPath(file, true);
            }

            cloudFilesNewserThenClientTimestamp = changedFiles.Select(X => lookupHash(X));
        }

        public static void ParseFile(string filename, out IEnumerable<string> trackedFiles, out DateTimeOffset? timestampFile)
        {
            trackedFiles = File.ReadAllLines(filename);
            timestampFile = DateTimeOffset.Parse(trackedFiles.First());
            trackedFiles = trackedFiles.Skip(1);
            trackedFiles = trackedFiles.Select(X => Codec.decrypt(X.Split('>')[1]));
        }

        public static DateTimeOffset GetDateTimeFromFile(string filename)
        {
            string datetimeString = File.ReadLines(filename).First();
            return DateTimeOffset.Parse(datetimeString);
        }

        public void ScheduleUpdate()
        {
            updateCloudFile = true;
        }

        public async Task<bool> SaveMappingToFileAndCloud()
        {
            lastSave = DateTimeOffset.Parse(DateTimeOffset.UtcNow.ToString("u")); //do some rounding, does not need to be fast
            return SaveMappingToFile() && await saveMappingToCloud();
        }

        private bool SaveMappingToFile()
        {
            try
            {
                StreamWriter writer = File.AppendText("_" + fileMappingFile);

                writer.WriteLine(lastSave);
                foreach (var item in pathDict)
                {
                    writer.WriteLine(item.Key + ">" + Codec.encrypt(item.Value));
                }
                writer.Flush();
                writer.Close();
                File.Delete(fileMappingFile);
                File.Move("_" + fileMappingFile, fileMappingFile);
                Logger.instance.logInfo("Saved an up to date file mapping on the client");
                return true;
            }
            catch (Exception e)
            {
                Logger.instance.logError(e.Message);
                return false;
            }
        }

        private async Task<bool> saveMappingToCloud()
        {
            var wasUploaded = await FileManager.instance.uploadFileData(fileMappingFile, File.ReadAllBytes(fileMappingFile));
            if (wasUploaded)
            {
                Logger.instance.logInfo("Saved an up to date file mapping to the cloud");
            }
            else
            {
                Logger.instance.logError("Unable to save an up to date file mapping to the cloud");
            }
            return wasUploaded;
        }

        public string hashPath(string path)
        {
            return hashPath(path, false);
        }

        public string hashPath(string path, bool track)
        {
            using (SHA256CryptoServiceProvider hashFunction = new SHA256CryptoServiceProvider())
            {
                string hash = UrlBase64.Encode(hashFunction.ComputeHash(System.Text.Encoding.UTF8.GetBytes(path)));
                if (track)
                {
                    AddToTracking(path, hash);
                }

                return hash;
            }
        }

        public void AddToTracking(string path, string hash)
        {
            string lookup = lookupHash(hash);

            if (lookup == String.Empty)
            {
                pathDict.Add(hash, path);
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

        internal void removeMapping(string name)
        {
            if (pathDict.Values.Contains(name))
            {
                pathDict.Remove(hashPath(name));
            }
        }

        internal void removeMappings(IEnumerable<string> names)
        {
            foreach (string item in names)
            {
                removeMapping(item);
            }
        }

        public bool isFileTracked(string cryptPath)
        {
            return pathDict.Values.Contains(cryptPath);
        }

        public async Task<bool> isFileUploaded(string cryptPath)
        {
            return await FileManager.instance.getURL(cryptPath) != String.Empty;
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
