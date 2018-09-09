using System;
using System.Collections.Generic;
using System.IO;

namespace Cryptdrive
{
    // Function 1: Add path to FileWatcher(String path)
    // Function 2: Remove path to FileWatcher(String path)
    class FileWatcher
    {
        private FileWatcher()
        {
        }

        public static FileWatcher instance = new FileWatcher();
        private List<FileSystemWatcher> fileSystemWatchers = new List<FileSystemWatcher>();

        public void monitorDirectory(string path)
        {
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = path;
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.Created += fileSystemWatcher_Created;
            fileSystemWatcher.Renamed += fileSystemWatcher_Renamed;
            fileSystemWatcher.Deleted += fileSystemWatcher_Deleted;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatchers.Add(fileSystemWatcher);
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File changed: {0}", e.Name);
            List<string> temp = new List<string>();
            temp.Add(e.FullPath);
            FileManager.instance.syncFiles(temp);
            syncClientTreeNode();
        }

        private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File created: {0}", e.Name);
            syncClientTreeNode();
        }

        private void fileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File renamed: {0}", e.Name);
            syncClientTreeNode();
        }

        private void fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File deleted: {0}", e.Name);
            syncClientTreeNode();
        }

        public void syncClientTreeNode()
        {
            List<string> paths = new List<string>();
            foreach (FileSystemWatcher watcher in fileSystemWatchers)
            {
                paths.Add(watcher.Path);
            }
            GUIForm.instance.listDirectory(paths);
        }
    }
}
