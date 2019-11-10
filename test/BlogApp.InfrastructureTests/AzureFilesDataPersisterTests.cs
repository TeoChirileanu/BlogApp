using System.Threading.Tasks;
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
        public async Task ShouldPersistAndReadPost()
        {
            // Arrange
            IPostPersister persister = new AzureFilesPostPersister();

            try
            {
                // Act
                await persister.PersistPost(_data);

                // Assert
                var data = await persister.GetPost(_data.Title);
                Check.That(data).IsEqualTo(_data);
            }
            finally // sort of local TearDown
            {
                // Cleanup
                await persister.DeletePost(_data);
            }
        }

        [Test]
        public async Task ShouldReadAllPosts()
        {
            // Arrange
            IPostPersister persister = new AzureFilesPostPersister();

            // Act
            var posts = await persister.GetPosts();

            // Assert
            Check.That(posts).Not.IsEmpty();
            Check.That(posts).ContainsOnlyElementsThatMatch(post =>
                !string.IsNullOrWhiteSpace(post.Title) &&
                !string.IsNullOrWhiteSpace(post.Content));
        }
    }
}