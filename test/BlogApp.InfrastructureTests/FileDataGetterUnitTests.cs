using System.IO;
using BlogApp.Common;
using BlogApp.Infrastructure;
using BlogApp.UseCases.Adapters;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class FileDataGetterUnitTests
    {
        private const string InputFile = Constants.InputFile;
        private const string Title = Constants.Title;
        private const string Content = Constants.Content;

        [SetUp]
        public void Setup()
        {
            using var writer = new StreamWriter(InputFile);
            writer.WriteLine(Title);
            writer.WriteLine(Content);
        }

        [Test]
        public void ShouldGetDataFromFile()
        {
            // Arrange
            IDataGetter dataGetter = new FileDataGetter(InputFile);

            // Act
            var data = dataGetter.GetData();

            // Assert
            Check.That(data).IsNotNull();
            Check.That(data.Title).IsEqualTo(Title);
            Check.That(data.Content).IsEqualTo(Content);
        }

        [TearDown]
        public void TearDown()
        {
            Helpers.DeleteFile(InputFile);
        }
    }
}