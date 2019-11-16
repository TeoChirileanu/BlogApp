using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using BlogApp.Common;

namespace BlogApp.Infrastructure
{
    public class BlobRepository
    {
        private readonly BlobContainerClient _containerClient;

        public BlobRepository()
        {
            var serviceClient = new BlobServiceClient(Constants.ConnectionString);
            _containerClient = serviceClient.GetBlobContainerClient(Constants.Container);
        }

        public async Task<string> GetBlobContent(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await using var memoryStream = new MemoryStream();
            try
            {
                await blobClient.DownloadToAsync(memoryStream);
            }
            catch (RequestFailedException e) when (e.Status is 404)
            {
                Console.WriteLine($"Blob {blobName} does not exist.");
                return null;
            }

            var content = Encoding.UTF8.GetString(memoryStream.ToArray());
            return content;
        }

        public async IAsyncEnumerable<(string blobName, string blobContent)> GetAllBlobs()
        {
            await foreach (var blob in _containerClient.GetBlobsAsync())
            {
                var blobName = blob.Name;
                var blobContent = await GetBlobContent(blobName);
                yield return (blobName, blobContent);
            }
        }

        public async Task AddBlob(string name, string content)
        {
            var blobClient = _containerClient.GetBlobClient(name);

            var temporaryFile = Path.GetRandomFileName();
            var bytes = Encoding.UTF8.GetBytes(content);
            await File.WriteAllBytesAsync(temporaryFile, bytes);
            var stream = File.OpenRead(temporaryFile);
            await blobClient.UploadAsync(stream);
            stream.Close();
            File.Delete(temporaryFile);
        }

        public async Task RemoveBlob(string name)
        {
            var blobClient = _containerClient.GetBlobClient(name);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}