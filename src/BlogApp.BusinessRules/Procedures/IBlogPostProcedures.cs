using BlogApp.BusinessRules.Data;

namespace BlogApp.BusinessRules.Procedures
{
    public interface IBlogPostProcedures
    {
        BlogPostData Add(IBlogPostData data);
        BlogPostData Edit(IBlogPostData data);
        bool Remove(IBlogPostData data);
    }
}