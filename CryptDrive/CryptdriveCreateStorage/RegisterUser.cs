using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace CryptdriveCloud
{
    public static class RegisterUser
    {
        [FunctionName("RegisterUser")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            string username = req.Query["name"];

            //TODO KlartextPW
            string password = req.Query["password"];
            string pkString = req.Query["pkString"];
            string email = req.Query["email"];
            bool result = DbManager.RegisterUser(username, password, email);
            if (result)
            {
                return (ActionResult)new OkObjectResult($"register new user successful");
            }
            else
            {
                return (ActionResult)new BadRequestObjectResult($"Can not register new user");
            }
        }
    }
}
