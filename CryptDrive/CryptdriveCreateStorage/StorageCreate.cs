using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NeoSmart.Utils;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptdriveCloud
{
    static class StorageCreate
    {
        static async public Task<string> create(string username)
        {
            string usernameContainer = get(username);
            usernameContainer = Regex.Replace(usernameContainer, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled).ToLower();

            //get the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Cryptdrive.AzureLinkStringStorage.STORAGE_CONNECTION_STRING);

            //blob client now
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(usernameContainer);

            //Create a new container, if it does not exist
            await container.CreateIfNotExistsAsync();
            return (usernameContainer);
        }

        static public string get(string username)
        {
            var sha256 = new SHA256CryptoServiceProvider();
            return UrlBase64.Encode(sha256.ComputeHash(Encoding.UTF8.GetBytes(username)));
        }
    }
}
