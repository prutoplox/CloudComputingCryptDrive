using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptdriveCloud
{
    static class BlobManager
    {
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
