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
            if (register == false)
            {
                if (!String.IsNullOrWhiteSpace(Username_tf.Text) && !String.IsNullOrWhiteSpace(Password_tf.Text))
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
                        Logger.instance.logInfo("RESPONSE:" + responseString);
                        FileManager.instance.containerName = responseString;
                        GUIForm.instance.Visible = true;
                    }
                    register = false;
                }
                else
                {
                    Logger.instance.logError("Not all textfelds are filled out, the empty textfields are "
                        + (String.IsNullOrWhiteSpace(Username_tf.Text) ? "Username " : "")
                        + (String.IsNullOrWhiteSpace(Password_tf.Text) ? "Password " : "")
                        );

                    //TODO ErrorMessage
                    register = true;
                    return;
                }
            }
            else
            {
                register = true;
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
            if (register == true)
            {
                if (!String.IsNullOrWhiteSpace(Username_tf.Text) && !String.IsNullOrWhiteSpace(Password_tf.Text) && !String.IsNullOrWhiteSpace(Email_tf.Text))
                {
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
                    var response = await AzureConnectionManager.client.PostAsync(AzureLinkStringStorage.REGISTER_USER_AZURE_STRING, multipartContent);
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Output_tf.Text = responseString;
                        FileManager.instance.containerName = responseString;
                        this.Hide();
                        GUIForm.instance.Visible = true;
                    }

                    Logger.instance.logInfo("RESPONSE:" + responseString);
                }
                else
                {
                    Logger.instance.logError("Not all textfelds are filled out, the empty textfields are "
                        + (String.IsNullOrWhiteSpace(Username_tf.Text) ? "Username " : "")
                        + (String.IsNullOrWhiteSpace(Password_tf.Text) ? "Password " : "")
                        + (String.IsNullOrWhiteSpace(Email_tf.Text) ? "Email " : "")
                        );

                    //TODO ErrorMessage
                    hideRegisterFields();
                    register = false;
                    return;
                }
            }
            else
            {
                showRegisterFields();
                register = true;
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

        private void Output_tf_TextChanged(object sender, EventArgs e)
        {
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileManager.instance.containerName = "testcontainer";
            this.Hide();
            GUIForm.instance.Visible = true;
        }
    }
}
