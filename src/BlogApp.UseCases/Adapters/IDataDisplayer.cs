using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IDataDisplayer
    {
        void DisplayData(IBlogPostData data);
    }
}