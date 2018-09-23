﻿using System;
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
        public static FileWatcher instance = new FileWatcher();
        private Dictionary<string, FileSystemWatcher> fileSystemWatchers = new Dictionary<string, FileSystemWatcher>();

        public IEnumerable<string> ignoredFiles { get; set; } = Enumerable.Empty<string>();

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
                    Logger.instance.logError("Could not load the folder mapping from the mapping file, will create a new mapping");
                    Logger.instance.logError(e.Message);
                }
            }
        }

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

        public IEnumerable<string> MonitoredFiles
        {
            get
            {
                foreach (var folder in MonitoredFolders)
                {
                    foreach (var file in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))
                    {
                        string cleanFile = makeCleanPaths(file);
                        yield return FileManager.convertPathToCryptPath(cleanFile);
                    }
                }
            }
        }

        public bool needsFileSystemCheck { get; set; }

        public bool allEnabled
        {
            set
            {
                foreach (var item in fileSystemWatchers)
                {
                    item.Value.EnableRaisingEvents = value;
                }
            }
        }

        public IEnumerable<string> MonitoredFilesNewerThen(DateTimeOffset timestamp)
        {
            foreach (var folder in MonitoredFolders)
            {
                foreach (var file in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))
                {
                    string cleanFile = makeCleanPaths(file);
                    if (File.GetLastWriteTimeUtc(cleanFile) > timestamp)
                    {
                        yield return FileManager.convertPathToCryptPath(file);
                    }
                }
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
            fileSystemWatcher.Error += FileSystemWatcher_Error;
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

        private void FileSystemWatcher_Error(object sender, ErrorEventArgs e)
        {
            Logger.instance.logError("Too many changes at once, scheduling a full scan to make sure no file was missed...");
            needsFileSystemCheck = true;
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            string path = makeCleanPaths(e.FullPath);
            if (ignoredFiles.Contains(path))
            {
                Logger.instance.logInfo("Ignoring " + path + " since it's being synced from the cloud");
                return;
            }

            if (!File.Exists(path))
            {
                //Deletion sometimes calls the file change event for some yet unknown reason...
                fileSystemWatcher_Deleted(sender, e);
                return;
            }

            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                Logger.instance.logInfo("Folder changed: " + e.Name);

                //Should be no need to iterate over the files in the folder since they should raise a separate change/create event
                //FileManager.instance.syncFiles(filesInFolder(path));
            }
            else
            {
                Logger.instance.logInfo("File changed: " + e.Name);
                FileManager.instance.syncFile(path, false);
            }

            //syncClientTreeNode();
            GUIForm.instance.insertPath(path);
        }

        private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            string path = makeCleanPaths(e.FullPath);
            if (ignoredFiles.Contains(path))
            {
                Logger.instance.logInfo("Ignoring " + path + " since it's being synced from the cloud");
                return;
            }

            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                Logger.instance.logInfo("Folder created: " + e.Name);

                //The changed path is a directory, iterate over all files in it
                FileManager.instance.syncFiles(filesInFolder(path));
            }
            else
            {
                Logger.instance.logInfo("File created: " + e.Name);
                FileManager.instance.syncFile(path, false);
            }

            GUIForm.instance.insertPath(path);

            //syncClientTreeNode();
        }

        private void fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            string path = makeCleanPaths(e.FullPath);
            string oldPath = makeCleanPaths(e.OldFullPath);
            if (ignoredFiles.Contains(path))
            {
                Logger.instance.logInfo("Ignoring " + path + " since it's being synced from the cloud");
                return;
            }

            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                Logger.instance.logInfo("Folder " + oldPath + " was renamed to " + path);

                //The changed path is a directory, iterate over all files in it
                foreach (var fileName in filesInFolder(path))
                {
                    FileManager.instance.renameFileHashedNames(fileName.Replace(path, oldPath), fileName);
                }
            }
            else
            {
                Logger.instance.logInfo("File " + oldPath + " was renamed to " + path);
                FileManager.instance.renameFileHashedNames(oldPath, path);
            }

            //syncClientTreeNode();
            GUIForm.instance.renamePath(oldPath, path);
        }

        private void fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string path = makeCleanPaths(e.FullPath);
            if (ignoredFiles.Contains(path))
            {
                Logger.instance.logInfo("Ignoring " + path + " since it's being synced from the cloud");
                return;
            }

            Logger.instance.logInfo("File or Folder deleted: " + path);
            IEnumerable<string> names = FileNameStorage.instance.getFilesWithPrefix(FileManager.convertPathToCryptPath(path));
            FileManager.instance.deleteFiles(names);

            //syncClientTreeNode();

            GUIForm.instance.insertPath(path);
        }

        public void syncClientTreeNode()
        {
            List<string> paths = new List<string>();
            foreach (FileSystemWatcher watcher in fileSystemWatchers.Values)
            {
                paths.Add(makeCleanPaths(watcher.Path));
            }
            GUIForm.instance.listDirectory(paths);
        }

        internal string getMonitoredCryptFolderName(string path)
        {
            foreach (var item in fileSystemWatchers)
            {
                string monitoredPath = makeCleanPaths(item.Value.Path);
                if (path.Substring(0, Math.Min(monitoredPath.Length, path.Length)) == monitoredPath)
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
                string monitoredPath = makeCleanPaths(item.Value.Path);
                if (path.Substring(0, Math.Min(monitoredPath.Length, path.Length)) == monitoredPath)
                {
                    return monitoredPath;
                }
            }
            Logger.instance.logError("File was not in monitored folder!");
            return "";
        }

        public IEnumerable<string> filesInFolder(string path)
        {
            foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                yield return makeCleanPaths(file);
            }
        }

        public string getFoldernameOfCryptFolder(string cryptfolder)
        {
            fileSystemWatchers.TryGetValue(cryptfolder, out FileSystemWatcher watcher);
            return makeCleanPaths(watcher.Path);
        }

        public DateTime getLastWriteToCryptFileUTC(string cryptPath)
        {
            //Convert to physical path
            string path = getFoldernameOfCryptFolder(cryptPath);

            //read attribute about last write
            if (File.Exists(path))
            {
                return File.GetLastWriteTimeUtc(path);
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        static string makeCleanPaths(string path)
        {
            string returnValue = path.Replace("\\", "/");
            return returnValue;
        }
    }
}
