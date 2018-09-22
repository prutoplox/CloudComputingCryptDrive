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
            this.SuspendLayout();
            // 
            // Login_button
            // 
            this.Login_button.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.Login_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Login_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Login_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Login_button.Location = new System.Drawing.Point(618, 167);
            this.Login_button.Name = "Login_button";
            this.Login_button.Size = new System.Drawing.Size(124, 31);
            this.Login_button.TabIndex = 0;
            this.Login_button.Text = "Login";
            this.Login_button.UseVisualStyleBackColor = true;
            this.Login_button.Click += new System.EventHandler(this.Login_button_Click);
            // 
            // Register_button
            // 
            this.Register_button.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.Register_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Register_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Register_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Register_button.Location = new System.Drawing.Point(618, 206);
            this.Register_button.Name = "Register_button";
            this.Register_button.Size = new System.Drawing.Size(124, 34);
            this.Register_button.TabIndex = 1;
            this.Register_button.Text = "Register";
            this.Register_button.UseVisualStyleBackColor = true;
            this.Register_button.Click += new System.EventHandler(this.Register_button_Click);
            // 
            // Output_tf
            // 
            this.Output_tf.Location = new System.Drawing.Point(27, 265);
            this.Output_tf.Name = "Output_tf";
            this.Output_tf.Size = new System.Drawing.Size(508, 20);
            this.Output_tf.TabIndex = 12;
            this.Output_tf.TextChanged += new System.EventHandler(this.Output_tf_TextChanged);
            // 
            // ResetDB
            // 
            this.ResetDB.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.ResetDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ResetDB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ResetDB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ResetDB.Location = new System.Drawing.Point(618, 88);
            this.ResetDB.Name = "ResetDB";
            this.ResetDB.Size = new System.Drawing.Size(124, 34);
            this.ResetDB.TabIndex = 13;
            this.ResetDB.Text = "ResetDB";
            this.ResetDB.UseVisualStyleBackColor = true;
            this.ResetDB.Click += new System.EventHandler(this.ResetDB_Click);
            // 
            // skipLogin
            // 
            this.skipLogin.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.skipLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.skipLogin.Cursor = System.Windows.Forms.Cursors.Default;
            this.skipLogin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.skipLogin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.skipLogin.Location = new System.Drawing.Point(618, 128);
            this.skipLogin.Name = "skipLogin";
            this.skipLogin.Size = new System.Drawing.Size(124, 33);
            this.skipLogin.TabIndex = 13;
            this.skipLogin.Text = "Skip Login";
            this.skipLogin.UseVisualStyleBackColor = true;
            this.skipLogin.Click += new System.EventHandler(this.skipLogin_Click);
            // 
            // initAll
            // 
            this.initAll.BackgroundImage = global::Cryptdrive.Properties.Resources.button3;
            this.initAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.initAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.initAll.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.initAll.Location = new System.Drawing.Point(618, 51);
            this.initAll.Name = "initAll";
            this.initAll.Size = new System.Drawing.Size(124, 31);
            this.initAll.TabIndex = 14;
            this.initAll.Text = "Init all?";
            this.initAll.UseVisualStyleBackColor = true;
            this.initAll.Click += new System.EventHandler(this.initAll_Click);
            // 
            // Email_tf
            // 
            this.Email_tf.Location = new System.Drawing.Point(180, 214);
            this.Email_tf.Name = "Email_tf";
            this.Email_tf.Size = new System.Drawing.Size(212, 20);
            this.Email_tf.TabIndex = 5;
            this.Email_tf.TextChanged += new System.EventHandler(this.Email_tf_TextChanged);
            // 
            // ConfirmPassword_tf
            // 
            this.ConfirmPassword_tf.Location = new System.Drawing.Point(180, 188);
            this.ConfirmPassword_tf.Name = "ConfirmPassword_tf";
            this.ConfirmPassword_tf.Size = new System.Drawing.Size(212, 20);
            this.ConfirmPassword_tf.TabIndex = 4;
            this.ConfirmPassword_tf.TextChanged += new System.EventHandler(this.ConfirmPassword_tf_TextChanged);
            // 
            // ConfirmEmail_tf
            // 
            this.ConfirmEmail_tf.Location = new System.Drawing.Point(180, 240);
            this.ConfirmEmail_tf.Name = "ConfirmEmail_tf";
            this.ConfirmEmail_tf.Size = new System.Drawing.Size(212, 20);
            this.ConfirmEmail_tf.TabIndex = 6;
            this.ConfirmEmail_tf.TextChanged += new System.EventHandler(this.ConfirmEmail_tf_TextChanged);
            // 
            // Username_tf
            // 
            this.Username_tf.Location = new System.Drawing.Point(180, 136);
            this.Username_tf.Name = "Username_tf";
            this.Username_tf.Size = new System.Drawing.Size(212, 20);
            this.Username_tf.TabIndex = 2;
            this.Username_tf.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // Username_label
            // 
            this.Username_label.AutoSize = true;
            this.Username_label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Username_label.Location = new System.Drawing.Point(117, 136);
            this.Username_label.Name = "Username_label";
            this.Username_label.Size = new System.Drawing.Size(55, 13);
            this.Username_label.TabIndex = 7;
            this.Username_label.Text = "Username";
            this.Username_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // Password_label
            // 
            this.Password_label.AutoSize = true;
            this.Password_label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Password_label.Location = new System.Drawing.Point(121, 162);
            this.Password_label.Name = "Password_label";
            this.Password_label.Size = new System.Drawing.Size(53, 13);
            this.Password_label.TabIndex = 8;
            this.Password_label.Text = "Password";
            this.Password_label.Click += new System.EventHandler(this.Password_label_Click);
            // 
            // Password_tf
            // 
            this.Password_tf.Location = new System.Drawing.Point(180, 159);
            this.Password_tf.Name = "Password_tf";
            this.Password_tf.Size = new System.Drawing.Size(212, 20);
            this.Password_tf.TabIndex = 3;
            this.Password_tf.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Email_label
            // 
            this.Email_label.AutoSize = true;
            this.Email_label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Email_label.Location = new System.Drawing.Point(140, 214);
            this.Email_label.Name = "Email_label";
            this.Email_label.Size = new System.Drawing.Size(32, 13);
            this.Email_label.TabIndex = 9;
            this.Email_label.Text = "Email";
            this.Email_label.Click += new System.EventHandler(this.Email_label_Click);
            // 
            // ConfirmPassword_label
            // 
            this.ConfirmPassword_label.AutoSize = true;
            this.ConfirmPassword_label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ConfirmPassword_label.Location = new System.Drawing.Point(81, 188);
            this.ConfirmPassword_label.Name = "ConfirmPassword_label";
            this.ConfirmPassword_label.Size = new System.Drawing.Size(91, 13);
            this.ConfirmPassword_label.TabIndex = 10;
            this.ConfirmPassword_label.Text = "Confirm Password";
            this.ConfirmPassword_label.Click += new System.EventHandler(this.ConfirmPassword_label_Click);
            // 
            // ConfirmEmail_label
            // 
            this.ConfirmEmail_label.AutoSize = true;
            this.ConfirmEmail_label.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ConfirmEmail_label.Location = new System.Drawing.Point(102, 240);
            this.ConfirmEmail_label.Name = "ConfirmEmail_label";
            this.ConfirmEmail_label.Size = new System.Drawing.Size(70, 13);
            this.ConfirmEmail_label.TabIndex = 11;
            this.ConfirmEmail_label.Text = "Confirm Email";
            this.ConfirmEmail_label.Click += new System.EventHandler(this.ConfirmEmail_label_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Cryptdrive.Properties.Resources.logowithname;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(551, 100);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(12, 118);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(551, 184);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::Cryptdrive.Properties.Resources.borderagain;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(570, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(210, 289);
            this.panel3.TabIndex = 17;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumTurquoise;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(792, 314);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.skipLogin);
            this.Controls.Add(this.Output_tf);
            this.Controls.Add(this.Register_button);
            this.Controls.Add(this.ConfirmEmail_label);
            this.Controls.Add(this.Login_button);
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
            this.DoubleBuffered = true;
            this.Name = "LoginForm";
            this.Text = "CryptDrive Login / Register";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.Load += new System.EventHandler(this.LoginForm_Load);
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