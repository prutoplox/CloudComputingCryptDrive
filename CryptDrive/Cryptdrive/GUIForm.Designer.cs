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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIForm));
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
            this.dropbox_btn = new System.Windows.Forms.Button();
            this.sync_bt = new System.Windows.Forms.Button();
            this.dropboxsync_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.db_del = new System.Windows.Forms.CheckBox();
            this.db_sync = new System.Windows.Forms.CheckBox();
            this.cd_del = new System.Windows.Forms.CheckBox();
            this.cd_sync = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dropboxdelete_bt = new System.Windows.Forms.Button();
            this.dropbox_image = new System.Windows.Forms.PictureBox();
            this.control_panel = new System.Windows.Forms.Panel();
            this.legend_panel = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.help_panel = new System.Windows.Forms.Panel();
            this.helpText = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.FileWatcherSupportTimer = new System.Windows.Forms.Timer(this.components);
            this.ucTreeView1 = new UcTreeView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dropbox_image)).BeginInit();
            this.control_panel.SuspendLayout();
            this.legend_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.help_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(6)))));
            this.delete.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete.Location = new System.Drawing.Point(313, 383);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(75, 23);
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
            this.help_bt.Click += new System.EventHandler(this.helpBtn_Click);
            // 
            // searchFilePath
            // 
            this.searchFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.searchFilePath.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.searchFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.searchFilePath.Enabled = false;
            this.searchFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchFilePath.Location = new System.Drawing.Point(219, 412);
            this.searchFilePath.Name = "searchFilePath";
            this.searchFilePath.Size = new System.Drawing.Size(169, 23);
            this.searchFilePath.TabIndex = 2;
            this.searchFilePath.Text = "Browe monitored folder...";
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
            this.pathTextField.Location = new System.Drawing.Point(50, 425);
            this.pathTextField.Name = "pathTextField";
            this.pathTextField.Size = new System.Drawing.Size(190, 20);
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
            this.Logout_bt.Click += new System.EventHandler(this.Logout_bt_Click);
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
            this.dropbox_btn.Click += new System.EventHandler(this.dropbox_btn_Click);
            // 
            // sync_bt
            // 
            this.sync_bt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(64)))));
            this.sync_bt.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.sync_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sync_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sync_bt.Location = new System.Drawing.Point(313, 356);
            this.sync_bt.Name = "sync_bt";
            this.sync_bt.Size = new System.Drawing.Size(75, 22);
            this.sync_bt.TabIndex = 10;
            this.sync_bt.Text = "Sync";
            this.sync_bt.UseVisualStyleBackColor = false;
            this.sync_bt.Click += new System.EventHandler(this.sync_bt_Click);
            // 
            // dropboxsync_btn
            // 
            this.dropboxsync_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.dropboxsync_btn.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.dropboxsync_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dropboxsync_btn.Enabled = false;
            this.dropboxsync_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dropboxsync_btn.Location = new System.Drawing.Point(98, 357);
            this.dropboxsync_btn.Name = "dropboxsync_btn";
            this.dropboxsync_btn.Size = new System.Drawing.Size(75, 21);
            this.dropboxsync_btn.TabIndex = 11;
            this.dropboxsync_btn.Text = "- Sync";
            this.dropboxsync_btn.UseVisualStyleBackColor = false;
            this.dropboxsync_btn.Click += new System.EventHandler(this.dropboxsync_btn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.db_del);
            this.panel1.Controls.Add(this.db_sync);
            this.panel1.Controls.Add(this.cd_del);
            this.panel1.Controls.Add(this.cd_sync);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dropboxdelete_bt);
            this.panel1.Controls.Add(this.sync_bt);
            this.panel1.Controls.Add(this.searchFilePath);
            this.panel1.Controls.Add(this.dropbox_image);
            this.panel1.Controls.Add(this.dropboxsync_btn);
            this.panel1.Controls.Add(this.delete);
            this.panel1.Location = new System.Drawing.Point(27, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 452);
            this.panel1.TabIndex = 12;
            // 
            // db_del
            // 
            this.db_del.AutoSize = true;
            this.db_del.Location = new System.Drawing.Point(77, 388);
            this.db_del.Name = "db_del";
            this.db_del.Size = new System.Drawing.Size(15, 14);
            this.db_del.TabIndex = 17;
            this.db_del.UseVisualStyleBackColor = true;
            this.db_del.CheckedChanged += new System.EventHandler(this.db_del_CheckedChanged);
            // 
            // db_sync
            // 
            this.db_sync.AutoSize = true;
            this.db_sync.Location = new System.Drawing.Point(77, 361);
            this.db_sync.Name = "db_sync";
            this.db_sync.Size = new System.Drawing.Size(15, 14);
            this.db_sync.TabIndex = 16;
            this.db_sync.UseVisualStyleBackColor = true;
            this.db_sync.CheckedChanged += new System.EventHandler(this.db_sync_CheckedChanged);
            // 
            // cd_del
            // 
            this.cd_del.AutoSize = true;
            this.cd_del.Location = new System.Drawing.Point(292, 388);
            this.cd_del.Name = "cd_del";
            this.cd_del.Size = new System.Drawing.Size(15, 14);
            this.cd_del.TabIndex = 15;
            this.cd_del.UseVisualStyleBackColor = true;
            this.cd_del.CheckedChanged += new System.EventHandler(this.cd_del_CheckedChanged);
            // 
            // cd_sync
            // 
            this.cd_sync.AutoSize = true;
            this.cd_sync.Location = new System.Drawing.Point(292, 361);
            this.cd_sync.Name = "cd_sync";
            this.cd_sync.Size = new System.Drawing.Size(15, 14);
            this.cd_sync.TabIndex = 14;
            this.cd_sync.UseVisualStyleBackColor = true;
            this.cd_sync.CheckedChanged += new System.EventHandler(this.cd_sync_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(216, 374);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Crypt Drive";
            // 
            // dropboxdelete_bt
            // 
            this.dropboxdelete_bt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(4)))), ((int)(((byte)(255)))));
            this.dropboxdelete_bt.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.dropboxdelete_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dropboxdelete_bt.Enabled = false;
            this.dropboxdelete_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dropboxdelete_bt.Location = new System.Drawing.Point(98, 383);
            this.dropboxdelete_bt.Name = "dropboxdelete_bt";
            this.dropboxdelete_bt.Size = new System.Drawing.Size(75, 23);
            this.dropboxdelete_bt.TabIndex = 12;
            this.dropboxdelete_bt.Text = "- Delete";
            this.dropboxdelete_bt.UseVisualStyleBackColor = false;
            this.dropboxdelete_bt.Click += new System.EventHandler(this.dropboxdelete_bt_Click);
            // 
            // dropbox_image
            // 
            this.dropbox_image.BackColor = System.Drawing.Color.Transparent;
            this.dropbox_image.BackgroundImage = global::Cryptdrive.Properties.Resources.dropbox_logo;
            this.dropbox_image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dropbox_image.Enabled = false;
            this.dropbox_image.Location = new System.Drawing.Point(23, 357);
            this.dropbox_image.Name = "dropbox_image";
            this.dropbox_image.Size = new System.Drawing.Size(48, 51);
            this.dropbox_image.TabIndex = 0;
            this.dropbox_image.TabStop = false;
            // 
            // control_panel
            // 
            this.control_panel.BackColor = System.Drawing.Color.Transparent;
            this.control_panel.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.control_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.control_panel.Controls.Add(this.help_panel);
            this.control_panel.Controls.Add(this.help_bt);
            this.control_panel.Controls.Add(this.dropbox_btn);
            this.control_panel.Controls.Add(this.Logout_bt);
            this.control_panel.Controls.Add(this.legend_panel);
            this.control_panel.Location = new System.Drawing.Point(468, 11);
            this.control_panel.Name = "control_panel";
            this.control_panel.Size = new System.Drawing.Size(424, 253);
            this.control_panel.TabIndex = 13;
            // 
            // legend_panel
            // 
            this.legend_panel.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.legend_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.legend_panel.Controls.Add(this.label7);
            this.legend_panel.Controls.Add(this.label6);
            this.legend_panel.Controls.Add(this.label5);
            this.legend_panel.Controls.Add(this.label4);
            this.legend_panel.Controls.Add(this.label3);
            this.legend_panel.Controls.Add(this.label2);
            this.legend_panel.Controls.Add(this.pictureBox6);
            this.legend_panel.Controls.Add(this.pictureBox5);
            this.legend_panel.Controls.Add(this.pictureBox4);
            this.legend_panel.Controls.Add(this.pictureBox3);
            this.legend_panel.Controls.Add(this.pictureBox2);
            this.legend_panel.Controls.Add(this.pictureBox1);
            this.legend_panel.Location = new System.Drawing.Point(28, 37);
            this.legend_panel.Name = "legend_panel";
            this.legend_panel.Size = new System.Drawing.Size(385, 202);
            this.legend_panel.TabIndex = 11;
            this.legend_panel.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(208, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "File on the client is newer then in the cloud";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(208, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "File in the cloud is newer then on the client";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "File in Cloud and on Client synced";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "File in the cloud but not yet downloaded";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "File on client not yet synced to CryptDrive";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Folder or (yet) untracked";
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Blue;
            this.pictureBox6.Location = new System.Drawing.Point(13, 119);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(15, 15);
            this.pictureBox6.TabIndex = 5;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Orange;
            this.pictureBox5.Location = new System.Drawing.Point(13, 98);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(15, 15);
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Green;
            this.pictureBox4.Location = new System.Drawing.Point(13, 77);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(15, 15);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Yellow;
            this.pictureBox3.Location = new System.Drawing.Point(13, 56);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(15, 15);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Red;
            this.pictureBox2.Location = new System.Drawing.Point(13, 35);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(15, 15);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(13, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(15, 15);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // help_panel
            // 
            this.help_panel.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.help_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.help_panel.Controls.Add(this.helpText);
            this.help_panel.Location = new System.Drawing.Point(28, 37);
            this.help_panel.Name = "help_panel";
            this.help_panel.Size = new System.Drawing.Size(385, 202);
            this.help_panel.TabIndex = 10;
            // 
            // helpText
            // 
            this.helpText.Location = new System.Drawing.Point(13, 13);
            this.helpText.Multiline = true;
            this.helpText.Name = "helpText";
            this.helpText.Size = new System.Drawing.Size(359, 175);
            this.helpText.TabIndex = 0;
            this.helpText.Text = resources.GetString("helpText.Text");
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
            // FileWatcherSupportTimer
            // 
            this.FileWatcherSupportTimer.Interval = 1000;
            this.FileWatcherSupportTimer.Tick += new System.EventHandler(this.FileWatcherSupportTimer_Tick);
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
            this.ucTreeView1.PathSeparator = "/";
            this.ucTreeView1.Size = new System.Drawing.Size(365, 336);
            this.ucTreeView1.Spacing = 4;
            this.ucTreeView1.TabIndex = 9;
            // 
            // GUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Cryptdrive.Properties.Resources.cloud2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(920, 475);
            this.Controls.Add(this.pathTextField);
            this.Controls.Add(this.output);
            this.Controls.Add(this.control_panel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.ucTreeView1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GUIForm";
            this.Text = "Crypt Drive";
            this.VisibleChanged += new System.EventHandler(this.GUIForm_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dropbox_image)).EndInit();
            this.control_panel.ResumeLayout(false);
            this.legend_panel.ResumeLayout(false);
            this.legend_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.help_panel.ResumeLayout(false);
            this.help_panel.PerformLayout();
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
        private System.Windows.Forms.Button dropbox_btn;
        private UcTreeView ucTreeView1;
        private System.Windows.Forms.Button sync_bt;
        private System.Windows.Forms.Button dropboxsync_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel control_panel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox dropbox_image;
        private System.Windows.Forms.Panel help_panel;
        private System.Windows.Forms.Button dropboxdelete_bt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox db_del;
        private System.Windows.Forms.CheckBox db_sync;
        private System.Windows.Forms.CheckBox cd_del;
        private System.Windows.Forms.CheckBox cd_sync;
        private System.Windows.Forms.Timer FileWatcherSupportTimer;
        private System.Windows.Forms.TextBox helpText;
        private System.Windows.Forms.Panel legend_panel;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

