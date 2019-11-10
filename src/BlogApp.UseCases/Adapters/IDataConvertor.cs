using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IDataConvertor
    {
        IBlogPostData ConvertData(IBlogPostData data);
    }
}