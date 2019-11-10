using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.UseCases
{
    public class BlogPostDataManager : IBlogPostDataManager
    {
        private readonly IDataDisplayer _dataDisplayer;
        private readonly IDataGetter _dataGetter;
        private readonly IDataConvertor _dataConvertor;
        private readonly IPostPersister _postPersister;

        public BlogPostDataManager(IDataGetter dataGetter, IDataConvertor dataConvertor, IPostPersister postPersister,
            IDataDisplayer dataDisplayer)
        {
            _dataGetter = dataGetter;
            _dataConvertor = dataConvertor;
            _postPersister = postPersister;
            _dataDisplayer = dataDisplayer;
        }

        public IBlogPostData GetData()
        {
            return _dataGetter.GetData();
        }

        public object ProcessData(IBlogPostData data)
        {
            return _dataConvertor.ConvertData(data);
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