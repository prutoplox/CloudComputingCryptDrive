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
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                log.LogInformation("Request is a " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                string containerName = req.Query["container"];
                if (containerName == null)
                {
                    string errorMessage = "No container name given!";
                    log.LogError(errorMessage);
                    return new BadRequestObjectResult(errorMessage);
                }

                string filenameOld = req.Query["filenameOld"];
                if (filenameOld == null)
                {
                    string errorMessage = "No old file name given!";
                    log.LogError(errorMessage);
                    return new BadRequestObjectResult(errorMessage);
                }

                string filenameNew = req.Query["filenameNew"];
                if (filenameNew == null)
                {
                    string errorMessage = "No new file name given!";
                    log.LogError(errorMessage);
                    return new BadRequestObjectResult(errorMessage);
                }

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

                        await releaseLease(sourceBlob);

                        log.LogInformation("Released the lease, now comes the deleting...");

                        //delete old file after copy is done
                        await sourceBlob.DeleteIfExistsAsync();
                        log.LogInformation("... file deleted, rename finished");
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
                    await releaseLease(sourceBlob);
                }
                return (ActionResult)new OkObjectResult($"Hello, ");
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                log.LogError(e.StackTrace);
                return new UnprocessableEntityObjectResult(e.Message);
            }
        }

        private static async Task releaseLease(CloudBlockBlob sourceBlob)
        {
            // Break the lease on the source blob.
            if (sourceBlob != null)
            {
                if (await sourceBlob.ExistsAsync())
                {
                    await sourceBlob.FetchAttributesAsync();

                    if (sourceBlob.Properties.LeaseState != LeaseState.Available)
                    {
                        await sourceBlob.BreakLeaseAsync(new TimeSpan(0));
                    }
                }
            }
        }
    }
}
