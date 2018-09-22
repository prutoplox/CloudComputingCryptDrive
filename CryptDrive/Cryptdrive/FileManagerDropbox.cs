using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptdrive
{
    class FileManagerDropbox
    {
        public string strAccessToken = string.Empty;
        public string strAuthenticationURL = string.Empty;
        public DropBoxBase DBB;

        public static FileManagerDropbox instance = new FileManagerDropbox();

        private FileManagerDropbox()
        { }

        public bool syncFilesAutomatically { get; set; }

        public bool deleteFilesAutomatically { get; set; }

        private string getDropboxCryptDriveContainerNamePath()
        {
            return "/Dropbox/CryptDriveSS2018/" + FileManager.instance.containerName;
        }

        public void createFolder()
        {
            try
            {
                if (DBB != null)
                {
                    if (strAccessToken != null && strAuthenticationURL != null)
                    {
                        if (DBB.FolderExists(getDropboxCryptDriveContainerNamePath()) == false)
                        {
                            DBB.CreateFolder(getDropboxCryptDriveContainerNamePath());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex = ex.InnerException ?? ex;
            }
        }

        public async Task<bool> syncFiles(IEnumerable<string> cryptpaths)
        {
            try
            {
                if (!syncFilesAutomatically)
                {
                    return false;
                }
                foreach (string cryptpath in cryptpaths)
                {
                    string realpath = FileManager.convertCryptPathToPath(cryptpath);
                    string hashpath = FileNameStorage.instance.hashPath(cryptpath);

                    if (DBB != null)
                    {
                        if (strAccessToken != null && strAuthenticationURL != null)
                        {
                            return DBB.Upload(getDropboxCryptDriveContainerNamePath(), hashpath, realpath);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public async Task<bool> renameFiles(IEnumerable<string> oldCryptFileNames, IEnumerable<string> newCryptFileNames)
        {
            if (oldCryptFileNames.Count() != newCryptFileNames.Count())
            {
                return false;
            }
            try
            {
                if (!syncFilesAutomatically)
                {
                    return false;
                }
                foreach (string cryptpathold in oldCryptFileNames)
                {
                    if (DBB != null)
                    {
                        if (strAccessToken != null && strAuthenticationURL != null)
                        {
                            string hashpathold = FileNameStorage.instance.hashPath(cryptpathold);
                            string cryptpathnew = newCryptFileNames.First();
                            newCryptFileNames = newCryptFileNames.Skip(1);
                            string hashpathnew = FileNameStorage.instance.hashPath(cryptpathnew);
                            return DBB.Rename(hashpathold, hashpathnew);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public async Task<bool> deleteFiles(IEnumerable<string> cryptpaths)
        {
            try
            {
                if (!deleteFilesAutomatically)
                {
                    return false;
                }
                foreach (string cryptpath in cryptpaths)
                {
                    if (DBB != null)
                    {
                        if (strAccessToken != null && strAuthenticationURL != null)
                        {
                            string hashpath = FileNameStorage.instance.hashPath(cryptpath);
                            return DBB.Delete(getDropboxCryptDriveContainerNamePath() + "/" + hashpath);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public async Task<bool> downloadFile(string cryptpath, string localPath)
        {
            try
            {
                if (DBB != null)
                {
                    if (strAccessToken != null && strAuthenticationURL != null)
                    {
                        return DBB.Download(getDropboxCryptDriveContainerNamePath(), FileNameStorage.instance.hashPath(cryptpath), localPath);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
    }
}
