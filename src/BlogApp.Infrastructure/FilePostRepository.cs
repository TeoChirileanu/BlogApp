using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.Infrastructure
{
    public class FilePostRepository : IPostRepository
    {
        public async Task AddPost(IBlogPostData post)
        {
            try
            {
                await FileService.AddFile((post.Title, post.Content));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public async Task<IBlogPostData> GetPost(string title)
        {
            IBlogPostData result = null;
            try
            {
                var (name, content) = await FileService.GetFile(title);
                result = new BlogPostData(name, content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return result;
        }

        public async Task<IList<IBlogPostData>> GetPosts()
        {
            var result = new List<IBlogPostData>();
            try
            {
                var files = await FileService.GetAllFiles();
                var posts = files
                    .Select(file => new BlogPostData(file.Item1, file.Item2));
                result.AddRange(posts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return result;
        }

        public async Task RemovePost(string title)
        {
            try
            {
                await FileService.RemoveFile(title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}