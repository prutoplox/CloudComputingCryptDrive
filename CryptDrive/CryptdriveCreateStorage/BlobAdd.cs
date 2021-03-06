using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CryptdriveCloud
{
    public static class BlobAdd
    {
        [FunctionName("BlobAdd")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function starts process a request.");
                log.LogInformation("Request is a " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                string containerName = req.Query["username"];
                if (containerName == null)
                {
                    string errorMessage = "No container name given!";
                    log.LogError(errorMessage);
                    return new BadRequestObjectResult(errorMessage);
                }

                string fileName = req.Query["filename"];
                if (fileName == null)
                {
                    string errorMessage = "No file name given!";
                    log.LogError(errorMessage);
                    return new BadRequestObjectResult(errorMessage);
                }

                if (req.ContentLength == null || req.ContentLength == 0)
                {
                    return new BadRequestObjectResult("Could not see raw data");
                }
                else
                {
                    log.LogInformation(@"Uploading {req.ContentLength} bytes");
                }

                if (req.ContentLength > int.MaxValue)
                {
                    return new BadRequestObjectResult("Content to upload is too large");
                }

                int sizeOfBlobsInContainer = await BlobManager.getSizeInBytes(containerName);
                int remainingSize = BlobManager.maxSizeOfBlobForUser - sizeOfBlobsInContainer;

                if (req.ContentLength > remainingSize)
                {
                    log.LogError("User tried to upload too much into his container");
                    return new UnauthorizedResult();
                }
                else
                {
                    log.LogInformation("User has enough free space");
                }

                byte[] data = new byte[(int)req.ContentLength];
                req.Body.Read(data, 0, (int)req.ContentLength);
                log.LogInformation("Reading container name");
                CloudBlobContainer container = BlobManager.getBlobContainer(containerName);

                // Upload a BlockBlob to the newly created container
                log.LogInformation("Uploading BlockBlob");
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                await blockBlob.UploadFromByteArrayAsync(data, 0, data.Length);

                log.LogInformation("C# HTTP trigger function finish process a request.");

                return new OkObjectResult($"Uploaded {fileName} to {containerName} which had a size of {req.ContentLength}");
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                log.LogError(e.StackTrace);
                return new UnprocessableEntityObjectResult(e.Message);
            }
        }

        private static async void ListBlobs(CloudBlobContainer container, ILogger log)
        {
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await container.ListBlobsSegmentedAsync(null, blobContinuationToken);

                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    log.LogInformation(item.Uri.ToString());
                }
            } while (blobContinuationToken != null); // Loop while the continuation token is not null.
        }
    }
}
