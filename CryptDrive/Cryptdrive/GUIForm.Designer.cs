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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.delete = new System.Windows.Forms.Button();
            this.searchFile = new System.Windows.Forms.Button();
            this.searchFilePath = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.TextBox();
            this.pathTextField = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.Logout_bt = new System.Windows.Forms.Button();
            this.Settings_btn = new System.Windows.Forms.Button();
            this.Debug_bt = new System.Windows.Forms.Button();
            this.FileWatcherSupportTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(41, 292);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(75, 23);
            this.delete.TabIndex = 0;
            this.delete.Text = "Delete?";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // searchFile
            // 
            this.searchFile.Location = new System.Drawing.Point(287, 12);
            this.searchFile.Name = "searchFile";
            this.searchFile.Size = new System.Drawing.Size(75, 23);
            this.searchFile.TabIndex = 1;
            this.searchFile.Text = "SearchFile";
            this.searchFile.UseVisualStyleBackColor = true;
            this.searchFile.Click += new System.EventHandler(this.searchFile_Click);
            // 
            // searchFilePath
            // 
            this.searchFilePath.Enabled = false;
            this.searchFilePath.Location = new System.Drawing.Point(287, 292);
            this.searchFilePath.Name = "searchFilePath";
            this.searchFilePath.Size = new System.Drawing.Size(75, 23);
            this.searchFilePath.TabIndex = 2;
            this.searchFilePath.Text = "searchFilePath";
            this.searchFilePath.UseVisualStyleBackColor = true;
            this.searchFilePath.Click += new System.EventHandler(this.searchFilePath_Click);
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.SystemColors.HighlightText;
            this.output.Location = new System.Drawing.Point(41, 337);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output.Size = new System.Drawing.Size(310, 126);
            this.output.TabIndex = 3;
            this.output.WordWrap = false;
            // 
            // pathTextField
            // 
            this.pathTextField.Location = new System.Drawing.Point(122, 292);
            this.pathTextField.Name = "pathTextField";
            this.pathTextField.Size = new System.Drawing.Size(159, 20);
            this.pathTextField.TabIndex = 4;
            this.pathTextField.TextChanged += new System.EventHandler(this.pathTextField_TextChanged);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(41, 55);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(321, 231);
            this.treeView1.TabIndex = 5;
            // 
            // Logout_bt
            // 
            this.Logout_bt.Location = new System.Drawing.Point(817, 11);
            this.Logout_bt.Name = "Logout_bt";
            this.Logout_bt.Size = new System.Drawing.Size(75, 23);
            this.Logout_bt.TabIndex = 6;
            this.Logout_bt.Text = "Logout";
            this.Logout_bt.UseVisualStyleBackColor = true;
            this.Logout_bt.Click += new System.EventHandler(this.button1_Click);
            // 
            // Settings_btn
            // 
            this.Settings_btn.Location = new System.Drawing.Point(736, 11);
            this.Settings_btn.Name = "Settings_btn";
            this.Settings_btn.Size = new System.Drawing.Size(75, 23);
            this.Settings_btn.TabIndex = 7;
            this.Settings_btn.Text = "Settings";
            this.Settings_btn.UseVisualStyleBackColor = true;
            // 
            // Debug_bt
            // 
            this.Debug_bt.BackColor = System.Drawing.Color.Maroon;
            this.Debug_bt.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.55F);
            this.Debug_bt.ForeColor = System.Drawing.Color.LawnGreen;
            this.Debug_bt.Location = new System.Drawing.Point(688, 323);
            this.Debug_bt.Name = "Debug_bt";
            this.Debug_bt.Size = new System.Drawing.Size(220, 140);
            this.Debug_bt.TabIndex = 8;
            this.Debug_bt.Text = "DEBUG !";
            this.Debug_bt.UseVisualStyleBackColor = false;
            this.Debug_bt.Click += new System.EventHandler(this.Debug_bt_Click);
            // 
            // FileWatcherSupportTimer
            // 
            this.FileWatcherSupportTimer.Interval = 1000;
            this.FileWatcherSupportTimer.Tick += new System.EventHandler(this.FileWatcherSupportTimer_Tick);
            // 
            // GUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 475);
            this.Controls.Add(this.Debug_bt);
            this.Controls.Add(this.Settings_btn);
            this.Controls.Add(this.Logout_bt);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.pathTextField);
            this.Controls.Add(this.output);
            this.Controls.Add(this.searchFilePath);
            this.Controls.Add(this.searchFile);
            this.Controls.Add(this.delete);
            this.Name = "GUIForm";
            this.Text = "7";
            this.Activated += new System.EventHandler(this.GUIForm_Activated);
            this.Load += new System.EventHandler(this.GUIForm_Load);
            this.VisibleChanged += new System.EventHandler(this.GUIForm_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button searchFile;
        private System.Windows.Forms.Button searchFilePath;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.TextBox pathTextField;
        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button Logout_bt;
        private System.Windows.Forms.Button Settings_btn;
        private System.Windows.Forms.Button Debug_bt;
        internal System.Windows.Forms.Timer FileWatcherSupportTimer;
    }
}

