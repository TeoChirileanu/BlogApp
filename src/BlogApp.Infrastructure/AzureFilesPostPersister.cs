using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.UseCases.Adapters;

namespace BlogApp.Infrastructure
{
    public class AzureFilesPostPersister : IPostPersister
    {
        private readonly BlobContainerClient _containerClient;

        public AzureFilesPostPersister()
        {
            var serviceClient = new BlobServiceClient(Constants.ConnectionString);
            _containerClient = serviceClient.GetBlobContainerClient(Constants.Container);
        }

        public async Task PersistPost(IBlogPostData post)
        {
            var title = post.Title;
            var content = post.Content;
            await SavePost(title, content);
        }

        public async Task<IBlogPostData> GetPost(string title)
        {
            var content = await GetContent(title);
            var post = new BlogPostData(title, content);
            return post;
        }

        public async Task<IList<IBlogPostData>> GetPosts()
        {
            IList<IBlogPostData> posts = new List<IBlogPostData>();
            await foreach (var blob in _containerClient.GetBlobsAsync())
            {
                var title = blob.Name;
                var content = await GetContent(title);
                var post = new BlogPostData(title, content);
                posts.Add(post);
            }

            return posts;
        }

        public async Task DeletePost(IBlogPostData post)
        {
            var title = post.Title;
            var blobClient = _containerClient.GetBlobClient(title);
            await blobClient.DeleteIfExistsAsync();
        }

        private async Task<string> GetContent(string title)
        {
            var blobClient = _containerClient.GetBlobClient(title);
            await using var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);
            var content = Encoding.ASCII.GetString(stream.ToArray());
            return content;
        }

        private async Task SavePost(string title, string content)
        {
            var blobClient = _containerClient.GetBlobClient(title);
            var temporaryFile = Path.GetRandomFileName();
            // warning: does not work with memory stream
            await File.WriteAllTextAsync(temporaryFile, content);
            await using var stream = File.OpenRead(temporaryFile);
            await blobClient.UploadAsync(stream);
        }
    }
}