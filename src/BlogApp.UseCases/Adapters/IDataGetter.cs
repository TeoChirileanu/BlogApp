using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IDataGetter
    {
        IBlogPostData GetData();
    }
}