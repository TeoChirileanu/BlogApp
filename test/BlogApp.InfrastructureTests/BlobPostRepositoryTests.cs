using System.Threading.Tasks;
using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.Infrastructure;
using BlogApp.UseCases.Adapters;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class BlobPostRepositoryTests
    {
        private IBlogPostData _data;

        [SetUp]
        public void Setup()
        {
            _data = new BlogPostData(Constants.Title, Constants.Content);
        }

        [Test]
        public async Task ShouldAddPost()
        {
            // Arrange
            IPostRepository repository = new BlobPostRepository();

            // Act
            await repository.AddPost(_data);

            // Assert
            var data = await repository.GetPost(_data.Title);
            Check.That(data).IsEqualTo(_data);

            // Clean
            await repository.RemovePost(_data.Title);
        }

        [Test]
        public async Task ShouldGetPost()
        {
            // Arrange
            IPostRepository repository = new BlobPostRepository();
            await repository.AddPost(_data);

            // Act
            var data = await repository.GetPost(_data.Title);

            // Assert
            Check.That(data).IsEqualTo(_data);

            // Clean
            await repository.RemovePost(_data.Title);
        }

        [Test]
        public async Task ShouldGetAllPosts()
        {
            // Arrange
            IPostRepository repository = new BlobPostRepository();
            const int howManyArticlesToAdd = 2;
            for (var i = 0; i < howManyArticlesToAdd; i++)
            {
                var post = new BlogPostData($"{_data.Title}-{i}", _data.Content);
                await repository.AddPost(post);
            }

            // Act
            var posts = await repository.GetPosts();

            // Assert
            Check.That(posts.Count).IsEqualTo(howManyArticlesToAdd);
            Check.That(posts).ContainsOnlyElementsThatMatch(post =>
                !string.IsNullOrWhiteSpace(post.Title) &&
                !string.IsNullOrWhiteSpace(post.Content));
        }

        [Test]
        public async Task ShouldRemovePost()
        {
            // Arrange
            IPostRepository repository = new BlobPostRepository();
            await repository.AddPost(_data);

            // Act
            await repository.RemovePost(_data.Title);

            // Assert
            var data = await repository.GetPost(_data.Title);
            Check.That(data.Title).IsNullOrWhiteSpace();
            Check.That(data.Content).IsNullOrWhiteSpace();
        }
    }
}