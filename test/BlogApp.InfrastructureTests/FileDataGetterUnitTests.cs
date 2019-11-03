using System;
using System.IO;
using BlogApp.Infrastructure;
using BlogApp.UseCases.Adapters;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class FileDataGetterUnitTests
    {
        private const string TestFile = "test.txt";
        private const string Title = "<title>";
        private const string Content = "<content>";

        [SetUp]
        public void Setup()
        {
            using var writer = new StreamWriter(TestFile);
            writer.WriteLine(Title);
            writer.WriteLine(Content);
        }

        [Test]
        public void ShouldGetDataFromFile()
        {
            // Arrange
            IDataGetter dataGetter = new FileDataGetter(TestFile);

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
            File.Delete(TestFile);
        }
    }
}