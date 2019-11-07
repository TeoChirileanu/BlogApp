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
        private readonly IBlogPostData _data = new BlogPostData(Constants.Title, Constants.Content);

        [Test]
        public void ShouldPersistData()
        {
            // Arrange
            var blogPostData = new Collection<IBlogPostData>();
            IPostPersister postPersister = new InMemoryPostPersister(blogPostData);

            // Act
            postPersister.PersistPost(_data);

            // Assert
            Check.That(blogPostData.Count).IsNotZero();
            Check.That(blogPostData).ContainsExactly(_data);
        }

        [Test]
        public void ShouldGetData()
        {
            // Arrange
            var blogPostData = new Collection<IBlogPostData> {_data};
            IPostPersister postPersister = new InMemoryPostPersister(blogPostData);

            // Act
            var persistedData = postPersister.GetPost(_data.Title);

            // Assert
            Check.That(persistedData).IsEqualTo(_data);
        }
    }
}