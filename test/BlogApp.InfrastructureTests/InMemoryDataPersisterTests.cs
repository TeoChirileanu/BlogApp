using System.Collections.ObjectModel;
using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.Infrastructure;
using BlogApp.UseCases.Adapters;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class InMemoryDataPersisterTests
    {
        private readonly BlogPostData _data = new BlogPostData(Constants.Title, Constants.Content);

        [Test]
        public void ShouldPersistData()
        {
            // Arrange
            var blogPostData = new Collection<IBlogPostData>();
            IDataPersister dataPersister = new InMemoryDataPersister(blogPostData);

            // Act
            dataPersister.PersistData(_data);

            // Assert
            Check.That(blogPostData.Count).IsNotZero();
            Check.That(blogPostData).ContainsExactly(_data);
        }

        [Test]
        public void ShouldGetData()
        {
            // Arrange
            var blogPostData = new Collection<IBlogPostData> {_data};
            IDataPersister dataPersister = new InMemoryDataPersister(blogPostData);

            // Act
            var persistedData = dataPersister.GetData(_data.Title);

            // Assert
            Check.That(persistedData).IsEqualTo(_data);
        }
    }
}