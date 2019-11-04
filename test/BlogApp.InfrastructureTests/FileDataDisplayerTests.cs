using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.Infrastructure;
using BlogApp.UseCases.Adapters;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class FileDataDisplayerTests
    {
        private const string OutputFile = Constants.OutputFile;
        private const string Title = Constants.Title;
        private const string Content = Constants.Content;

        [Test]
        public void ShouldDisplayDataInAFile()
        {
            // Arrange
            IDataDisplayer dataDisplayer = new FileDataDisplayer(OutputFile);

            // Act
            var data = new BlogPostData(Title, Content);
            dataDisplayer.DisplayData(data);

            // Assert
            var lines = Helpers.ReadLines(OutputFile);
            Check.That(lines.Length).IsEqualTo(2);
            Check.That(lines[0]).IsEqualTo(Title);
            Check.That(lines[1]).IsEqualTo(Content);
        }

        [TearDown]
        public void TearDown()
        {
            Helpers.DeleteFile(OutputFile);
        }
    }
}