using System;
using System.Collections.Generic;

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
            // Todo Call Azure BlobAdd
        }

        public void syncFiles(List<string> files)
        {
            // Todo Call Azure BlobSync
        }

        public void deleteFiles(List<string> files)
        {
            // Todo Call Azure BlobDelete
        }
    }
}
