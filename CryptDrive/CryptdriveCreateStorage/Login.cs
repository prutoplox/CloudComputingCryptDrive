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
using System.Collections.Generic;

namespace CryptdriveCloud
{
    public static class Login
    {
        [FunctionName("Login")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            try
            {
                //DbManager.instance.CreateTableIfNotExists();
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

                List<string> result = DbManager.GetUser(username);

                if (result.Count != 0)
                {
                    if (result[1] == username && result[2] == password)
                    {
                        return new OkObjectResult($"{StorageCreate.get(username)}");
                    }
                    else
                    {
                        return (ActionResult)new BadRequestObjectResult($"Wrong Login Data for {username} ");
                    }
                }
                else
                {
                    return (ActionResult)new BadRequestObjectResult($"Can not login user");
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
