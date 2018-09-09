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

        private void button1_Click(object sender, EventArgs e)
        {
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
                if (!String.IsNullOrWhiteSpace(Username_tf.Text) || !String.IsNullOrWhiteSpace(Password_tf.Text) || !String.IsNullOrWhiteSpace(Email_tf.Text))
                {
                    string username = Username_tf.Text;
                    string password = Password_tf.Text;
                    string confirmPassword = ConfirmPassword_tf.Text;
                    string email = Email_tf.Text;
                    string confirmEmail = ConfirmEmail_tf.Text;

                    if (password != confirmPassword)
                    {
                        //TODO ErrorMessage
                        return;
                    }
                    else if (email != confirmEmail)
                    {
                        //TODO ErrorMessage
                        return;
                    }

                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(new StringContent(username), "username");
                    multipartContent.Add(new StringContent(email), "email");
                    multipartContent.Add(new StringContent(password), "password");
                    var response = await AzureConnectionManager.client.PostAsync(AzureLinkStringStorage.REGISTER_USER_AZURE_STRING, multipartContent);

                    var responseString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("RESPONSE:" + responseString);
                    register = false;

                    /*HttpClient httpClient = new HttpClient();
        MultipartFormDataContent form = new MultipartFormDataContent();

        form.Add(new StringContent(username), "username");
        form.Add(new StringContent(useremail), "email");
        form.Add(new StringContent(password), "password");
        form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "profile_pic", "hello1.jpg");
        HttpResponseMessage response = await httpClient.PostAsync("PostUrl", form);

        response.EnsureSuccessStatusCode();
        httpClient.Dispose();
        string sd = response.Content.ReadAsStringAsync().Result;*/
                }
                else
                {
                    //TODO ErrorMessage
                    showRegisterFields();
                    register = true;
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
    }
}
