using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Files;
using Azure.Storage.Files.Models;
using BlogApp.Common;
using ByteSizeLib;

namespace BlogApp.Infrastructure
{
    public static class FileService
    {
        private static readonly long MaxSize =
            (long) ByteSize.FromMegaBytes(1.0d).Bytes;

        private static readonly FileServiceClient FileServiceClient =
            new FileServiceClient(Constants.ConnectionString);

        private static readonly ShareClient ShareClient =
            FileServiceClient.GetShareClient(Constants.Share);

        // todo: find a way to mock this
        private static readonly DirectoryClient DirectoryClient =
            ShareClient.GetDirectoryClient(Constants.Directory);

        public static async Task AddFile((string name, string content) file)
        {
            var (name, content) = file;
            var fileClient = DirectoryClient.GetFileClient(name);
            await fileClient.CreateAsync(MaxSize); // overrides if exists
            var temporaryFile = Path.GetTempFileName();
            await File.WriteAllTextAsync(temporaryFile, content);
            await using var stream = File.OpenRead(temporaryFile);
            await fileClient.UploadAsync(stream);
            stream.Close();
            File.Delete(temporaryFile);
        }

        public static async Task<(string fileName, string fileContent)> GetFile(string fileName)
        {
            var fileClient = DirectoryClient.GetFileClient(fileName);
            Stream stream;
            long contentLength;
            try
            {
                var response = await fileClient.DownloadAsync();
                stream = response.Value.Content;
                contentLength = response.Value.ContentLength;
            }
            catch (StorageRequestFailedException e)
                when (e.ErrorCode == FileErrorCode.ResourceNotFound)
            {
                Console.WriteLine($"File {fileName} does not exist");
                return (null, null);
            }

            var memory = new Memory<byte>(new byte[contentLength]);
            await stream.ReadAsync(memory);
            var bytes = memory.ToArray();
            var fileContent = Encoding.ASCII.GetString(bytes);
            var trimmedContent = fileContent.Trim('\0');
            return (fileName, trimmedContent);
        }

        public static async Task<IList<(string, string)>> GetAllFiles()
        {
            var result = new List<(string, string)>();
            await foreach (var item in DirectoryClient.GetFilesAndDirectoriesAsync())
            {
                var file = await GetFile(item.Name);
                result.Add(file);
            }

            return result;
        }

        public static async Task RemoveAllFiles()
        {
            await foreach (var item in DirectoryClient.GetFilesAndDirectoriesAsync())
            {
                if (item.IsDirectory) continue;
                await RemoveFile(item.Name);
            }
        }

        public static Task RemoveFile(string fileName)
        {
            var fileClient = DirectoryClient.GetFileClient(fileName);
            return fileClient.DeleteAsync();
        }
    }
}