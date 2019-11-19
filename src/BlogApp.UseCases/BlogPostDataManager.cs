using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.UseCases
{
    public class BlogPostDataManager : IBlogPostDataManager
    {
        private readonly IDataConvertor _dataConvertor;
        private readonly IDataDisplayer _dataDisplayer;
        private readonly IDataGetter _dataGetter;
        private readonly IPostRepository _postRepository;

        public BlogPostDataManager(IDataGetter dataGetter, IDataConvertor dataConvertor, IPostRepository postRepository,
            IDataDisplayer dataDisplayer)
        {
            _dataGetter = dataGetter;
            _dataConvertor = dataConvertor;
            _postRepository = postRepository;
            _dataDisplayer = dataDisplayer;
        }

        public IBlogPostData GetData()
        {
            return _dataGetter.GetData();
        }

        public object ProcessData(IBlogPostData data)
        {
            return _dataConvertor.ConvertMarkdownToHtml(data);
        }

        public void PersistData(IBlogPostData data)
        {
            _postRepository.AddPost(data);
        }

        public void DisplayData(IBlogPostData data)
        {
            _dataDisplayer.DisplayData(data);
        }
    }
}