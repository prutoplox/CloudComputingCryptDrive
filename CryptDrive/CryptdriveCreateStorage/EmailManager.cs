using System;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CryptdriveCloud
{
    class EmailManager
    {
        public static void sendEmailToUser(string useremail, string username, string registrationlink, ILogger log)
        {
            MailMessage mail = new MailMessage("CryptDrive@outlook.de", useremail);
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.office365.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("CryptDrive@outlook.de", "CcSs2018CcSs2018");
            mail.Subject = "Welcome to CryptDrive " + username + "! [EmailConfirmation] ";
            mail.Body = "Hi" + username + ". Thank you for choosing CryptDrive. Please confirm your mail with following link: " + registrationlink;
            client.Send(mail);
            if (true)
            {
                log.LogInformation(string.Format("Class\n"));
            }
        }
    }
}