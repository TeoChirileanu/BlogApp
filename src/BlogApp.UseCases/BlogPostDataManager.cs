using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.UseCases
{
    public class BlogPostDataManager : IBlogPostDataManager
    {
        private readonly IDataDisplayer _dataDisplayer;
        private readonly IDataGetter _dataGetter;
        private readonly IPostPersister _postPersister;
        private readonly IDataProcessor _dataProcessor;

        public BlogPostDataManager(IDataGetter dataGetter, IDataProcessor dataProcessor, IPostPersister postPersister,
            IDataDisplayer dataDisplayer)
        {
            _dataGetter = dataGetter;
            _dataProcessor = dataProcessor;
            _postPersister = postPersister;
            _dataDisplayer = dataDisplayer;
        }

        public IBlogPostData GetData()
        {
            return _dataGetter.GetData();
        }

        public object ProcessData(IBlogPostData data)
        {
            return _dataProcessor.ProcessData(data);
        }

        public void PersistData(IBlogPostData data)
        {
            _postPersister.PersistPost(data);
        }

        public void DisplayData(IBlogPostData data)
        {
            _dataDisplayer.DisplayData(data);
        }
    }
}