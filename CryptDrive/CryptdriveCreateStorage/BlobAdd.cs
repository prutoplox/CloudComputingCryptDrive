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
        [FunctionName("AddBlob")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function starts process a request.");
                string connectionString = "DefaultEndpointsProtocol=https;AccountName=temporaryteststorage;AccountKey=hkmPMZAmtW+0hckSgl0CqPsx7+e0GVVVLQ28ZE4Grz/I7fhotAhbYyhb8FvCkxErTILV/7Xv0PUaAKMRRgL1wA==;EndpointSuffix=core.windows.net";
                string containerName = req.Query["username"];
                string fileName = req.Query["filename"];

                if (req.ContentLength == 0)
                {
                    return new BadRequestObjectResult("Could not see raw data");
                }
                else
                {
                    log.LogInformation("Uploading {req.ContentLength} bytes");
                }

                if (req.ContentLength > int.MaxValue)
                {
                    return new BadRequestObjectResult("Content to upload is too large");
                }
                byte[] data = new byte[(int)req.ContentLength];
                req.Body.Read(data, 0, (int)req.ContentLength);

                // Retrieve storage account information from connection string
                // How to create a storage connection string - http://msdn.microsoft.com/en-us/library/azure/ee758697.aspx
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

                log.LogInformation("Reading container name");

                // Create a blob client for interacting with the blob service.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                // Upload a BlockBlob to the newly created container
                log.LogInformation("Uploading BlockBlob");
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
                await blockBlob.UploadFromByteArrayAsync(data, 0, data.Length);

                // List the blobs in the container, without this the blobs weren't showing up on the panel on the website for some reason...
                //Console.WriteLine("List blobs in container.");
                //ListBlobs(container, log);

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
