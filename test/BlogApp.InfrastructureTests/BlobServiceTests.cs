using System.IO;
using System.Threading.Tasks;
using BlogApp.Infrastructure;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class BlobServiceTests
    {
        [Test]
        public async Task ShouldGetBlob()
        {
            // Arrange
            var randomString = Path.GetRandomFileName().Split('.')[0];
            var blobName = randomString;
            var blobContent = randomString;
            await BlobService.AddBlob(blobName, blobContent);

            // Act
            var content = BlobService.GetBlob(blobName);

            // Assert
            Check.That(content).IsNotNull();

            // Clean
            await BlobService.RemoveBlob(blobName);
        }

        [Test]
        public async Task ShouldGetAllBlobs()
        {
            // Arrange && Act
            var blobs = await BlobService.GetAllBlobs();

            // Assert
            Check.That(blobs).Not.IsEmpty();
            foreach (var blob in blobs)
            {
                var (blobName, blobContent) = blob;
                Check.That(blobName).Not.IsNullOrWhiteSpace();
                Check.That(blobContent).Not.IsNullOrWhiteSpace();
            }
        }

        [Test]
        public async Task ShouldAddBlob()
        {
            // Arrange
            var originalName = "foo";
            var originalContent = "bar";

            // Act
            await BlobService.AddBlob(originalName, originalContent);

            // Assert
            var (newName, newContent) = await BlobService.GetBlob(originalName);
            Check.That(newName).IsEqualTo(originalName);
            Check.That(newContent).IsEqualTo(originalContent);

            // Clean
            await BlobService.RemoveBlob(originalName);
        }

        [Test]
        public async Task ShouldRemoveBlob()
        {
            // Arrange
            var name = "bar";
            var content = "foo";
            await BlobService.AddBlob(name, content);

            // Act
            await BlobService.RemoveBlob(name);

            // Assert
            var (newName, newContent) = await BlobService.GetBlob(name);
            Check.That(newName).IsNullOrWhiteSpace();
            Check.That(newContent).IsNullOrWhiteSpace();
        }
    }
}