﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptdrive
{
    public class AzureLinkStringStorage
    {
        public const string BLOB_ADD_AZURE_STRING = "http://localhost:7071/api/BlobAdd";
        public const string DELETE_AZURE_STRING = "http://localhost:7071/api/BlobDelete";
        public const string BLOB_GET_AZURE_STRING = "http://localhost:7071/api/BlobGet";
        public const string DBTESTFUNCTION_AZURE_STRING = "http://localhost:7071/api/DBTestFunction";
        public const string REGISTER_USER_AZURE_STRING = "http://localhost:7071/api/RegisterUser";
        public const string STORAGE_CREATE_AZURE_STRING = "http://localhost:7071/api/StorageCreate";
        public const string STORAGE_DELETE_AZURE_STRING = "http://localhost:7071/api/StorageDelete";
        public const string SYNC_BLOB_AZURE_STRING = "http://localhost:7071/api/SyncBlob";
        public const string STORAGE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=devstoragemarius;AccountKey=Z0+9qcUyTzgovwkCCsLWH0RZUkih8/tltiabksN3QfB7SthB3c7TYdJBgA7KuTi6B40CMqF1BRAQk5cL6tMfSQ==;EndpointSuffix=core.windows.net";
        public const string DB_DATASOURCE = "cryptdriveruserdb.database.windows.net";
        public const string DB_USERID = "cryptAdmin";
        public const string DB_PASSWORD = "CcSs2018";
        public const string DB_INITALCATALOG = "cryptdriverdb";
    }
}