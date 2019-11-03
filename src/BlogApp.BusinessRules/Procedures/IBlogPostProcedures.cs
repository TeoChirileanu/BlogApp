using BlogApp.BusinessRules.Data;

namespace BlogApp.BusinessRules.Procedures
{
    public interface IBlogPostProcedures
    {
        BlogPostData Add(BlogPostData data);
        BlogPostData Edit(BlogPostData data);
        bool Remove(BlogPostData data);
    }
}
