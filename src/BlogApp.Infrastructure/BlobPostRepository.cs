using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.UseCases.Adapters;

namespace BlogApp.Infrastructure
{
    public class BlobPostRepository : IPostRepository
    {
        public BlobPostRepository()
        {
            var serviceClient = new BlobServiceClient(Constants.ConnectionString);
            serviceClient.GetBlobContainerClient(Constants.Container);
        }

        public async Task AddPost(IBlogPostData post)
        {
            await BlobService.AddBlob(post.Title, post.Content);
        }

        public async Task<IBlogPostData> GetPost(string title)
        {
            var (name, content) = await BlobService.GetBlob(title);
            var post = new BlogPostData(name, content);
            return post;
        }

        public async Task<IList<IBlogPostData>> GetPosts()
        {
            IList<IBlogPostData> posts = new List<IBlogPostData>();
            foreach (var (blobName, blobContent) in await BlobService.GetAllBlobs())
                posts.Add(new BlogPostData(blobName, blobContent));
            return posts;
        }

        public async Task RemovePost(string title)
        {
            await BlobService.RemoveBlob(title);
        }
    }
}