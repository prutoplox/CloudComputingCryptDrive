using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CryptdriveCloud
{
    public static class BlobDelete
    {
        [FunctionName("BlobDelete")]
        public async static Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function starts process a request.");
                log.LogInformation("Request is a " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                string containerName = req.Query["containername"];
                if (containerName == null)
                {
                    string errorMessage = "No container name given!";
                    log.LogError(errorMessage);
                    return new BadRequestObjectResult(errorMessage);
                }
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Cryptdrive.AzureLinkStringStorage.STORAGE_CONNECTION_STRING);
                var client = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = BlobManager.getBlobContainer(containerName);
                bool isFormType = req.HasFormContentType;
                int numberOfDeletedFiles = 0;
                var keys = req.Form.Keys;
                foreach (var key in keys)
                {
                    StringValues returns;
                    req.Form.TryGetValue(key, out returns);
                    log.LogInformation("Trying to delete " + returns);
                    log.LogInformation("    From key " + key);
                    bool deleted = await container.GetBlockBlobReference(returns).DeleteIfExistsAsync();
                    if (deleted)
                    {
                        numberOfDeletedFiles++;
                    }
                }
                return new OkObjectResult(numberOfDeletedFiles + " files deleted from the cloud");
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
