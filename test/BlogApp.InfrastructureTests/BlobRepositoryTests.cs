using System.IO;
using BlogApp.Infrastructure;
using NFluent;
using NUnit.Framework;

namespace BlogApp.InfrastructureTests
{
    public class BlobRepositoryTests
    {
        [Test]
        public void ShouldGetBlob()
        {
            // Arrange
            var randomString = Path.GetRandomFileName().Split('.')[0];
            var blobName = randomString;
            var blobContent = randomString;
            BlobRepository.AddBlob(blobName, blobContent);

            // Act
            var content = BlobRepository.GetBlobContent(blobName);

            // Assert
            Check.That(content).IsNotNull();

            // Clean
            BlobRepository.RemoveBlob(blobName);
        }

        [Test]
        public void ShouldGetAllBlobs()
        {
            // Arrange

            // Act
            var blobs = BlobRepository.GetAllBlobs();

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
        public void ShouldAddBlob()
        {
            // Arrange
            var name = "foo";
            var originalContent = "bar";

            // Act
            BlobRepository.AddBlob(name, originalContent);

            // Assert
            var newContent = BlobRepository.GetBlobContent(name);
            Check.That(newContent).IsEqualTo(originalContent);

            // Clean
            BlobRepository.RemoveBlob(name);
        }

        [Test]
        public void ShouldRemoveBlob()
        {
            // Arrange
            var name = "bar";
            var content = "foo";
            BlobRepository.AddBlob(name, content);

            // Act
            BlobRepository.RemoveBlob(name);

            // Assert
            var newContent = BlobRepository.GetBlobContent(name);
            Check.That(newContent).IsNullOrWhiteSpace();
        }
    }
}