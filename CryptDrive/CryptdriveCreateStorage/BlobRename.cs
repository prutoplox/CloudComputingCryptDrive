using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System;
using Microsoft.WindowsAzure.Storage;

namespace CryptdriveCloud
{
    public static class BlobRename
    {
        [FunctionName("BlobRename")]
        public static async System.Threading.Tasks.Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string containerName = req.Query["container"];
            string filenameOld = req.Query["filenameOld"];
            string filenameNew = req.Query["filenameNew"];
            log.LogInformation("Got the names:" + filenameOld + "->" + filenameNew + " in " + containerName);
            CloudBlobContainer container = BlobManager.getBlobContainer(containerName);

            // Based on https://github.com/Azure-Samples/storage-blob-dotnet-getting-started/blob/master/BlobStorage/Advanced.cs function CopyBlockBlobAsync
            var sourceBlob = container.GetBlockBlobReference(filenameOld); ;
            var destBlob = container.GetBlockBlobReference(filenameNew);
            string leaseId = null;
            log.LogInformation("Got the container references");

            try
            {
                // Lease the source blob for the copy operation to prevent another client from modifying it.
                // Specifying null for the lease interval creates an infinite lease.
                leaseId = await sourceBlob.AcquireLeaseAsync(null);

                // Ensure that the source blob exists.
                if (await sourceBlob.ExistsAsync())
                {
                    // Get the ID of the copy operation.
                    string copyId = await destBlob.StartCopyAsync(sourceBlob);

                    // Fetch the destination blob's properties before checking the copy state.
                    await destBlob.FetchAttributesAsync();

                    log.LogInformation("Status of copy operation: {0}", destBlob.CopyState.Status);
                    log.LogInformation("Completion time: {0}", destBlob.CopyState.CompletionTime);
                    log.LogInformation("Bytes copied: {0}", destBlob.CopyState.BytesCopied.ToString());
                    log.LogInformation("Total bytes: {0}", destBlob.CopyState.TotalBytes.ToString());
                }
                else
                {
                    log.LogError("Source not found!");
                }
            }
            catch (StorageException e)
            {
                log.LogError(e.Message);
                throw;
            }
            finally
            {
                // Break the lease on the source blob.
                if (sourceBlob != null)
                {
                    await sourceBlob.FetchAttributesAsync();

                    if (sourceBlob.Properties.LeaseState != LeaseState.Available)
                    {
                        await sourceBlob.BreakLeaseAsync(new TimeSpan(0));
                    }
                }
            }
            return (ActionResult)new OkObjectResult($"Hello, ");
        }
    }
}
