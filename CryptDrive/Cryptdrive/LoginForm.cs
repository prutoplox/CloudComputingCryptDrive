using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cryptdrive
{
    public partial class LoginForm : Form
    {
        private bool register = false;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            hideRegisterFields();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private async void Login_button_Click(object sender, EventArgs e)
        {
            if (ConfirmPassword_tf.Visible)
            {
                toggleRegisterFields();
            }

            if (String.IsNullOrWhiteSpace(Username_tf.Text) || String.IsNullOrWhiteSpace(Password_tf.Text))
            {
                Logger.instance.logError("Not all textfelds are filled out, the empty textfields are "
                    + (String.IsNullOrWhiteSpace(Username_tf.Text) ? "Username " : "")
                    + (String.IsNullOrWhiteSpace(Password_tf.Text) ? "Password " : "")
                    );

                //TODO ErrorMessage
                register = true;
                return;
            }

            try
            {
                string username = Username_tf.Text;
                string password = Password_tf.Text;
                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new StringContent(username), "username");
                multipartContent.Add(new StringContent(password), "password");
                var response = await AzureConnectionManager.client.PostAsync(AzureLinkStringStorage.LOGIN_AZURE_STRING, multipartContent);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    this.Visible = false;
                    var responseString = await response.Content.ReadAsStringAsync();
                    Logger.instance.logInfo("Logged in successfully with following server answer:" + responseString);
                    ActiveUserStorage.instance.setActiveUser(username, responseString);
                    FileManager.instance.containerName = responseString;
                    GUIForm.instance.Visible = true;
                }
            }
            catch (HttpRequestException exc)
            {
                Logger.instance.logError("Could not log in due to the network error " + exc.Message);
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private async void Register_button_Click(object sender, EventArgs e)
        {
            if (!ConfirmPassword_tf.Visible)
            {
                toggleRegisterFields();
                return;
            }

            if (String.IsNullOrWhiteSpace(Username_tf.Text) || String.IsNullOrWhiteSpace(Password_tf.Text) || String.IsNullOrWhiteSpace(Email_tf.Text))
            {
                Logger.instance.logError("Not all textfelds are filled out, the empty textfields are "
                       + (String.IsNullOrWhiteSpace(Username_tf.Text) ? "Username " : "")
                       + (String.IsNullOrWhiteSpace(Password_tf.Text) ? "Password " : "")
                       + (String.IsNullOrWhiteSpace(Email_tf.Text) ? "Email " : "")
                       );

                //TODO ErrorMessage
                return;
            }

            string username = Username_tf.Text;
            string password = Password_tf.Text;
            string confirmPassword = ConfirmPassword_tf.Text;
            string email = Email_tf.Text;
            string confirmEmail = ConfirmEmail_tf.Text;

            if (password != confirmPassword)
            {
                //TODO ErrorMessage
                Logger.instance.logError("Passwords are not matching");
                return;
            }
            else if (email != confirmEmail)
            {
                //TODO ErrorMessage
                Logger.instance.logError("Emails are not matching");
                return;
            }

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(username), "username");
            multipartContent.Add(new StringContent(email), "email");
            multipartContent.Add(new StringContent(password), "password");
            try
            {
                var response = await AzureConnectionManager.client.PostAsync(AzureLinkStringStorage.REGISTER_USER_AZURE_STRING, multipartContent);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Output_tf.Text = responseString;
                    FileManager.instance.containerName = responseString;
                    ActiveUserStorage.instance.setActiveUser(username, responseString);
                    this.Hide();
                    GUIForm.instance.Visible = true;
                    Logger.instance.logInfo("Registered successfully with following server message:" + responseString);
                }
                else
                {
                    Logger.instance.logError("Could not register, server responded with: " + responseString);
                }
            }
            catch (HttpRequestException exc)
            {
                Logger.instance.logError("Could not register on the server due to a network error");
                Logger.instance.logError(exc.Message);
                return;
            }
        }

        private void showRegisterFields()
        {
            ConfirmPassword_tf.Show();
            ConfirmPassword_label.Show();
            Email_label.Show();
            Email_tf.Show();
            ConfirmEmail_label.Show();
            ConfirmEmail_tf.Show();
        }

        private void hideRegisterFields()
        {
            ConfirmPassword_tf.Hide();
            ConfirmPassword_label.Hide();
            Email_label.Hide();
            Email_tf.Hide();
            ConfirmEmail_label.Hide();
            ConfirmEmail_tf.Hide();
        }

        private void toggleRegisterFields()
        {
            ConfirmPassword_tf.Visible = !ConfirmPassword_tf.Visible;
            ConfirmPassword_label.Visible = !ConfirmPassword_label.Visible;
            Email_label.Visible = !Email_label.Visible;
            Email_tf.Visible = !Email_tf.Visible;
            ConfirmEmail_label.Visible = !ConfirmEmail_label.Visible;
            ConfirmEmail_tf.Visible = !ConfirmEmail_tf.Visible;
        }

        private void Output_tf_TextChanged(object sender, EventArgs e)
        {
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private async void ResetDB_Click(object sender, EventArgs e)
        {
            var response = await AzureConnectionManager.client.PostAsync(AzureLinkStringStorage.RESET_DB, null);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        private void skipLogin_Click(object sender, EventArgs e)
        {
            FileManager.instance.containerName = "testcontainer";
            this.Hide();
            GUIForm.instance.Visible = true;
        }

        private async void initAll_Click(object sender, EventArgs e)
        {
            var response = await AzureConnectionManager.client.PostAsync(AzureLinkStringStorage.INIT_ALL, null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void ConfirmPassword_tf_TextChanged(object sender, EventArgs e)
        {
        }

        private void Email_tf_TextChanged(object sender, EventArgs e)
        {
        }

        private void ConfirmEmail_tf_TextChanged(object sender, EventArgs e)
        {
        }

        private void ConfirmEmail_label_Click(object sender, EventArgs e)
        {
        }

        private void Email_label_Click(object sender, EventArgs e)
        {
        }

        private void ConfirmPassword_label_Click(object sender, EventArgs e)
        {
        }

        private void Password_label_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
    }
}
