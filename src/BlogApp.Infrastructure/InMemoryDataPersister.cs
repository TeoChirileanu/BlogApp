using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.Infrastructure
{
    public class InMemoryDataPersister : IDataPersister
    {
        private readonly ICollection<BlogPostData> _blogPostData;

        public InMemoryDataPersister(ICollection<BlogPostData> blogPostData = null)
        {
            _blogPostData = blogPostData ?? new Collection<BlogPostData>();
        }

        public void PersistData(BlogPostData data)
        {
            _blogPostData.Add(data);
        }

        public BlogPostData GetData(string title)
        {
            return _blogPostData.FirstOrDefault(data => data.Title == title);
        }
    }
}