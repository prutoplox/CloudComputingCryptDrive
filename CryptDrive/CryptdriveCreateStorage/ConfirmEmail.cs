using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CryptdriveCloud
{
    public static class ConfirmEmail
    {
        [FunctionName("ConfirmEmail")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            try
            {
                string username = req.Query["username"];
                if (username == null)
                {
                    string errorMessage = "No username given!";
                    log.LogError(errorMessage);
                    return new BadRequestObjectResult(errorMessage);
                }

                string linkId = req.Query["linkId"];
                if (linkId == null)
                {
                    string errorMessage = "No linkId name given!";
                    log.LogError(errorMessage);
                    return new BadRequestObjectResult(errorMessage);
                }

                List<string> columns = DbManager.GetUser(username);

                string password = columns[2];
                string email = columns[3];
                using (SHA256CryptoServiceProvider hashFunction = new SHA256CryptoServiceProvider())
                {
                    string shouldBeLinkID = NeoSmart.Utils.UrlBase64.Encode(hashFunction.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))) + NeoSmart.Utils.UrlBase64.Encode(hashFunction.ComputeHash(System.Text.Encoding.UTF8.GetBytes(email)));
                    if (shouldBeLinkID == linkId)
                    {
                        if (DbManager.UpdateUserEmailConfirmed(username))
                        {
                            return new OkObjectResult("Email confirmed");
                        }
                        else
                        {
                            return new BadRequestObjectResult("Could not confirm email");
                        }
                    }
                    else
                    {
                        return new BadRequestObjectResult("The link ID was not correct for the user");
                    }
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error while Confirm Email");
            }
        }
    }
}
