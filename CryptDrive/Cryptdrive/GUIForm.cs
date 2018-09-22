using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptdrive
{
    public partial class GUIForm : Form
    {
        private bool login = false;
        public static GUIForm instance = new GUIForm();

        public GUIForm()
        {
            InitializeComponent();
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

        private void delete_Click(object sender, EventArgs e)
        {
            //TODO ONLY USE CHECKED FILES
            IEnumerable<string> paths = FileWatcher.instance.MonitoredFiles;
            FileManager.instance.deleteFiles(paths);
        }

        private void searchFile_Click(object sender, EventArgs e)
        {
        }

        public void listDirectory(List<string> paths)
        {
            TreeView treeView = treeView1;

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    //Muss = Else Block sein
                    treeView.Nodes.Clear();
                }));
            }
            else
            {
                //Muss = If Block sein!
                treeView.Nodes.Clear();
            }

            foreach (string path in paths)
            {
                var rootDirectoryInfo = new DirectoryInfo(path);

                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        //Muss = Else Block sein!
                        treeView.CheckBoxes = true;

                        //TODO https://stackoverflow.com/questions/28644011/how-to-show-multiple-check-boxes-in-a-treeview-in-c
                        treeView.Nodes.Add(createDirectoryNode(rootDirectoryInfo));
                    }));
                }
                else
                {
                    //Muss = If Block sein!
                    treeView.CheckBoxes = true;

                    //TODO https://stackoverflow.com/questions/28644011/how-to-show-multiple-check-boxes-in-a-treeview-in-c
                    treeView.Nodes.Add(createDirectoryNode(rootDirectoryInfo)); ;
                }

                /*
             System.InvalidOperationException: "Der für dieses Steuerelement durchgeführte Vorgang wird vom falschen Thread aufgerufen.
             Marshallen Sie den richtigen Thread mit Control.Invoke oder Control.BeginInvoke, um den Vorgang auszuführen."
             */
            }
        }

        private static TreeNode createDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(createDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Nodes.Add(new TreeNode(file.Name));
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

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void GUIForm_Load(object sender, EventArgs e)
        {
        }

        private void GUIForm_Activated(object sender, EventArgs e)
        {
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
                        FileManager.instance.syncCryptFile(file);
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

        private void GUIForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
