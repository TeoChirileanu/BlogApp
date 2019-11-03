using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IDataGetter
    {
        BlogPostData GetData();
    }
}