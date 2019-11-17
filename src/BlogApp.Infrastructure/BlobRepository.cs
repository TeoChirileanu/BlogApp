using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Azure;
using Azure.Storage.Blobs;
using BlogApp.Common;

namespace BlogApp.Infrastructure
{
    public static class BlobRepository
    {
        private static readonly BlobServiceClient ServiceClient =
            new BlobServiceClient(Constants.ConnectionString);

        private static readonly BlobContainerClient ContainerClient =
            ServiceClient.GetBlobContainerClient(Constants.Container);


        public static string GetBlobContent(string blobName)
        {
            var blobClient = ContainerClient.GetBlobClient(blobName);
            using var memoryStream = new MemoryStream();
            try
            {
                blobClient.DownloadTo(memoryStream);
            }
            catch (RequestFailedException e) when (e.Status is 404)
            {
                Console.WriteLine($"Blob {blobName} does not exist.");
                return null;
            }

            var content = Encoding.UTF8.GetString(memoryStream.ToArray());
            return content;
        }

        public static (string blobName, string blobContent)[] GetAllBlobs()
        {
            var list = new List<(string blobName, string blobContent)>();
            foreach (var blob in ContainerClient.GetBlobs())
            {
                var blobName = blob.Name;
                var blobContent = GetBlobContent(blobName);
                list.Add((blobName, blobContent));
            }

            return list.ToArray();
        }

        public static void AddBlob(string name, string content)
        {
            var blobClient = ContainerClient.GetBlobClient(name);

            var temporaryFile = Path.GetRandomFileName();
            var bytes = Encoding.UTF8.GetBytes(content);
            File.WriteAllBytes(temporaryFile, bytes);
            var stream = File.OpenRead(temporaryFile);
            try
            {
                blobClient.Upload(stream);
            }
            catch (RequestFailedException e) when (e.ErrorCode is "BlobAlreadyExists")
            {
                Console.WriteLine($"Blob {name} already exists.");
            }
            stream.Close();
            File.Delete(temporaryFile);
        }

        public static void RemoveBlob(string name)
        {
            var blobClient = ContainerClient.GetBlobClient(name);
            blobClient.DeleteIfExists();
        }
    }
}