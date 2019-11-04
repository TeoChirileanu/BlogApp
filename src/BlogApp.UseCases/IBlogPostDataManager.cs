using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases
{
    public interface IBlogPostDataManager
    {
        BlogPostData GetData();
        object ProcessData(BlogPostData data);
        void PersistData(BlogPostData data);
        void DisplayData(BlogPostData data);
    }
}