using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Cryptdrive
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                throw new Exception("TODO: GUI Error");
            }
        }

        public static GUI instance;

        private void searchFilePath_Click(object sender, EventArgs e)
        {
            string path = "";
            DialogResult pathObject = folderBrowserDialog.ShowDialog();
            if (pathObject == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
                List<string> paths = new List<string>();
                paths.Add(path);
                FileWatcher.instance.monitorDirectory(path);
                pathTextField.Text += path;
                FileWatcher.instance.syncClientTreeNode();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            Logger.instance.logInfo("Krass auf delete gedrückt, jo!!");
            List<String> testTemp = new List<string>();
            testTemp.Add(@"C:\Users\mariu\OneDrive\Desktop\Neuer Ordner (2)\awda.txt");

            testTemp.Add(@"F:\CloudComp\CryptDrive\Cryptdrive\Testfiles\small.txt");
            testTemp.Add(@"F:\CloudComp\CryptDrive\Cryptdrive\Testfiles\large.txt");
            FileManager.instance.syncFiles(testTemp);
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
            output.Text += textLine + Environment.NewLine;
        }
    }
}
