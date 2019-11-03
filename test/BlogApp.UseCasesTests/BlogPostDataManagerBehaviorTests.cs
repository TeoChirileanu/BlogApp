using BlogApp.BusinessRules.Data;
using BlogApp.UseCases;
using BlogApp.UseCases.Adapters;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace BlogApp.UseCasesTests
{
    public class BlogPostDataManagerBehaviorTests
    {
        private readonly IDataGetter _dataGetter = Substitute.For<IDataGetter>();
        private readonly IDataProcessor _dataProcessor = Substitute.For<IDataProcessor>();
        private readonly IDataPersister _dataPersister = Substitute.For<IDataPersister>();
        private readonly IDataDisplayer _dataDisplayer = Substitute.For<IDataDisplayer>();
        private static readonly BlogPostData Data = new BlogPostData();

        [SetUp]
        public void Setup()
        {
            _dataGetter.GetData().Returns(Data);
            _dataProcessor.ProcessData(Data).Returns(new object());
        }

        [Test]
        public void ShouldGetData()
        {
            // Arrange
            var dataManager = GetDataManager();

            // Act
            var data = dataManager.GetData();

            // Assert
            Check.That(data).IsEqualTo(Data);
            _dataGetter.Received().GetData();
        }

        [Test]
        public void ShouldProcessData()
        {
            // Arrange
            var dataManager = GetDataManager();

            // Act
            var processedData = dataManager.ProcessData(Data);

            // Assert
            Check.That(processedData).IsNotNull();
            _dataProcessor.Received().ProcessData(Data);
        }

        [Test]
        public void ShouldPersistData()
        {
            // Arrange
            var dataManager = GetDataManager();

            // Act
            dataManager.PersistData(Data);

            // Assert
            _dataPersister.Received().PersistData(Data);
        }

        [Test]
        public void ShouldDisplayData()
        {
            // Arrange
            var dataManager = GetDataManager();

            // Act
            dataManager.DisplayData(Data);

            // Assert
            _dataDisplayer.Received().DisplayData(Data);
        }

        private IBlogPostDataManager GetDataManager()
        {
            return new BlogPostDataManager(
                _dataGetter, _dataProcessor, _dataPersister, _dataDisplayer);
        }
    }
}