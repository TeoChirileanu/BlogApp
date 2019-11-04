using BlogApp.BusinessRules.Data;
using BlogApp.BusinessRules.Entities;
using BlogApp.BusinessRules.Procedures;
using BlogApp.Common;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace BlogApp.BusinessRulesTests
{
    public class BlogPostEntityBehaviorTests
    {
        private readonly BlogPostData _data = new BlogPostData(Constants.Title, Constants.Content);
        private readonly IBlogPostProcedures _procedures = Substitute.For<IBlogPostProcedures>();

        [SetUp]
        public void SetUp()
        {
            _procedures.Add(_data).Returns(_data);
            _procedures.Edit(_data).Returns(_data);
            _procedures.Remove(_data).Returns(true);
        }

        [Test]
        public void ShouldAddBlogPost()
        {
            // Arrange
            var entity = new BlogPostEntity(_procedures);

            // Act
            var addedData = entity.Add(_data);

            // Assert
            Check.That(addedData).IsNotNull().And.IsEqualTo(_data);
            Check.That(_procedures.Received().Add(_data));
        }

        [Test]
        public void ShouldEditBlogPost()
        {
            // Arrange
            var entity = new BlogPostEntity(_procedures);

            // Act
            var addedData = entity.Edit(_data);

            // Assert
            Check.That(addedData).IsNotNull().And.IsEqualTo(_data);
            Check.That(_procedures.Received().Edit(_data));
        }

        [Test]
        public void ShouldRemoveBlogPost()
        {
            // Arrange
            var entity = new BlogPostEntity(_procedures);

            // Act
            var dataRemoved = entity.Remove(_data);

            // Assert
            Check.That(dataRemoved).IsTrue();
            Check.That(_procedures.Received().Remove(_data));
        }
    }
}