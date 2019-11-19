using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlogApp.Common;

namespace BlogApp.Infrastructure
{
    public static class BlobService
    {
        private static readonly BlobServiceClient ServiceClient =
            new BlobServiceClient(Constants.ConnectionString);

        private static readonly BlobContainerClient ContainerClient =
            ServiceClient.GetBlobContainerClient(Constants.Container);


        public static async Task<(string name, string content)> GetBlob(string name)
        {
            var blobClient = ContainerClient.GetBlobClient(name);
            await using var memoryStream = new MemoryStream();
            try
            {
                await blobClient.DownloadAsync(memoryStream);
            }
            catch (StorageRequestFailedException e)
                when (e.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                Console.WriteLine($"Blob {name} does not exist.");
                return (null, null);
            }

            var content = Encoding.UTF8.GetString(memoryStream.ToArray());
            return (name, content);
        }

        public static async Task<IList<(string blobName, string blobContent)>> GetAllBlobs()
        {
            var list = new List<(string blobName, string blobContent)>();
            await foreach (var blob in ContainerClient.GetBlobsAsync())
            {
                var (blobName, blobContent) = await GetBlob(blob.Name);
                list.Add((blobName, blobContent));
            }

            return list.ToArray();
        }

        public static async Task AddBlob(string name, string content)
        {
            var blobClient = ContainerClient.GetBlobClient(name);

            var temporaryFile = Path.GetRandomFileName();
            var bytes = Encoding.UTF8.GetBytes(content);
            await File.WriteAllBytesAsync(temporaryFile, bytes);
            var stream = File.OpenRead(temporaryFile);
            try
            {
                await blobClient.UploadAsync(stream);
            }
            catch (RequestFailedException e) when (e.Status is 409)
            {
                Console.WriteLine($"Blob {name} already exists.");
            }

            stream.Close();
            File.Delete(temporaryFile);
        }

        public static async Task RemoveBlob(string name)
        {
            var blobClient = ContainerClient.GetBlobClient(name);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}