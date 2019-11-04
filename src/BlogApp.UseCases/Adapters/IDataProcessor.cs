using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IDataProcessor
    {
        BlogPostData ProcessData(BlogPostData data);
    }
}