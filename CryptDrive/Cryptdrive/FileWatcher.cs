using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cryptdrive
{
    // Function 1: Add path to FileWatcher(String path)
    // Function 2: Remove path to FileWatcher(String path)
    class FileWatcher
    {
        private static string folderMappingFile = "Foldermapping.txt";

        private FileWatcher()
        {
            if (File.Exists(folderMappingFile))
            {
                try
                {
                    string[] lines = File.ReadAllLines(folderMappingFile);
                    Logger.instance.logInfo("Loaded the existing mappings from the file " + folderMappingFile);
                    foreach (var line in lines)
                    {
                        string[] parts = line.Split(new char[] { '>' });
                        monitorDirectory(parts[0], parts[1], false);
                    }

                    return;
                }
                catch (Exception e)
                {
                    Logger.instance.logError("Could not load the encryption key from the file, creating a new key for this session.");
                }
            }
        }

        public static FileWatcher instance = new FileWatcher();
        private Dictionary<string, FileSystemWatcher> fileSystemWatchers = new Dictionary<string, FileSystemWatcher>();

        public IEnumerable<string> MonitoredFolders
        {
            get
            {
                return fileSystemWatchers.Select(X => X.Value.Path);
            }
        }

        public IEnumerable<string> CryptDriveFolders
        {
            get
            {
                return fileSystemWatchers.Select(X => X.Key);
            }
        }

        public void monitorDirectory(string cryptDrivepathname, string path)
        {
            monitorDirectory(cryptDrivepathname, path, !MonitoredFolders.Contains(path));
        }

        public void monitorDirectory(string cryptDrivepathname, string path, bool isNewlyMonitored)
        {
            if (fileSystemWatchers.Keys.Contains(cryptDrivepathname))
            {
                Logger.instance.logError(cryptDrivepathname + " is already a monitored Cryptdrive folder!");
                return;
            }

            //want to make relative paths unambigitous where they are located.
            path = Path.GetFullPath(path);

            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = path;
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.Created += fileSystemWatcher_Created;
            fileSystemWatcher.Renamed += fileSystemWatcher_Renamed;
            fileSystemWatcher.Deleted += fileSystemWatcher_Deleted;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatchers.Add(cryptDrivepathname, fileSystemWatcher);

            if (isNewlyMonitored)
            {
                File.AppendAllText(folderMappingFile, cryptDrivepathname + ">" + path + Environment.NewLine);
            }
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Logger.instance.logInfo("File changed: " + e.Name);
            List<string> temp = new List<string>();
            temp.Add(e.FullPath);

            FileManager.instance.syncFiles(temp);
            syncClientTreeNode();
        }

        private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Logger.instance.logInfo("File created: " + e.Name);
            syncClientTreeNode();
        }

        private void fileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)
        {
            Logger.instance.logInfo("File renamed: " + e.Name);
            syncClientTreeNode();
        }

        private void fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Logger.instance.logInfo("File deleted: " + e.Name);
            syncClientTreeNode();
        }

        public void syncClientTreeNode()
        {
            List<string> paths = new List<string>();
            foreach (FileSystemWatcher watcher in fileSystemWatchers.Values)
            {
                paths.Add(watcher.Path);
            }
            GUIForm.instance.listDirectory(paths);
        }

        internal string getMonitoredCryptFolderName(string path)
        {
            foreach (var item in fileSystemWatchers)
            {
                string monitoredPath = item.Value.Path;
                if (path.Substring(0, monitoredPath.Length) == monitoredPath)
                {
                    return item.Key;
                }
            }
            Logger.instance.logError("File was not in monitored folder!");
            return "";
        }

        internal string getMonitoredFolderName(string path)
        {
            foreach (var item in fileSystemWatchers)
            {
                string monitoredPath = item.Value.Path;
                if (path.Substring(0, monitoredPath.Length) == monitoredPath)
                {
                    return monitoredPath;
                }
            }
            Logger.instance.logError("File was not in monitored folder!");
            return "";
        }
    }
}
