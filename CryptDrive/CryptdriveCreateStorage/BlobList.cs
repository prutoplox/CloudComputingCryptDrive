using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using System;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace CryptdriveCloud
{
    public static class BlobList
    {
        [FunctionName("BlobList")]
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
                var backupBlobClient = storageAccount.CreateCloudBlobClient();
                var cloudBlobContainer = backupBlobClient.GetContainerReference(containerName);

                BlobContinuationToken blobContinuationToken = null;
                do
                {
                    var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);

                    // Get the value of the continuation token returned by the listing call.
                    blobContinuationToken = results.ContinuationToken;
                    foreach (IListBlobItem item in results.Results)
                    {
                        log.LogInformation(item.Uri.ToString());
                    }
                } while (blobContinuationToken != null); // Loop while the continuation token is not null.

                return new OkObjectResult($"lul");
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
