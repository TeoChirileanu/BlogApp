using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IDataPersister
    {
        void PersistData(IBlogPostData data);
        IBlogPostData GetData(string title);
    }
}