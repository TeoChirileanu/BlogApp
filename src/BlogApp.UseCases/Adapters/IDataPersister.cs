using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IDataPersister
    {
        void PersistData(BlogPostData data);
    }
}