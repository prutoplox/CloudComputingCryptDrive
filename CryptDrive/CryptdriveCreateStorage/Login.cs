using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace CryptdriveCloud
{
    public static class Login
    {
        [FunctionName("Login")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues usernameReturn;
                bool gotUsername = req.Form.TryGetValue("username", out usernameReturn);

                Microsoft.Extensions.Primitives.StringValues passwordReturn;
                bool gotPassword = req.Form.TryGetValue("password", out passwordReturn);

                if (!gotUsername || !gotPassword)
                {
                    return new BadRequestObjectResult("Not all required information was supplied");
                }

                string password = passwordReturn.ToString();
                string username = usernameReturn.ToString();

                UpdateRowSource result = DbManager.GetUser(username);
                if (result != null)
                {
                    return new OkObjectResult($"{username} used the password {password}");
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
