using System.Threading.Tasks;
using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.Infrastructure;
using BlogApp.UseCases.Adapters;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class AzurePostRepositoryTests
    {
        private readonly IBlogPostData _data = new BlogPostData(Constants.Title, Constants.Content);

        [Test]
        public async Task ShouldSaveAndReadPost()
        {
            // Arrange
            IPostRepository repository = new AzurePostRepository();

            try
            {
                // Act
                await repository.SavePost(_data);

                // Assert
                var data = await repository.GetPost(_data.Title);
                Check.That(data).IsEqualTo(_data);
            }
            finally // sort of local TearDown
            {
                // Cleanup
                await repository.DeletePost(_data);
            }
        }

        [Test]
        public async Task ShouldReadAllPosts()
        {
            // Arrange
            IPostRepository repository = new AzurePostRepository();

            // Act
            var posts = await repository.GetPosts();

            // Assert
            Check.That(posts).Not.IsEmpty();
            Check.That(posts).ContainsOnlyElementsThatMatch(post =>
                !string.IsNullOrWhiteSpace(post.Title) &&
                !string.IsNullOrWhiteSpace(post.Content));
        }
    }
}