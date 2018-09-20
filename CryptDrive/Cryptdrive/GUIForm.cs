using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

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

            FileNameStorage.instance.SaveMappingToFileAndCloud();

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
        private DateTime lastSuccessFullCheck = DateTime.Now;

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

            if (!isCheckingFileMappingFile)
            {
                isCheckingFileMappingFile = true;
                checkForNewFileMappingFile(lastSuccessFullCheck);
            }
        }

        private async void checkForNewFileMappingFile(DateTime checkStartTime)
        {
            Logger.instance.logDebug("Checking for new files in the cloud");
            IEnumerable<string> newFiles = await FileManager.instance.ListNewerFiles((DateTime)checkStartTime - new TimeSpan(0, 0, FileWatcherSupportTimer.Interval / 1000));
            if (newFiles.Any())
            {
                foreach (var item in newFiles)
                {
                    if (item == FileNameStorage.fileMappingFile)
                    {
                        string localTmpFile = "_tmp_" + FileNameStorage.fileMappingFile;
                        await FileManager.instance.downloadFile(FileNameStorage.fileMappingFile, localTmpFile);
                        DateTime remoteTimestamp = FileNameStorage.GetDateTimeFromFile(localTmpFile);
                        File.Delete(localTmpFile);
                        if (remoteTimestamp != FileNameStorage.instance.lastSave)
                        {
                            Logger.instance.logInfo("Remote has newer files, preparing to download updated files...");

                            //TODO do stuff
                        }
                    }
                }
                Logger.instance.logDebug("A new file has been found");
            }
            lastSuccessFullCheck = DateTime.Now;
            isCheckingFileMappingFile = false;
        }

        private void GUIForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
