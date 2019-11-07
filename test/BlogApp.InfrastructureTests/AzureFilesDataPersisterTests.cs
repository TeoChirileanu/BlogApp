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
        public void ShouldPersistAndReadPost()
        {
            // Arrange
            IPostPersister persister = new AzureFilesPostPersister();

            // Act
            persister.PersistPost(_data);

            // Assert
            var data = persister.GetPost(_data.Title);
            Check.That(data).IsEqualTo(_data);
        }

        [Test]
        public void ShouldReadAllPosts()
        {
            // Arrange
            IPostPersister persister = new AzureFilesPostPersister();

            // Act
            var posts = persister.GetPosts();

            // Assert
            Check.That(posts).Not.IsEmpty();
            Check.That(posts).ContainsOnlyElementsThatMatch(post =>
                !string.IsNullOrWhiteSpace(post.Title) &&
                !string.IsNullOrWhiteSpace(post.Content));
        }
    }
}