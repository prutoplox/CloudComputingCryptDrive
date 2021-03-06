using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CryptdriveCloud
{
    public static class RegisterUser
    {
        [FunctionName("RegisterUser")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            try
            {
                // TODO WORK IN PROGRESS DONT WORK AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
                log.LogInformation("C# HTTP trigger function starts process a request.");
                log.LogInformation("Request is a " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                Microsoft.Extensions.Primitives.StringValues usernameReturn;
                bool gotUsername = req.Form.TryGetValue("username", out usernameReturn);

                Microsoft.Extensions.Primitives.StringValues passwordReturn;
                bool gotPassword = req.Form.TryGetValue("password", out passwordReturn);

                Microsoft.Extensions.Primitives.StringValues emailReturn;
                bool gotEmail = req.Form.TryGetValue("email", out emailReturn);

                if (!gotUsername || !gotPassword || !gotEmail)
                {
                    return new BadRequestObjectResult("Not all required information was supplied");
                }

                string password = passwordReturn.ToString();
                string username = usernameReturn.ToString();
                string email = emailReturn.ToString();
                string container = StorageCreate.getNameFor(username);

                bool result = DbManager.RegisterUser(username, password, email, container);

                if (result)
                {
                    using (SHA256CryptoServiceProvider hashFunction = new SHA256CryptoServiceProvider())
                    {
                        string content = "username=" + username + "&linkId=" + NeoSmart.Utils.UrlBase64.Encode(hashFunction.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))) + NeoSmart.Utils.UrlBase64.Encode(hashFunction.ComputeHash(System.Text.Encoding.UTF8.GetBytes(email)));
                        string registrationLink = Cryptdrive.AzureLinkStringStorage.CONFIRM_EMAIL_AZURE_STRING + Cryptdrive.AzureLinkStringStorage.LINKING_INITALCHARACTER + content;
                        EmailManager.sendEmailToUser(email, username, registrationLink, log);
                        await StorageCreate.create(username);
                        return new OkObjectResult($"{container}");
                    }
                }
                else
                {
                    return (ActionResult)new BadRequestObjectResult($"Can not register new user");
                }
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                log.LogError(e.StackTrace);
                return new UnprocessableEntityObjectResult(e.Message);
            }
        }
    }
}
