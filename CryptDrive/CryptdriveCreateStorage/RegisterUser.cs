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

namespace CryptdriveCloud
{
    public static class RegisterUser
    {
        [FunctionName("RegisterUser")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            try
            {
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
                    await StorageCreate.create(username);
                    return new OkObjectResult($"{container}");
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
