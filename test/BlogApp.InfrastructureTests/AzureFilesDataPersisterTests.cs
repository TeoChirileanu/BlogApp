using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.Infrastructure;
using BlogApp.UseCases.Adapters;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class AzureFilesDataPersisterTests
    {
        private readonly IBlogPostData _data = new BlogPostData(Constants.Title, Constants.Content);

        [Test]
        public void ShouldPersistData()
        {
            // Arrange
            IDataPersister dataPersister = new AzureFilesDataPersister();

            // Act
            dataPersister.PersistData(_data);

            // Assert
            var data = dataPersister.GetData(_data.Title);
            Check.That(data).IsNotNull();
            Check.That(data).IsEqualTo(_data);
        }

        [Test]
        public void ShouldReadPersistedData()
        {
            // Arrange
            IDataPersister dataPersister = new AzureFilesDataPersister();

            // Act
            var data = dataPersister.GetData(_data.Title);

            // Assert
            Check.That(data).IsNotNull();
            Check.That(data).IsEqualTo(_data);
        }
    }
}