using BlogApp.BusinessRules.Data;
using BlogApp.BusinessRules.Procedures;

namespace BlogApp.BusinessRules.Entities
{
    public class BlogPostEntity
    {
        private readonly IBlogPostProcedures _procedures;

        public BlogPostEntity(IBlogPostProcedures procedures) => _procedures = procedures;


        public BlogPostData Add(BlogPostData data) => _procedures.Add(data);

        public BlogPostData Edit(BlogPostData data) => _procedures.Edit(data);

        public bool Remove(BlogPostData data) => _procedures.Remove(data);
    }
}
