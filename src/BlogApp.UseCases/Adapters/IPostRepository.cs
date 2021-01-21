using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IPostRepository
    {
        Task AddPost(IBlogPostData post);
        Task<IBlogPostData> GetPost(string title);
        Task<IList<IBlogPostData>> GetPosts();
        Task RemovePost(string title);
    }
}