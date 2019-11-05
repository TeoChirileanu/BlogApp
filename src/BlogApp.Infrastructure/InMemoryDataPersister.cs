using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.Infrastructure
{
    public class InMemoryDataPersister : IDataPersister
    {
        private readonly ICollection<IBlogPostData> _blogPostData;

        public InMemoryDataPersister(ICollection<IBlogPostData> blogPostData = null)
        {
            _blogPostData = blogPostData ?? new Collection<IBlogPostData>();
        }

        public void PersistData(IBlogPostData data)
        {
            _blogPostData.Add(data);
        }

        public IBlogPostData GetData(string title)
        {
            return _blogPostData.FirstOrDefault(data => data.Title == title);
        }
    }
}