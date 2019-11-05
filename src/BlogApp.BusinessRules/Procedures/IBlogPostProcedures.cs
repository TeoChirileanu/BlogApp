using BlogApp.BusinessRules.Data;

namespace BlogApp.BusinessRules.Procedures
{
    public interface IBlogPostProcedures
    {
        IBlogPostData Add(IBlogPostData data);
        IBlogPostData Edit(IBlogPostData data);
        bool Remove(IBlogPostData data);
    }
}