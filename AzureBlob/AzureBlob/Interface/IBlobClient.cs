namespace AzureBlob.Interface
{
    public interface IBlobClient
    {
        string GetReadOnlySas(string blobName);
    }
}
