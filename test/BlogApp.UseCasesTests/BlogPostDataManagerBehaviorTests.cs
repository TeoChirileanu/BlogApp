using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.UseCases;
using BlogApp.UseCases.Adapters;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace BlogApp.UseCasesTests
{
    public class BlogPostDataManagerBehaviorTests
    {
        private readonly IBlogPostData _data = new BlogPostData(Constants.Title, Constants.Content);
        private IDataDisplayer _dataDisplayer;
        private IDataGetter _dataGetter;
        private IDataConvertor _dataConvertor;
        private IPostRepository _postRepository;

        [SetUp]
        public void Setup()
        {
            _dataGetter = Substitute.For<IDataGetter>();
            _postRepository = Substitute.For<IPostRepository>();
            _dataConvertor = Substitute.For<IDataConvertor>();
            _dataDisplayer = Substitute.For<IDataDisplayer>();
            _dataGetter.GetData().Returns(_data);
            _dataConvertor.ConvertData(_data).Returns(_data);
        }

        [Test]
        public void ShouldGetData()
        {
            // Arrange
            var dataManager = GetDataManager();

            // Act
            var data = dataManager.GetData();

            // Assert
            Check.That(data).IsEqualTo(_data);
            _dataGetter.Received().GetData();
        }

        [Test]
        public void ShouldProcessData()
        {
            // Arrange
            var dataManager = GetDataManager();

            // Act
            var processedData = dataManager.ProcessData(_data);

            // Assert
            Check.That(processedData).IsNotNull();
            _dataConvertor.Received().ConvertData(_data);
        }

        [Test]
        public void ShouldPersistData()
        {
            // Arrange
            var dataManager = GetDataManager();

            // Act
            dataManager.PersistData(_data);

            // Assert
            _postRepository.Received().SavePost(_data);
        }

        [Test]
        public void ShouldDisplayData()
        {
            // Arrange
            var dataManager = GetDataManager();

            // Act
            dataManager.DisplayData(_data);

            // Assert
            _dataDisplayer.Received().DisplayData(_data);
        }

        private IBlogPostDataManager GetDataManager()
        {
            return new BlogPostDataManager(
                _dataGetter, _dataConvertor, _postRepository, _dataDisplayer);
        }
    }
}