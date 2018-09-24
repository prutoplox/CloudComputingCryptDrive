using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using System.Web.UI.WebControls;

namespace Cryptdrive
{
    public partial class GUIForm : Form
    {
        private bool login = false;
        public static GUIForm instance = new GUIForm();

        public int LeftPadding { get; set; }

        public GUIForm()
        {
            InitializeComponent();

            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;

            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.cd_del, "Automatically remove files on the cloud");
            toolTip1.SetToolTip(this.cd_sync, "Automatically sync files on the cloud");
            toolTip1.SetToolTip(this.db_del, "Automatically remove files on DropBox");
            toolTip1.SetToolTip(this.db_sync, "Automatically sync files on DropBox");

            this.Visible = false;
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                throw new Exception("TODO: GUI Error");
            }
        }

        private void searchFilePath_Click(object sender, EventArgs e)
        {
            string path = "";
            DialogResult pathObject = folderBrowserDialog.ShowDialog();
            if (pathObject == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
                List<string> paths = new List<string>();
                paths.Add(path);

                FileWatcher.instance.monitorDirectory(pathTextField.Text, path);
                FileWatcher.instance.syncClientTreeNode();
            }
        }

        private void helpBtn_Click(object sender, EventArgs e)
        {
        }

        public void listDirectory(List<string> paths)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    //Muss = Else Block sein
                    ucTreeView1.Nodes.Clear();
                }));
            }
            else
            {
                //Muss = If Block sein!
                ucTreeView1.Nodes.Clear();
            }

            foreach (string path in paths)
            {
                var rootDirectoryInfo = new DirectoryInfo(path);

                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        //Muss = Else Block sein!
                        ucTreeView1.Nodes.Add(createDirectoryNode(rootDirectoryInfo, FileWatcher.instance.getMonitoredCryptFolderName(path) + ">"));
                    }));
                }
                else
                {
                    //Muss = If Block sein!
                    ucTreeView1.Nodes.Add(createDirectoryNode(rootDirectoryInfo, FileWatcher.instance.getMonitoredCryptFolderName(path) + ">")); ;
                }

                /*
             System.InvalidOperationException: "Der für dieses Steuerelement durchgeführte Vorgang wird vom falschen Thread aufgerufen.
             Marshallen Sie den richtigen Thread mit Control.Invoke oder Control.BeginInvoke, um den Vorgang auszuführen."
             */
            }
        }

        public void removeCryptpath(string cryptPath)
        {
            CheckBoxHelper item = findCryptPath(cryptPath);
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    //Muss = Else Block sein!
                    item.Parent.Nodes.Remove(item);
                }));
            }
            else
            {
                //Muss = If Block sein!
                item.Parent.Nodes.Remove(item);
            }
        }

        public void removePath(string path)
        {
            removeCryptpath(FileManager.convertPathToCryptPath(path));
        }

        public void insertPath(string path)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    //Muss = Else Block sein!
                    insertCryptPath(FileManager.convertPathToCryptPath(path));
                }));
            }
            else
            {
                //Muss = If Block sein!
                insertCryptPath(FileManager.convertPathToCryptPath(path));
            }
        }

        public void renamePath(string oldPath, string newPath)
        {
            //Only the last foldername or the filename is allowed to change
            int lastIndex = oldPath.LastIndexOf("/");
            string commonPath = oldPath.Substring(0, lastIndex + 1);
            string last = newPath.Replace(commonPath, "");

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    //Muss = Else Block sein!
                    CheckBoxHelper toRename = findCryptPath(FileManager.convertPathToCryptPath(oldPath));
                    toRename.Name = last;
                    toRename.Text = last;
                    toRename.Label = last;
                }));
            }
            else
            {
                //Muss = If Block sein!
                CheckBoxHelper toRename = findCryptPath(FileManager.convertPathToCryptPath(oldPath));
                toRename.Name = last;
                toRename.Text = last;
                toRename.Label = last;
            }
        }

        public CheckBoxHelper findCryptPath(string path)
        {
            foreach (System.Windows.Forms.TreeNode treeNode in ucTreeView1.Nodes)
            {
                if (path.StartsWith(treeNode.Text))
                {
                    return findCryptPath(path.Substring(Math.Min(treeNode.Text.Length + 1, path.Length)), treeNode); //+1 to remove the escaped path separator
                }
            }
            return null;
        }

        public CheckBoxHelper findCryptPath(string path, System.Windows.Forms.TreeNode node)
        {
            foreach (System.Windows.Forms.TreeNode treeNode in node.Nodes)
            {
                if (path == treeNode.Text)
                {
                    return (CheckBoxHelper)treeNode;
                }
                if (path.StartsWith(treeNode.Text))
                {
                    return findCryptPath(path.Substring(Math.Min(treeNode.Text.Length + 1, path.Length)), treeNode); //+1 to remove the escaped path separator
                }
            }
            return null;
        }

        public void insertCryptPath(string path)
        {
            foreach (System.Windows.Forms.TreeNode treeNode in ucTreeView1.Nodes)
            {
                if (path.StartsWith(treeNode.Text))
                {
                    insertCryptPath(path.Substring(Math.Min(treeNode.Text.Length + 1, path.Length)), treeNode); //+1 to remove the escaped path separator
                }
            }
        }

        public void insertCryptPath(string path, System.Windows.Forms.TreeNode node)
        {
            bool needInserting = true;
            foreach (System.Windows.Forms.TreeNode treeNode in node.Nodes)
            {
                if (path == treeNode.Text)
                {
                    //No need to insert the same node again
                    return;
                }
                if (path.StartsWith(treeNode.Text))
                {
                    needInserting = false;
                    insertCryptPath(path.Substring(Math.Min(treeNode.Text.Length + 1, path.Length)), treeNode);
                }
            }
            if (needInserting)
            {
                CheckBoxHelper newNode = new CheckBoxHelper(path, false, false, false, false);
                node.Nodes.Add(newNode);
            }
        }

        private static CheckBoxHelper createDirectoryNode(DirectoryInfo directoryInfo, string naming)
        {
            string nodeName = naming != String.Empty ? naming : directoryInfo.Name;
            CheckBoxHelper directoryNode = new CheckBoxHelper(nodeName, false, false, false, false);

            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Nodes.Add(createDirectoryNode(directory, String.Empty));
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                CheckBoxHelper helper = new CheckBoxHelper(file.Name, false, false, false, false);
                directoryNode.Nodes.Add(helper);
            }

            return directoryNode;
        }

        public void LogToTextBox(string textLine)
        {
            //Called from Logger
            output.AppendText(textLine + Environment.NewLine);
        }

        private void pathTextField_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(pathTextField.Text))
            {
                searchFilePath.Enabled = false;
                return;
            }

            if (FileWatcher.instance.CryptDriveFolders.Contains(pathTextField.Text))
            {
                searchFilePath.Enabled = false;
                return;
            }

            searchFilePath.Enabled = true;
        }

        private void Debug_bt_Click(object sender, EventArgs e)
        {
            Console.WriteLine(FileNameStorage.instance.filePathsInCloudNotOnClientTracked);
            Console.WriteLine(FileNameStorage.instance.filePathsOnClientNotInCloud);

            FileNameStorage.instance.ScheduleUpdate();

            //FileManager.instance.renameFileAsync("large", "newlarge");
        }

        private void GUIForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                return;
            }

            Logger.instance.logInfo("Working in the folder " + Application.StartupPath);
            Directory.CreateDirectory("testFolder");
            if (FileWatcher.instance.CryptDriveFolders.Contains("main"))
            {
                Logger.instance.logDebug("The main crypt folder is already monitored");
            }
            else
            {
                FileWatcher.instance.monitorDirectory("main", "testFolder");
            }
            System.Diagnostics.Process.Start("explorer.exe", @"testFolder");
            FileWatcher.instance.syncClientTreeNode();
            FileNameStorage.instance.Init();

            //TODO handle files on cloud, files newer then cloud, file oder then cloud
        }

        private void GUIForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private bool isCheckingFileMappingFile = false;

        private void FileWatcherSupportTimer_Tick(object sender, EventArgs e)
        {
            if (FileWatcher.instance.needsFileSystemCheck)
            {
                FileWatcher.instance.needsFileSystemCheck = false;
                foreach (var file in FileWatcher.instance.MonitoredFiles)
                {
                    if (!FileNameStorage.instance.isFileTracked(file))
                    {
                        FileManager.instance.syncCryptFile(file, false);
                    }
                }
            }

            if (FileNameStorage.instance.updateCloudFile)
            {
                Logger.instance.logInfo("Saving an up to date filemapping to the cloud");
                FileNameStorage.instance.updateCloudFile = false;
                FileNameStorage.instance.SaveMappingToFileAndCloud();
            }

            if (!isCheckingFileMappingFile)
            {
                isCheckingFileMappingFile = true;
                checkForNewFileMappingFile();
            }
        }

        private async void checkForNewFileMappingFile()
        {
            Logger.instance.logDebug("Checking for new files in the cloud");
            DateTime checkStart = DateTime.Now;
            IEnumerable<string> newFiles = await FileManager.instance.ListNewerFiles(FileNameStorage.instance.lastCloudSync);
            bool downloadNewFiles = false;
            if (newFiles.Any())
            {
                foreach (var item in newFiles)
                {
                    if (item == FileNameStorage.fileMappingFile)
                    {
                        string localTmpFile = "_tmp_" + FileNameStorage.fileMappingFile;
                        await FileManager.instance.downloadFile(FileNameStorage.fileMappingFile, localTmpFile);
                        DateTimeOffset? remoteTimestamp;
                        IEnumerable<string> trackedFiles;
                        FileNameStorage.ParseFile(localTmpFile, out trackedFiles, out remoteTimestamp);
                        if (remoteTimestamp > FileNameStorage.instance.lastSave)
                        {
                            Logger.instance.logInfo("Remote has newer files, preparing to download updated files...");
                            FileNameStorage.instance.AddCloudTrackingFiles(File.ReadLines(localTmpFile).Skip(1), newFiles.Where(X => X != localTmpFile));
                            downloadNewFiles = true;
                        }
                        File.Delete(localTmpFile);
                    }
                }

                if (downloadNewFiles)
                {
                    //FileWatcher.instance.ignoredFiles = newFiles.Where(X => X != FileNameStorage.fileMappingFile).Select(X => FileManager.convertCryptPathToPath(FileNameStorage.instance.lookupHash(X))).Select(X => X.Replace("/", ""));
                    FileWatcher.instance.allEnabled = false;
                    await Task.WhenAll(newFiles                                                                                                  //Wait till all files...
                        .Where(X => X != FileNameStorage.fileMappingFile)                                                                             //...except the mapping gile...
                        .Where(X => FileNameStorage.instance.lookupHash(X) != String.Empty)
                            .Select(X => FileManager.instance.downloadFile(X,                                                                                   //...have been downloaded ...
                                FileManager.convertCryptPathToPath(FileNameStorage.instance.lookupHash(X)))));                                          //...with the right name
                    FileWatcher.instance.allEnabled = true;

                    //FileWatcher.instance.ignoredFiles = Enumerable.Empty<string>();
                }
            }
            isCheckingFileMappingFile = false;
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void dropbox_btn_Click(object sender, EventArgs e)
        {
            AuthenticateDropBox();
            FileManagerDropbox.instance.createFolder();
        }

        public void AuthenticateDropBox()
        {
            try
            {
                string somethingsomething = "q4ela0mxhrm7eti";
                if (string.IsNullOrEmpty(somethingsomething))
                {
                    MessageBox.Show("Please enter valid App Key !");
                    return;
                }
                if (FileManagerDropbox.instance.DBB == null)
                {
                    FileManagerDropbox.instance.DBB = new DropBoxBase(somethingsomething, "CryptDriveSS2018");

                    FileManagerDropbox.instance.strAuthenticationURL = FileManagerDropbox.instance.DBB.GeneratedAuthenticationURL(); // This method must be executed before generating Access Token.
                    FileManagerDropbox.instance.strAccessToken = FileManagerDropbox.instance.DBB.GenerateAccessToken();
                    dropboxsync_btn.Enabled = true;
                    dropbox_image.Visible = true;
                    dropboxdelete_bt.Enabled = true;
                }
                else
                {
                    dropboxsync_btn.Enabled = false;
                    dropbox_image.Visible = false;
                    dropboxdelete_bt.Enabled = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<string> getAllDropBoxSyncPaths()
        {
            foreach (CheckBoxHelper item in getAllTreeChildren())
            {
                //DropBox Sync
                if (item.DropBoxSync)
                {
                    yield return item.FullPath;
                }
            }
        }

        public IEnumerable<string> getAllDropBoxDeletePaths()
        {
            foreach (CheckBoxHelper item in getAllTreeChildren())
            {
                if (item.DropBoxDelete)
                {
                    yield return item.FullPath;
                }
            }
        }

        public IEnumerable<string> getAllCryptDriveDeletePaths()
        {
            foreach (CheckBoxHelper item in getAllTreeChildren())
            {
                if (item.CryptDriveDelete)
                {
                    yield return item.FullPath;
                }
            }
        }

        public IEnumerable<string> getAllCryptDriveSyncPaths()
        {
            foreach (CheckBoxHelper item in getAllTreeChildren())
            {
                if (item.CryptDriveSync)
                {
                    yield return item.FullPath;
                }
            }
        }

        IEnumerable<System.Windows.Forms.TreeNode> CollectAllTreeNodes(System.Windows.Forms.TreeNodeCollection nodes)
        {
            foreach (System.Windows.Forms.TreeNode node in nodes)
            {
                yield return node;

                foreach (var child in CollectAllTreeNodes(node.Nodes))
                    yield return child;
            }
        }

        public IEnumerable<CheckBoxHelper> getAllTreeChildren()
        {
            foreach (CheckBoxHelper item in CollectAllTreeNodes(ucTreeView1.Nodes))
            {
                yield return item;
            }
        }

        private void sync_bt_Click(object sender, EventArgs e)
        {
            foreach (var path in getAllCryptDriveSyncPaths())
            {
                FileManager.instance.syncCryptFile(path, true);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            IEnumerable<string> filesToDelete = getAllCryptDriveDeletePaths();
            FileManager.instance.deleteFiles(filesToDelete, true);

            while (getAllCryptDriveDeletePaths().Any())
            {
                removeCryptpath(getAllCryptDriveDeletePaths().First());
            }
        }

        private void dropboxsync_btn_Click(object sender, EventArgs e)
        {
            FileManagerDropbox.instance.syncFiles(getAllDropBoxSyncPaths());
        }

        private void dropboxdelete_bt_Click(object sender, EventArgs e)
        {
            FileManagerDropbox.instance.deleteFiles(getAllDropBoxDeletePaths());
        }

        private void cd_sync_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.CheckBox box)
            {
                FileManager.instance.syncFilesAutomatically = box.Checked;
            }
            foreach (var cryptpath in FileNameStorage.instance.filePathsOnClientNotInCloud)
            {
                FileManager.instance.syncCryptFile(cryptpath, false);
            }
        }

        private void cd_del_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.CheckBox box)
            {
                FileManager.instance.deleteFilesAutomatically = box.Checked;
            }
        }

        private void db_sync_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.CheckBox box)
            {
                FileManagerDropbox.instance.syncFilesAutomatically = box.Checked;
            }
        }

        private void db_del_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.CheckBox box)
            {
                FileManagerDropbox.instance.deleteFilesAutomatically = box.Checked;
            }
        }
    }
}
