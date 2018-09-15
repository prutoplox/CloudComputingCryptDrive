using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Net.Mail;

using System;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace CryptdriveCloud
{
    class EmailManager
    {
        public static async Task sendEmailToUser(string useremail, string username, string registrationlink, ILogger log)
        {
            /* MailMessage mail = new MailMessage("cryptdrive@outlook.de", useremail);
             SmtpClient client = new SmtpClient();
             client.Port = 587;
             client.DeliveryMethod = SmtpDeliveryMethod.Network;
             client.UseDefaultCredentials = false;
             client.Credentials = new System.Net.NetworkCredential("crypdrive", "CcSs2018CcSs2018");
             client.Host = "smtp-mail.outlook.com";
             client.EnableSsl = true;
             client.ClientCertificates;
             mail.Subject = "Welcome to CryptDrive - Confirm your registration!";
             mail.Body = registrationlink;

             client.SendCompleted += Client_SendCompleted;

            await client.SendMailAsync(mail);*/

            Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
            Outlook.MailItem mailItem = (Outlook.MailItem)app.CreateItem(Outlook.OlItemType.olMailItem);
            mailItem.Subject = "Welcome to CryptDrive - Confirm your registration!";
            mailItem.To = useremail;
            mailItem.Body = registrationlink;
            mailItem.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
            mailItem.Display(false);
            mailItem.Send();
            if (mailItem.Submitted == true)
            {
                log.LogInformation(string.Format("Class\n"));
            }
        }
    }
}

/*MailjetClient client = new MailjetClient(Cryptdrive.AzureLinkStringStorage.MJ_APIKEY_PUBLIC, Cryptdrive.AzureLinkStringStorage.MJ_APIKEY_PRIVATE)
           {
               Version = ApiVersion.V3_1,
           };
           MailjetRequest request = new MailjetRequest
           {
               Resource = Send.Resource,
           }
              .Property(Send.Messages, new JArray {
               new JObject {
                {"From", new JObject {
                 {"Email", "cryptdrive@outlook.de"},
                 {"Name", "No Reply @ CryptDrive[Bot]"}
                 }},
                {"To", new JArray {
                 new JObject {
                  {"Email", useremail},
                  {"Name", username}
                  }
                 }},
                {"Subject", "Welcome To CryptDrive ! - [Registration-Link]"},
                {"TextPart", registrationlink},
                }
                  });
           MailjetResponse response = await client.PostAsync(request);
           if (response.IsSuccessStatusCode)
           {
               log.LogInformation(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
               log.LogInformation(response.GetData().ToString());
           }
           else
           {
               log.LogInformation(string.Format("StatusCode: {0}\n", response.StatusCode));
               log.LogInformation(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
               log.LogInformation(response.GetData().ToString());
               log.LogInformation(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
           }
            */
