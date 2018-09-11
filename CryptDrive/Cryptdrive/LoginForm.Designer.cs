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
            this.Login_button = new System.Windows.Forms.Button();
            this.Register_button = new System.Windows.Forms.Button();
            this.Username_tf = new System.Windows.Forms.TextBox();
            this.Password_tf = new System.Windows.Forms.TextBox();
            this.ConfirmPassword_tf = new System.Windows.Forms.TextBox();
            this.Email_tf = new System.Windows.Forms.TextBox();
            this.ConfirmEmail_tf = new System.Windows.Forms.TextBox();
            this.Username_label = new System.Windows.Forms.Label();
            this.Password_label = new System.Windows.Forms.Label();
            this.Email_label = new System.Windows.Forms.Label();
            this.ConfirmPassword_label = new System.Windows.Forms.Label();
            this.ConfirmEmail_label = new System.Windows.Forms.Label();
            this.Output_tf = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Login_button
            // 
            this.Login_button.Location = new System.Drawing.Point(15, 53);
            this.Login_button.Name = "Login_button";
            this.Login_button.Size = new System.Drawing.Size(75, 23);
            this.Login_button.TabIndex = 0;
            this.Login_button.Text = "Login";
            this.Login_button.UseVisualStyleBackColor = true;
            this.Login_button.Click += new System.EventHandler(this.Login_button_Click);
            // 
            // Register_button
            // 
            this.Register_button.Location = new System.Drawing.Point(15, 89);
            this.Register_button.Name = "Register_button";
            this.Register_button.Size = new System.Drawing.Size(75, 23);
            this.Register_button.TabIndex = 1;
            this.Register_button.Text = "Register";
            this.Register_button.UseVisualStyleBackColor = true;
            this.Register_button.Click += new System.EventHandler(this.Register_button_Click);
            // 
            // Username_tf
            // 
            this.Username_tf.Location = new System.Drawing.Point(193, 34);
            this.Username_tf.Name = "Username_tf";
            this.Username_tf.Size = new System.Drawing.Size(212, 20);
            this.Username_tf.TabIndex = 2;
            this.Username_tf.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // Password_tf
            // 
            this.Password_tf.Location = new System.Drawing.Point(193, 60);
            this.Password_tf.Name = "Password_tf";
            this.Password_tf.Size = new System.Drawing.Size(212, 20);
            this.Password_tf.TabIndex = 3;
            this.Password_tf.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // ConfirmPassword_tf
            // 
            this.ConfirmPassword_tf.Location = new System.Drawing.Point(193, 86);
            this.ConfirmPassword_tf.Name = "ConfirmPassword_tf";
            this.ConfirmPassword_tf.Size = new System.Drawing.Size(212, 20);
            this.ConfirmPassword_tf.TabIndex = 4;
            // 
            // Email_tf
            // 
            this.Email_tf.Location = new System.Drawing.Point(193, 112);
            this.Email_tf.Name = "Email_tf";
            this.Email_tf.Size = new System.Drawing.Size(212, 20);
            this.Email_tf.TabIndex = 5;
            // 
            // ConfirmEmail_tf
            // 
            this.ConfirmEmail_tf.Location = new System.Drawing.Point(193, 138);
            this.ConfirmEmail_tf.Name = "ConfirmEmail_tf";
            this.ConfirmEmail_tf.Size = new System.Drawing.Size(212, 20);
            this.ConfirmEmail_tf.TabIndex = 6;
            // 
            // Username_label
            // 
            this.Username_label.AutoSize = true;
            this.Username_label.Location = new System.Drawing.Point(134, 37);
            this.Username_label.Name = "Username_label";
            this.Username_label.Size = new System.Drawing.Size(55, 13);
            this.Username_label.TabIndex = 7;
            this.Username_label.Text = "Username";
            this.Username_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // Password_label
            // 
            this.Password_label.AutoSize = true;
            this.Password_label.Location = new System.Drawing.Point(134, 63);
            this.Password_label.Name = "Password_label";
            this.Password_label.Size = new System.Drawing.Size(53, 13);
            this.Password_label.TabIndex = 8;
            this.Password_label.Text = "Password";
            // 
            // Email_label
            // 
            this.Email_label.AutoSize = true;
            this.Email_label.Location = new System.Drawing.Point(153, 115);
            this.Email_label.Name = "Email_label";
            this.Email_label.Size = new System.Drawing.Size(32, 13);
            this.Email_label.TabIndex = 9;
            this.Email_label.Text = "Email";
            // 
            // ConfirmPassword_label
            // 
            this.ConfirmPassword_label.AutoSize = true;
            this.ConfirmPassword_label.Location = new System.Drawing.Point(96, 89);
            this.ConfirmPassword_label.Name = "ConfirmPassword_label";
            this.ConfirmPassword_label.Size = new System.Drawing.Size(91, 13);
            this.ConfirmPassword_label.TabIndex = 10;
            this.ConfirmPassword_label.Text = "Confirm Password";
            // 
            // ConfirmEmail_label
            // 
            this.ConfirmEmail_label.AutoSize = true;
            this.ConfirmEmail_label.Location = new System.Drawing.Point(117, 141);
            this.ConfirmEmail_label.Name = "ConfirmEmail_label";
            this.ConfirmEmail_label.Size = new System.Drawing.Size(70, 13);
            this.ConfirmEmail_label.TabIndex = 11;
            this.ConfirmEmail_label.Text = "Confirm Email";
            // 
            // Output_tf
            // 
            this.Output_tf.Location = new System.Drawing.Point(15, 217);
            this.Output_tf.Name = "Output_tf";
            this.Output_tf.Size = new System.Drawing.Size(390, 20);
            this.Output_tf.TabIndex = 12;
            this.Output_tf.TextChanged += new System.EventHandler(this.Output_tf_TextChanged);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Output_tf);
            this.Controls.Add(this.ConfirmEmail_label);
            this.Controls.Add(this.ConfirmPassword_label);
            this.Controls.Add(this.Email_label);
            this.Controls.Add(this.Password_label);
            this.Controls.Add(this.Username_label);
            this.Controls.Add(this.ConfirmEmail_tf);
            this.Controls.Add(this.Email_tf);
            this.Controls.Add(this.ConfirmPassword_tf);
            this.Controls.Add(this.Password_tf);
            this.Controls.Add(this.Username_tf);
            this.Controls.Add(this.Register_button);
            this.Controls.Add(this.Login_button);
            this.Name = "LoginForm";
            this.Text = "CryptDrive Login / Register";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Login_button;
        private System.Windows.Forms.Button Register_button;
        private System.Windows.Forms.TextBox Username_tf;
        private System.Windows.Forms.TextBox Password_tf;
        private System.Windows.Forms.TextBox ConfirmPassword_tf;
        private System.Windows.Forms.TextBox Email_tf;
        private System.Windows.Forms.TextBox ConfirmEmail_tf;
        private System.Windows.Forms.Label Username_label;
        private System.Windows.Forms.Label Password_label;
        private System.Windows.Forms.Label Email_label;
        private System.Windows.Forms.Label ConfirmPassword_label;
        private System.Windows.Forms.Label ConfirmEmail_label;
        private System.Windows.Forms.TextBox Output_tf;
    }
}