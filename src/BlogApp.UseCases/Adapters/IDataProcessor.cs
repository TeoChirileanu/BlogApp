using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IDataProcessor
    {
        IBlogPostData ProcessData(IBlogPostData data);
    }
}