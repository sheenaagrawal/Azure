using System;
using Microsoft.WindowsAzure.Storage.Blob;
using AzureBlob.Interface;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace AzureBlob.Impl
{
    public class BlobClient : IBlobClient
    {
        private CloudBlobContainer _container;

        public BlobClient()
        {
            CreateContainerClient().GetAwaiter().GetResult();
        }

        private async Task CreateContainerClient()
        {
            var storageaccount = CloudStorageAccount.Parse("<connectionString>");
            var client = storageaccount.CreateCloudBlobClient();
            _container = client.GetContainerReference("<containerName>");
            await _container.CreateIfNotExistsAsync();

        }

        public string GetReadOnlySas(string blobName)
        {
            try
            {
                var blobClient = _container.GetBlobReference(blobName);
                var policy = new SharedAccessBlobPolicy()
                {
                    Permissions = SharedAccessBlobPermissions.Read,
                    SharedAccessExpiryTime = System.DateTimeOffset.UtcNow.AddDays(7)
                };
                return blobClient.GetSharedAccessSignature(policy);
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
