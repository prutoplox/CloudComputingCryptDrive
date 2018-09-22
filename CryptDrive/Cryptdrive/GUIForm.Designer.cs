namespace Cryptdrive
{
    partial class GUIForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Knoten4");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Knoten5");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Knoten1", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Knoten6");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Knoten7");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Knoten8");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Knoten0", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Knoten2");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Knoten3");
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.delete = new System.Windows.Forms.Button();
            this.help_bt = new System.Windows.Forms.Button();
            this.searchFilePath = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.TextBox();
            this.pathTextField = new System.Windows.Forms.TextBox();
            this.Logout_bt = new System.Windows.Forms.Button();
            this.Settings_btn = new System.Windows.Forms.Button();
            this.Debug_bt = new System.Windows.Forms.Button();
            this.FileWatcherSupportTimer = new System.Windows.Forms.Timer(this.components);
            this.dropbox_btn = new System.Windows.Forms.Button();
            this.sync_bt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.control_panel = new System.Windows.Forms.Panel();
            this.help_panel = new System.Windows.Forms.Panel();
            this.dropbox_panel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucTreeView1 = new UcTreeView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.control_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(6)))));
            this.delete.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete.Location = new System.Drawing.Point(232, 363);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(75, 45);
            this.delete.TabIndex = 0;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // help_bt
            // 
            this.help_bt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.help_bt.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.help_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.help_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.help_bt.Location = new System.Drawing.Point(28, 8);
            this.help_bt.Name = "help_bt";
            this.help_bt.Size = new System.Drawing.Size(75, 23);
            this.help_bt.TabIndex = 1;
            this.help_bt.Text = "Help";
            this.help_bt.UseVisualStyleBackColor = false;
            this.help_bt.Click += new System.EventHandler(this.searchFile_Click);
            // 
            // searchFilePath
            // 
            this.searchFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.searchFilePath.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.searchFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.searchFilePath.Enabled = false;
            this.searchFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchFilePath.Location = new System.Drawing.Point(351, 425);
            this.searchFilePath.Name = "searchFilePath";
            this.searchFilePath.Size = new System.Drawing.Size(75, 23);
            this.searchFilePath.TabIndex = 2;
            this.searchFilePath.Text = "searchFilePath";
            this.searchFilePath.UseVisualStyleBackColor = false;
            this.searchFilePath.Click += new System.EventHandler(this.searchFilePath_Click);
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.SystemColors.HighlightText;
            this.output.Location = new System.Drawing.Point(485, 296);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output.Size = new System.Drawing.Size(396, 152);
            this.output.TabIndex = 3;
            this.output.WordWrap = false;
            // 
            // pathTextField
            // 
            this.pathTextField.Location = new System.Drawing.Point(41, 425);
            this.pathTextField.Name = "pathTextField";
            this.pathTextField.Size = new System.Drawing.Size(304, 20);
            this.pathTextField.TabIndex = 4;
            this.pathTextField.TextChanged += new System.EventHandler(this.pathTextField_TextChanged);
            // 
            // Logout_bt
            // 
            this.Logout_bt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.Logout_bt.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.Logout_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Logout_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Logout_bt.Location = new System.Drawing.Point(338, 8);
            this.Logout_bt.Name = "Logout_bt";
            this.Logout_bt.Size = new System.Drawing.Size(75, 23);
            this.Logout_bt.TabIndex = 6;
            this.Logout_bt.Text = "Logout";
            this.Logout_bt.UseVisualStyleBackColor = false;
            this.Logout_bt.Click += new System.EventHandler(this.button1_Click);
            // 
            // dropbox_btn
            // 
            this.dropbox_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.dropbox_btn.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.dropbox_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dropbox_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dropbox_btn.Location = new System.Drawing.Point(109, 8);
            this.dropbox_btn.Name = "dropbox_btn";
            this.dropbox_btn.Size = new System.Drawing.Size(75, 23);
            this.dropbox_btn.TabIndex = 7;
            this.dropbox_btn.Text = "DropBox";
            this.dropbox_btn.UseVisualStyleBackColor = false;
            // 
            // sync_bt
            // 
            this.sync_bt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(64)))));
            this.sync_bt.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.sync_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sync_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sync_bt.Location = new System.Drawing.Point(313, 363);
            this.sync_bt.Name = "sync_bt";
            this.sync_bt.Size = new System.Drawing.Size(75, 45);
            this.sync_bt.TabIndex = 10;
            this.sync_bt.Text = "Sync";
            this.sync_bt.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.button1.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(77, 363);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 45);
            this.button1.TabIndex = 11;
            this.button1.Text = "- Sync";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.sync_bt);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.delete);
            this.panel1.Location = new System.Drawing.Point(27, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 452);
            this.panel1.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Cryptdrive.Properties.Resources.dropbox_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(23, 363);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 45);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // control_panel
            // 
            this.control_panel.BackColor = System.Drawing.Color.Transparent;
            this.control_panel.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.control_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.control_panel.Controls.Add(this.help_panel);
            this.control_panel.Controls.Add(this.dropbox_panel);
            this.control_panel.Controls.Add(this.help_bt);
            this.control_panel.Controls.Add(this.dropbox_btn);
            this.control_panel.Controls.Add(this.Logout_bt);
            this.control_panel.Location = new System.Drawing.Point(468, 11);
            this.control_panel.Name = "control_panel";
            this.control_panel.Size = new System.Drawing.Size(424, 253);
            this.control_panel.TabIndex = 13;
            // 
            // help_panel
            // 
            this.help_panel.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.help_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.help_panel.Location = new System.Drawing.Point(28, 38);
            this.help_panel.Name = "help_panel";
            this.help_panel.Size = new System.Drawing.Size(385, 201);
            this.help_panel.TabIndex = 10;
            // 
            // dropbox_panel
            // 
            this.dropbox_panel.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.dropbox_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dropbox_panel.Location = new System.Drawing.Point(28, 48);
            this.dropbox_panel.Name = "dropbox_panel";
            this.dropbox_panel.Size = new System.Drawing.Size(385, 191);
            this.dropbox_panel.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(468, 282);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(424, 181);
            this.panel3.TabIndex = 14;
            // 
            // ucTreeView1
            // 
            this.ucTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.ucTreeView1.HideSelection = false;
            this.ucTreeView1.LeftPadding = 7;
            this.ucTreeView1.Location = new System.Drawing.Point(50, 26);
            this.ucTreeView1.Name = "ucTreeView1";
            treeNode1.Name = "Knoten4";
            treeNode1.Text = "Knoten4";
            treeNode2.Name = "Knoten5";
            treeNode2.Text = "Knoten5";
            treeNode3.Name = "Knoten1";
            treeNode3.Text = "Knoten1";
            treeNode4.Name = "Knoten6";
            treeNode4.Text = "Knoten6";
            treeNode5.Name = "Knoten7";
            treeNode5.Text = "Knoten7";
            treeNode6.Name = "Knoten8";
            treeNode6.Text = "Knoten8";
            treeNode7.Name = "Knoten0";
            treeNode7.Text = "Knoten0";
            treeNode8.Name = "Knoten2";
            treeNode8.Text = "Knoten2";
            treeNode9.Name = "Knoten3";
            treeNode9.Text = "Knoten3";
            this.ucTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            this.ucTreeView1.Size = new System.Drawing.Size(365, 336);
            this.ucTreeView1.Spacing = 4;
            this.ucTreeView1.TabIndex = 9;
            // 
            // FileWatcherSupportTimer
            // 
            this.FileWatcherSupportTimer.Enabled = true;
            this.FileWatcherSupportTimer.Interval = 5000;
            this.FileWatcherSupportTimer.Tick += new System.EventHandler(this.FileWatcherSupportTimer_Tick);
            // 
            // GUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Cryptdrive.Properties.Resources.cloud2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(920, 475);
            this.Controls.Add(this.ucTreeView1);
            this.Controls.Add(this.pathTextField);
            this.Controls.Add(this.output);
            this.Controls.Add(this.searchFilePath);
            this.Controls.Add(this.control_panel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GUIForm";
            this.Text = "Crypt Drive";
            this.Activated += new System.EventHandler(this.GUIForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GUIForm_FormClosed);
            this.Load += new System.EventHandler(this.GUIForm_Load);
            this.VisibleChanged += new System.EventHandler(this.GUIForm_VisibleChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.control_panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button help_bt;
        private System.Windows.Forms.Button searchFilePath;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.TextBox pathTextField;
        private System.Windows.Forms.Button Logout_bt;
        private System.Windows.Forms.Button Settings_btn;
        private System.Windows.Forms.Button Debug_bt;
        internal System.Windows.Forms.Timer FileWatcherSupportTimer;
        private System.Windows.Forms.Button dropbox_btn;
        private UcTreeView ucTreeView1;
        private System.Windows.Forms.Button sync_bt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel control_panel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel dropbox_panel;
        private System.Windows.Forms.Panel help_panel;
    }
}

