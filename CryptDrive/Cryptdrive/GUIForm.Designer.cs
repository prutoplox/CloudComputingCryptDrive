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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.delete = new System.Windows.Forms.Button();
            this.searchFile = new System.Windows.Forms.Button();
            this.searchFilePath = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.TextBox();
            this.pathTextField = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
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
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(41, 55);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(321, 231);
            this.treeView1.TabIndex = 5;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 475);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.pathTextField);
            this.Controls.Add(this.output);
            this.Controls.Add(this.searchFilePath);
            this.Controls.Add(this.searchFile);
            this.Controls.Add(this.delete);
            this.Name = "GUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GUI_Load);
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
    }
}

