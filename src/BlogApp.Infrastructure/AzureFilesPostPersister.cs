using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public void PersistPost(IBlogPostData data)
        {
            //var file = _containerClient.GetFileReference(data.Title);
            //file.UploadText(data.Content);
        }

        public IBlogPostData GetPost(string title)
        {
            //var file = _containerClient.GetFileReference(title);
            //var content = file.DownloadText();
            //var result = new BlogPostData(title, content);
            //return result;
            return null;
        }

        public IList<IBlogPostData> GetPosts()
        {
            IList<IBlogPostData> posts = new List<IBlogPostData>();
            foreach (var blob in _containerClient.GetBlobs())
            {
                var title = blob.Name;
                var blobClient = _containerClient.GetBlobClient(title);
                var stream = new MemoryStream();
                blobClient.DownloadTo(stream);
                var content = Encoding.ASCII.GetString(stream.ToArray());
                var post = new BlogPostData(title, content);
                posts.Add(post);
            }

            return posts;
        }
    }
}