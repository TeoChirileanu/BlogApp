using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IBlogPostData _post = new BlogPostData(Constants.Title, Constants.Content);

        [Test]
        public async Task ShouldPersistPost()
        {
            // Arrange
            var posts = new List<IBlogPostData>();
            IPostPersister postPersister = new InMemoryPostPersister(posts);

            // Act
            await postPersister.PersistPost(_post);

            // Assert
            Check.That(posts.Count).IsNotZero();
            Check.That(posts).ContainsExactly(_post);
        }

        [Test]
        public async Task ShouldGetPost()
        {
            // Arrange
            var posts = new List<IBlogPostData> {_post};
            IPostPersister postPersister = new InMemoryPostPersister(posts);

            // Act
            var post = await postPersister.GetPost(_post.Title);

            // Assert
            Check.That(post).IsEqualTo(_post);
        }

        [Test]
        public async Task ShouldGetAllPosts()
        {
            // Arrange
            const int howMany = 3;
            var somePosts = Enumerable.Range(0, howMany).Select(_ => _post);
            var posts = new List<IBlogPostData>(somePosts);
            IPostPersister postPersister = new InMemoryPostPersister(posts);

            // Act
            var allPosts = await postPersister.GetPosts();

            // Assert
            Check.That(allPosts).Not.IsEmpty();
            Check.That(allPosts.Count).IsEqualTo(howMany);
        }

        [Test]
        public async Task ShouldDeletePost()
        {
            // Arrange
            var posts = new List<IBlogPostData> {_post};
            IPostPersister postPersister = new InMemoryPostPersister(posts);

            // Act
            await postPersister.DeletePost(_post);

            // Assert
            Check.That(posts).IsEmpty();
        }
    }
}