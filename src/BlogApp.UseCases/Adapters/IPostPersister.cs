using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IPostPersister
    {
        Task PersistPost(IBlogPostData post);
        Task<IBlogPostData> GetPost(string title);
        Task<IList<IBlogPostData>> GetPosts();
        Task DeletePost(IBlogPostData post);
    }
}