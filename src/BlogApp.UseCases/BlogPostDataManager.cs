using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.UseCases
{
    public class BlogPostDataManager : IBlogPostDataManager
    {
        private readonly IDataDisplayer _dataDisplayer;
        private readonly IDataGetter _dataGetter;
        private readonly IDataPersister _dataPersister;
        private readonly IDataProcessor _dataProcessor;

        public BlogPostDataManager(IDataGetter dataGetter, IDataProcessor dataProcessor, IDataPersister dataPersister,
            IDataDisplayer dataDisplayer)
        {
            _dataGetter = dataGetter;
            _dataProcessor = dataProcessor;
            _dataPersister = dataPersister;
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
            _dataPersister.PersistData(data);
        }

        public void DisplayData(IBlogPostData data)
        {
            _dataDisplayer.DisplayData(data);
        }
    }
}