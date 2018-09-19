using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptdriveCloud
{
    static class BlobManager
    {
        public static readonly int maxSizeOfBlobForUser = 10000000; //10MB

        public static CloudBlobContainer getBlobContainer(string containerName)
        {
            // Retrieve storage account information from connection string
            // How to create a storage connection string - http://msdn.microsoft.com/en-us/library/azure/ee758697.aspx
            CloudStorageAccount storageAccount = getStorageAccount();

            // Create a blob client for interacting with the blob service.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            return container;
        }

        public static async Task<int> getSizeInBytes(string containerName)
        {
            CloudBlobContainer cloudBlobContainer = getBlobContainer(containerName);

            BlobContinuationToken blobContinuationToken = null;
            int totalSize = 0;
            do
            {
                var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);

                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    if (item is CloudBlockBlob blob)
                    {
                        totalSize += (int)blob.Properties.Length;
                    }
                }
            } while (blobContinuationToken != null); // Loop while the continuation token is not null.

            return totalSize;
        }

        public static CloudStorageAccount getStorageAccount()
        {
            var storageAccount = CloudStorageAccount.Parse(Cryptdrive.AzureLinkStringStorage.STORAGE_CONNECTION_STRING);
            return storageAccount;
        }

        public static void deleteBlobs()
        {
        }
    }
}
