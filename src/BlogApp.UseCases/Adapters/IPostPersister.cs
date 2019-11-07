using System.Collections.Generic;
using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IPostPersister
    {
        void PersistPost(IBlogPostData data);
        IBlogPostData GetPost(string title);
        IList<IBlogPostData> GetPosts();
    }
}