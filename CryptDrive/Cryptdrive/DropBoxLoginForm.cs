using Dropbox.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cryptdrive
{
    public partial class DropBoxLoginForm : Form
    {
        private DropBoxLoginForm()
        {
            InitializeComponent();
        }

        #region Variables

        private const string RedirectUri = "https://localhost/authorize";

        private string DBAppKey = "q4ela0mxhrm7eti";
        private string DBAuthenticationURL = string.Empty;
        private string DBoauth2State = string.Empty;

        #endregion Variables

        #region Properties

        public string AccessToken { get; private set; }

        public string UserId { get; private set; }

        public bool Result { get; private set; }

        #endregion Properties

        public DropBoxLoginForm(string AuthenticationURL, string oauth2State) : this()
        {
            DBAuthenticationURL = AuthenticationURL;
            DBoauth2State = oauth2State;
        }

        public void Navigate()
        {
            try
            {
                if (!string.IsNullOrEmpty(DBAppKey))
                {
                    // Uri authorizeUri = new Uri(DBAuthenticationURL);
                    Uri auth = new Uri(DBAuthenticationURL);

                    //webBrowser1.Url = auth;
                    webBrowser1.Navigate(auth);

                    //webBrowser1.Navigate(new Uri(DBAuthenticationURL));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!e.Url.AbsoluteUri.ToString().StartsWith(RedirectUri.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                // we need to ignore all navigation that isn't to the redirect uri.
                return;
            }

            try
            {
                OAuth2Response result = DropboxOAuth2Helper.ParseTokenFragment(e.Url);
                if (result.State != DBoauth2State)
                {
                    return;
                }

                this.AccessToken = result.AccessToken;
                this.UserId = result.Uid;
                this.Result = true;
            }
            catch (ArgumentException ex)
            {
            }
            finally
            {
                e.Cancel = true;
                this.Close();
            }
        }

        private void DropBoxLoginForm_Load(object sender, EventArgs e)
        {
            Navigate();

            // webBrowser1.BeginInvoke(new Action(Navigate));
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (!e.Url.AbsoluteUri.ToString().StartsWith(RedirectUri.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                // we need to ignore all navigation that isn't to the redirect uri.
                return;
            }

            try
            {
                OAuth2Response result = DropboxOAuth2Helper.ParseTokenFragment(e.Url);
                if (result.State != DBoauth2State)
                {
                    return;
                }

                this.AccessToken = result.AccessToken;
                this.UserId = result.Uid;
                this.Result = true;
            }
            catch (ArgumentException ex)
            {
            }
            finally
            {
                this.Close();
            }
        }
    }
}
