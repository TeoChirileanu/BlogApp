using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.Infrastructure;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class BlobRepositoryTests
    {
        [Test]
        public async Task ShouldGetBlob()
        {
            // Arrange
            var blobRepository = new BlobRepository();

            // Act
            var blobName = "title1.md";
            var content = await blobRepository.GetBlobContent(blobName);

            // Assert
            Check.That(content).IsNotNull();
        }

        [Test]
        public async Task ShouldGetAllBlobs()
        {
            // Arrange
            var blobRepository = new BlobRepository();

            // Act
            var blobs = new List<(string blobName, string blobContent)>();
            await foreach (var blob in blobRepository.GetAllBlobs()) blobs.Add(blob);

            // Assert
            Check.That(blobs).Not.IsEmpty();
            blobs.ForEach(blob =>
            {
                var (blobName, blobContent) = blob;
                Check.That(blobName).Not.IsNullOrWhiteSpace();
                Check.That(blobContent).Not.IsNullOrWhiteSpace();
            });
        }

        [Test]
        public async Task ShouldAddBlob()
        {
            // Arrange
            var blobRepository = new BlobRepository();
            var name = "foo";
            var originalContent = "bar";

            // Act
            await blobRepository.AddBlob(name, originalContent);

            // Assert
            var newContent = await blobRepository.GetBlobContent(name);
            Check.That(newContent).IsEqualTo(originalContent);

            // Clean
            await blobRepository.RemoveBlob(name);
        }

        [Test]
        public async Task ShouldRemoveBlob()
        {
            // Arrange
            var blobRepository = new BlobRepository();
            var name = "bar";
            var content = "foo";
            await blobRepository.AddBlob(name, content);

            // Act
            await blobRepository.RemoveBlob(name);

            // Assert
            var newContent = await blobRepository.GetBlobContent(name);
            Check.That(newContent).IsNullOrWhiteSpace();
        }
    }
}