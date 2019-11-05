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
        private IDataPersister _dataPersister;
        private IDataProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataGetter = Substitute.For<IDataGetter>();
            _dataPersister = Substitute.For<IDataPersister>();
            _dataProcessor = Substitute.For<IDataProcessor>();
            _dataDisplayer = Substitute.For<IDataDisplayer>();
            _dataGetter.GetData().Returns(_data);
            _dataProcessor.ProcessData(_data).Returns(_data);
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
            _dataProcessor.Received().ProcessData(_data);
        }

        [Test]
        public void ShouldPersistData()
        {
            // Arrange
            var dataManager = GetDataManager();

            // Act
            dataManager.PersistData(_data);

            // Assert
            _dataPersister.Received().PersistData(_data);
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
                _dataGetter, _dataProcessor, _dataPersister, _dataDisplayer);
        }
    }
}