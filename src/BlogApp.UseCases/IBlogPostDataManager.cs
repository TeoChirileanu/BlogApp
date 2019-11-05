using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases
{
    public interface IBlogPostDataManager
    {
        BlogPostData GetData();
        object ProcessData(IBlogPostData data);
        void PersistData(IBlogPostData data);
        void DisplayData(IBlogPostData data);
    }
}