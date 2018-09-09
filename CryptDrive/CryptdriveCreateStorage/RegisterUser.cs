using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System;

namespace CryptdriveCloud
{
    public static class RegisterUser
    {
        [FunctionName("RegisterUser")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            try
            {
                var test = req.Form;
                log.LogInformation(test.ToString());
                /*
                bool result = DbManager.RegisterUser(username, password, email);
                if (result)
                {
                    return (ActionResult)new OkObjectResult($"register new user successful");
                }
                else
                {
                    return (ActionResult)new BadRequestObjectResult($"Can not register new user");
                } */
                return new OkObjectResult($"User successfully registered");
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
