using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.Infrastructure
{
    public class InMemoryPostRepository : IPostRepository
    {
        private readonly IList<IBlogPostData> _blogPostData;

        public InMemoryPostRepository(IList<IBlogPostData> blogPostData = null)
        {
            _blogPostData = blogPostData ?? new List<IBlogPostData>();
        }

        public async Task SavePost(IBlogPostData post)
        {
            _blogPostData.Add(post);
            await Task.CompletedTask;
        }

        public Task<IBlogPostData> GetPost(string title)
        {
            var post = _blogPostData.FirstOrDefault(data => data.Title == title);
            return Task.FromResult(post);
        }

        public Task<IList<IBlogPostData>> GetPosts()
        {
            return Task.FromResult(_blogPostData);
        }

        public async Task DeletePost(IBlogPostData post)
        {
            _blogPostData.Remove(post);
            await Task.CompletedTask;
        }
    }
}