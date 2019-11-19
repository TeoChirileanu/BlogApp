using System.Threading.Tasks;
using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.Infrastructure;
using BlogApp.UseCases.Adapters;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class FilePostRepositoryTests
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
            IPostRepository repository = new FilePostRepository();

            try
            {
                // Act
                await repository.AddPost(_data);

                // Assert
                var data = await repository.GetPost(_data.Title);
                Check.That(data.Title == _data.Title);
                Check.That(data.Content == _data.Content);
                Check.That(data).Equals(_data);
            }
            finally
            {
                // Clean
                await repository.RemovePost(_data.Title);
            }
        }

        [Test]
        public async Task ShouldGetPost()
        {
            // Arrange
            IPostRepository repository = new FilePostRepository();
            try
            {
                await repository.AddPost(_data);

                // Act
                var data = await repository.GetPost(_data.Title);

                // Assert
                Check.That(data.Title == _data.Title);
                Check.That(data.Content == _data.Content);
            }
            finally
            {
                // Clean
                await repository.RemovePost(_data.Title);
            }
        }

        [TestCase(2)]
        public async Task ShouldGetAllPosts(int howManyArticlesToAdd)
        {
            // Arrange
            IPostRepository repository = new FilePostRepository();
            try
            {
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
            finally
            {
                // Clean
                await FileService.RemoveAllFiles();
            }
        }

        [Test]
        public async Task ShouldRemovePost()
        {
            // Arrange
            IPostRepository repository = new FilePostRepository();
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