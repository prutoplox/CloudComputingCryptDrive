namespace Cryptdrive
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.Login_button = new System.Windows.Forms.Button();
            this.Register_button = new System.Windows.Forms.Button();
            this.Output_tf = new System.Windows.Forms.TextBox();
            this.ResetDB = new System.Windows.Forms.Button();
            this.skipLogin = new System.Windows.Forms.Button();
            this.initAll = new System.Windows.Forms.Button();
            this.Email_tf = new System.Windows.Forms.TextBox();
            this.ConfirmPassword_tf = new System.Windows.Forms.TextBox();
            this.ConfirmEmail_tf = new System.Windows.Forms.TextBox();
            this.Username_tf = new System.Windows.Forms.TextBox();
            this.Username_label = new System.Windows.Forms.Label();
            this.Password_label = new System.Windows.Forms.Label();
            this.Password_tf = new System.Windows.Forms.TextBox();
            this.Email_label = new System.Windows.Forms.Label();
            this.ConfirmPassword_label = new System.Windows.Forms.Label();
            this.ConfirmEmail_label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Login_button
            // 
            resources.ApplyResources(this.Login_button, "Login_button");
            this.Login_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.Login_button.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.Login_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Login_button.Name = "Login_button";
            this.Login_button.UseVisualStyleBackColor = false;
            this.Login_button.Click += new System.EventHandler(this.Login_button_Click);
            // 
            // Register_button
            // 
            resources.ApplyResources(this.Register_button, "Register_button");
            this.Register_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.Register_button.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.Register_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Register_button.Name = "Register_button";
            this.Register_button.UseVisualStyleBackColor = false;
            this.Register_button.Click += new System.EventHandler(this.Register_button_Click);
            // 
            // Output_tf
            // 
            resources.ApplyResources(this.Output_tf, "Output_tf");
            this.Output_tf.Name = "Output_tf";
            this.Output_tf.TextChanged += new System.EventHandler(this.Output_tf_TextChanged);
            // 
            // ResetDB
            // 
            resources.ApplyResources(this.ResetDB, "ResetDB");
            this.ResetDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.ResetDB.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.ResetDB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ResetDB.Name = "ResetDB";
            this.ResetDB.UseVisualStyleBackColor = false;
            this.ResetDB.Click += new System.EventHandler(this.ResetDB_Click);
            // 
            // skipLogin
            // 
            resources.ApplyResources(this.skipLogin, "skipLogin");
            this.skipLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.skipLogin.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.skipLogin.Cursor = System.Windows.Forms.Cursors.Default;
            this.skipLogin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.skipLogin.Name = "skipLogin";
            this.skipLogin.UseVisualStyleBackColor = false;
            this.skipLogin.Click += new System.EventHandler(this.skipLogin_Click);
            // 
            // initAll
            // 
            resources.ApplyResources(this.initAll, "initAll");
            this.initAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(228)))), ((int)(((byte)(249)))));
            this.initAll.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.initAll.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.initAll.Name = "initAll";
            this.initAll.UseVisualStyleBackColor = false;
            this.initAll.Click += new System.EventHandler(this.initAll_Click);
            // 
            // Email_tf
            // 
            resources.ApplyResources(this.Email_tf, "Email_tf");
            this.Email_tf.Name = "Email_tf";
            this.Email_tf.TextChanged += new System.EventHandler(this.Email_tf_TextChanged);
            // 
            // ConfirmPassword_tf
            // 
            resources.ApplyResources(this.ConfirmPassword_tf, "ConfirmPassword_tf");
            this.ConfirmPassword_tf.Name = "ConfirmPassword_tf";
            this.ConfirmPassword_tf.TextChanged += new System.EventHandler(this.ConfirmPassword_tf_TextChanged);
            // 
            // ConfirmEmail_tf
            // 
            resources.ApplyResources(this.ConfirmEmail_tf, "ConfirmEmail_tf");
            this.ConfirmEmail_tf.Name = "ConfirmEmail_tf";
            this.ConfirmEmail_tf.TextChanged += new System.EventHandler(this.ConfirmEmail_tf_TextChanged);
            // 
            // Username_tf
            // 
            resources.ApplyResources(this.Username_tf, "Username_tf");
            this.Username_tf.Name = "Username_tf";
            this.Username_tf.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // Username_label
            // 
            resources.ApplyResources(this.Username_label, "Username_label");
            this.Username_label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Username_label.Name = "Username_label";
            this.Username_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // Password_label
            // 
            resources.ApplyResources(this.Password_label, "Password_label");
            this.Password_label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Password_label.Name = "Password_label";
            this.Password_label.Click += new System.EventHandler(this.Password_label_Click);
            // 
            // Password_tf
            // 
            resources.ApplyResources(this.Password_tf, "Password_tf");
            this.Password_tf.Name = "Password_tf";
            this.Password_tf.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Email_label
            // 
            resources.ApplyResources(this.Email_label, "Email_label");
            this.Email_label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Email_label.Name = "Email_label";
            this.Email_label.Click += new System.EventHandler(this.Email_label_Click);
            // 
            // ConfirmPassword_label
            // 
            resources.ApplyResources(this.ConfirmPassword_label, "ConfirmPassword_label");
            this.ConfirmPassword_label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ConfirmPassword_label.Name = "ConfirmPassword_label";
            this.ConfirmPassword_label.Click += new System.EventHandler(this.ConfirmPassword_label_Click);
            // 
            // ConfirmEmail_label
            // 
            resources.ApplyResources(this.ConfirmEmail_label, "ConfirmEmail_label");
            this.ConfirmEmail_label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ConfirmEmail_label.Name = "ConfirmEmail_label";
            this.ConfirmEmail_label.Click += new System.EventHandler(this.ConfirmEmail_label_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Cryptdrive.Properties.Resources.logowithname;
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.panel2.Controls.Add(this.Login_button);
            this.panel2.Controls.Add(this.Register_button);
            this.panel2.Name = "panel2";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.panel3.Name = "panel3";
            // 
            // LoginForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumTurquoise;
            this.BackgroundImage = global::Cryptdrive.Properties.Resources.cloud2;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.skipLogin);
            this.Controls.Add(this.Output_tf);
            this.Controls.Add(this.ConfirmEmail_label);
            this.Controls.Add(this.initAll);
            this.Controls.Add(this.ResetDB);
            this.Controls.Add(this.ConfirmPassword_label);
            this.Controls.Add(this.Email_label);
            this.Controls.Add(this.Password_tf);
            this.Controls.Add(this.Username_tf);
            this.Controls.Add(this.Password_label);
            this.Controls.Add(this.Email_tf);
            this.Controls.Add(this.Username_label);
            this.Controls.Add(this.ConfirmPassword_tf);
            this.Controls.Add(this.ConfirmEmail_tf);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoginForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Login_button;
        private System.Windows.Forms.Button Register_button;
        private System.Windows.Forms.TextBox Output_tf;
        private System.Windows.Forms.Button ResetDB;
        private System.Windows.Forms.Button skipLogin;
        private System.Windows.Forms.Button initAll;
        private System.Windows.Forms.TextBox Email_tf;
        private System.Windows.Forms.TextBox ConfirmPassword_tf;
        private System.Windows.Forms.TextBox ConfirmEmail_tf;
        private System.Windows.Forms.TextBox Username_tf;
        private System.Windows.Forms.Label Username_label;
        private System.Windows.Forms.Label Password_label;
        private System.Windows.Forms.TextBox Password_tf;
        private System.Windows.Forms.Label Email_label;
        private System.Windows.Forms.Label ConfirmPassword_label;
        private System.Windows.Forms.Label ConfirmEmail_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}