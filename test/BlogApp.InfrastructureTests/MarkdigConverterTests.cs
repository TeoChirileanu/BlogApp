using BlogApp.Infrastructure;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class MarkdigConverterTests
    {
        [Test]
        public void ShouldConvertToHtml()
        {
            // Arrange
            var markdown = "2nd paragraph. *Italic*, **bold**, and `monospace`.";
            var expected = "<p>2nd paragraph. <em>Italic</em>, <strong>bold</strong>, and <code>monospace</code>.</p>";

            // Act
            var html = MarkdigConverter.ConvertToHtml(markdown);

            // Assert
            Check.That(html).IsNotNull();
            Check.That(html == expected);
        }

        [Test]
        public void ShouldConvertToPlainText()
        {
            // Arrange
            var markdown = @"2nd paragraph. *Italic*, **bold**, and `monospace`.";
            var expected = "2nd paragraph. Italic, bold, and monospace.";

            // Act
            var plainText = MarkdigConverter.ConvertToPlainText(markdown);

            // Assert
            Check.That(plainText).IsNotNull();
            Check.That(plainText == expected);
        }
    }
}