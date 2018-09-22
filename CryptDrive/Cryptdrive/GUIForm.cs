using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
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

        //dropbox Variables

        private string strAccessToken = string.Empty;
        private string strAuthenticationURL = string.Empty;
        private DropBoxBase DBB;

        public int LeftPadding { get; set; }

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

        private static CheckBoxHelper createDirectoryNode(DirectoryInfo directoryInfo, string naming)
        {
            string nodeName = naming != String.Empty ? naming : directoryInfo.Name;
            CheckBoxHelper directoryNode = new CheckBoxHelper(nodeName, false, false, false, false);
            directoryNode.Name = nodeName;
            directoryNode.Text = nodeName;

            // var directoryNode = new TreeNode(directoryInfo.Name);

            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Nodes.Add(createDirectoryNode(directory, String.Empty));
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                // directoryNode.Nodes.Add(new TreeNode(file.Name));
                CheckBoxHelper helper = new CheckBoxHelper(file.Name, false, false, false, false);
                helper.Name = file.Name;
                helper.Text = file.Name;
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

            FileNameStorage.instance.saveMappingToCloud();

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
            createFolder();
        }

        private void createFolder()
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

        private string getDropboxCryptDriveContainerNamePath()
        {
            return "/Dropbox/CryptDriveSS2018/" + FileManager.instance.containerName;
        }

        private void btnDownload_Click()
        {
            try
            {
                if (DBB != null)
                {
                    if (strAccessToken != null && strAuthenticationURL != null)
                    {
                        DBB.Download(getDropboxCryptDriveContainerNamePath(), "Sample-test.jpg", @"D:\", "capture4_dwnld.png");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AuthenticateDropBox()
        {
            try
            {
                if (string.IsNullOrEmpty("q4ela0mxhrm7eti"))
                {
                    MessageBox.Show("Please enter valid App Key !");
                    return;
                }
                if (DBB == null)
                {
                    DBB = new DropBoxBase("q4ela0mxhrm7eti", "CryptDriveSS2018");

                    strAuthenticationURL = DBB.GeneratedAuthenticationURL(); // This method must be executed before generating Access Token.
                    strAccessToken = DBB.GenerateAccessToken();
                    dropboxsync_btn.Enabled = true;
                    dropbox_image.Visible = true;
                }
                else
                {
                    dropboxsync_btn.Enabled = false;
                    dropbox_image.Visible = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dropboxsync_btn_Click(object sender, EventArgs e)
        {
            dropBoxSync(getAllDropBoxSyncPaths());
        }

        public IEnumerable<string> getAllDropBoxSyncPaths()
        {
            foreach (CheckBoxHelper item in getAllChildren())
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
            foreach (CheckBoxHelper item in getAllChildren())
            {
                if (item.DropBoxDelete)
                {
                    yield return item.FullPath;
                }
            }
        }

        public IEnumerable<string> getAllCryptDriveDeletePaths()
        {
            foreach (CheckBoxHelper item in getAllChildren())
            {
                if (item.CryptDriveDelete)
                {
                    yield return item.FullPath;
                }
            }
        }

        public IEnumerable<string> getAllCryptDriveSyncPaths()
        {
            foreach (CheckBoxHelper item in getAllChildren())
            {
                if (item.CryptDriveSync)
                {
                    yield return item.FullPath;
                }
            }
        }

        IEnumerable<System.Windows.Forms.TreeNode> Collect(System.Windows.Forms.TreeNodeCollection nodes)
        {
            foreach (System.Windows.Forms.TreeNode node in nodes)
            {
                yield return node;

                foreach (var child in Collect(node.Nodes))
                    yield return child;
            }
        }

        public IEnumerable<CheckBoxHelper> getAllChildren()
        {
            foreach (CheckBoxHelper item in Collect(ucTreeView1.Nodes))
            {
                yield return item;
            }
        }

        private void dropBoxSync(IEnumerable<String> cryptpaths)
        {
            try
            {
                foreach (string cryptpath in cryptpaths)
                {
                    string realpath = FileManager.convertCryptPathToPath(cryptpath);
                    string hashpath = FileNameStorage.instance.hashPath(cryptpath);

                    if (DBB != null)
                    {
                        if (strAccessToken != null && strAuthenticationURL != null)
                        {
                            DBB.Upload(getDropboxCryptDriveContainerNamePath(), hashpath, realpath);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dropBoxDelete(IEnumerable<String> cryptpaths)
        {
            try
            {
                foreach (string cryptpath in cryptpaths)
                {
                    if (DBB != null)
                    {
                        if (strAccessToken != null && strAuthenticationURL != null)
                        {
                            string hashpath = FileNameStorage.instance.hashPath(cryptpath);
                            DBB.Delete(getDropboxCryptDriveContainerNamePath() + "/" + hashpath);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dropboxdelete_bt_Click(object sender, EventArgs e)
        {
            dropBoxDelete(getAllDropBoxDeletePaths());
        }
    }
}
