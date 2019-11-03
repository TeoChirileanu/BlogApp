using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IDataProcessor
    {
        dynamic ProcessData(BlogPostData data);
    }
}