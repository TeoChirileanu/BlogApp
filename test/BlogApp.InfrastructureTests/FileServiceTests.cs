using System.IO;
using System.Threading.Tasks;
using BlogApp.Infrastructure;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class FileServiceTests
    {
        private string _randomContent;
        private string _randomFile;

        [SetUp]
        public void SetUp()
        {
            _randomFile = Path.GetRandomFileName();
            _randomContent = Path.GetRandomFileName();
        }

        [Test]
        public async Task ShouldAddFile()
        {
            // Arrange & Act
            await FileService.AddFile((_randomFile, _randomContent));

            // Assert
            var (name, content) = await FileService.GetFile(_randomFile);
            Check.That(name).Not.IsNullOrEmpty();
            Check.That(content).Not.IsNullOrEmpty();
        }

        [Test]
        public async Task ShouldGetFile()
        {
            // Arrange
            await FileService.AddFile((_randomFile, _randomContent));

            // Act
            var (name, content) = await FileService.GetFile(_randomFile);

            // Assert
            Check.That(name == _randomFile);
            Check.That(content == _randomContent);
        }

        [TestCase(2)]
        public async Task ShouldGetAllFiles(int howManyFilesToAdd)
        {
            // Arrange
            for (var i = 0; i < howManyFilesToAdd; i++)
                await FileService.AddFile(($"{_randomFile}-{i}", _randomContent));

            // Act
            var files = await FileService.GetAllFiles();

            // Assert
            Check.That(files.Count).IsEqualTo(howManyFilesToAdd);
        }

        [Test]
        public async Task ShouldRemoveFile()
        {
            // Arrange
            await FileService.AddFile((_randomFile, _randomContent));

            // Act
            await FileService.RemoveFile(_randomFile);

            // Assert
            var (file, content) = await FileService.GetFile(_randomFile);
            Check.That(file).IsNullOrEmpty();
            Check.That(content).IsNullOrEmpty();
        }

        [TestCase(2)]
        //[Ignore("use with caution")]
        public async Task ShouldRemoveAll(int howManyFilesToAdd)
        {
            // Arrange
            File.WriteAllText(_randomFile, _randomContent);
            for (var i = 0; i < howManyFilesToAdd; i++)
                await FileService.AddFile(($"{_randomFile}-{i}", _randomContent));

            // Act
            await FileService.RemoveAllFiles();

            // Assert
            var files = await FileService.GetAllFiles();
            Check.That(files).IsNullOrEmpty();
        }

        [TearDown]
        public Task TearDown()
        {
            return FileService.RemoveAllFiles();
        }
    }
}