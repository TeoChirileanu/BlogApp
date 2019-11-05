using BlogApp.BusinessRules.Data;
using BlogApp.BusinessRules.Procedures;

namespace BlogApp.BusinessRules.Entities
{
    public sealed class BlogPostEntity
    {
        private readonly IBlogPostProcedures _procedures;

        public BlogPostEntity(IBlogPostProcedures procedures)
        {
            _procedures = procedures;
        }


        public BlogPostData Add(IBlogPostData data)
        {
            return _procedures.Add(data);
        }

        public BlogPostData Edit(IBlogPostData data)
        {
            return _procedures.Edit(data);
        }

        public bool Remove(IBlogPostData data)
        {
            return _procedures.Remove(data);
        }
    }
}