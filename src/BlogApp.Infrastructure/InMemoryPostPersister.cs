using System.Collections.Generic;
using System.Linq;
using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.Infrastructure
{
    public class InMemoryPostPersister : IPostPersister
    {
        private readonly IList<IBlogPostData> _blogPostData;

        public InMemoryPostPersister(IList<IBlogPostData> blogPostData = null)
        {
            _blogPostData = blogPostData ?? new List<IBlogPostData>();
        }

        public void PersistPost(IBlogPostData data)
        {
            _blogPostData.Add(data);
        }

        public IBlogPostData GetPost(string title)
        {
            return _blogPostData.FirstOrDefault(data => data.Title == title);
        }

        public IList<IBlogPostData> GetPosts()
        {
            return _blogPostData;
        }
    }
}