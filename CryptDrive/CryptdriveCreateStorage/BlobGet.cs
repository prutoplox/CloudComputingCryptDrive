using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptdriveCloud
{
    public static class BlobGet
    {
        /// <summary>
        /// Returns a link to the blob which can then be downloaded
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("BlobGet")]
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

                string blobName = req.Query["blobname"];
                if (blobName == null)
                {
                    string errorMessage = "No blob name given!";
                    log.LogError(errorMessage);
                    return new BadRequestObjectResult(errorMessage);
                }

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Cryptdrive.AzureLinkStringStorage.STORAGE_CONNECTION_STRING);
                var backupBlobClient = storageAccount.CreateCloudBlobClient();
                var cloudBlobContainer = backupBlobClient.GetContainerReference(containerName);
                CloudBlob blob = cloudBlobContainer.GetBlobReference(blobName);
                if (await blob.ExistsAsync())
                {
                    return new OkObjectResult(blob.Uri.ToString());
                }
                else
                {
                    return new BadRequestObjectResult("Error while getting File");
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
