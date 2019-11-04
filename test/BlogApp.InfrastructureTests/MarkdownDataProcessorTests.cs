using BlogApp.BusinessRules.Data;
using BlogApp.Common;
using BlogApp.Infrastructure;
using BlogApp.UseCases.Adapters;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class MarkdownDataProcessorTests
    {
        [Test]
        public void ShouldConvertMarkdownToHtml()
        {
            // Arrange
            IDataProcessor dataProcessor = new MarkdownDataProcessor();

            // Act
            var originalData = new BlogPostData(Constants.Title, Constants.MarkdownContent);
            var processedData = dataProcessor.ProcessData(originalData);

            // Assert
            Check.That(processedData.Title).IsEqualTo(originalData.Title);
            Check.That(processedData.Content.Equals(Constants.HtmlContent));
        }
    }
}