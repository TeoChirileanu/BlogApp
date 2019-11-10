using System;
using System.IO;
using System.Linq;
using Azure.Storage.Blobs;
using BlogApp.Common;

namespace BlogApp.Utils.NewArticleUploader
{
    public static class ArticleManager
    {
        private static readonly string ArticlesLocation =
            Path.GetFullPath(@"..\..\..\..\..\..\articles");
        private static readonly BlobServiceClient ServiceClient =
            new BlobServiceClient(Constants.ConnectionString);
        private static readonly BlobContainerClient ContainerClient =
            ServiceClient.GetBlobContainerClient(Constants.Container);

        public static void UploadNewArticleToAzure()
        {
            var newArticle = GetNewArticle();
            if (newArticle == null)
            {
                Console.WriteLine("No new article found");
                return;
            }

            try
            {
                Console.WriteLine($"Uploading {newArticle}...");
                UploadIntoTheCloud(newArticle);
                Console.WriteLine($"Successfully uploaded!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        private static void UploadIntoTheCloud(string newArticlePath)
        {
            var newArticle = Path.GetFileName(newArticlePath);
            var blobClient = ContainerClient.GetBlobClient(newArticle);
            using var stream = File.OpenRead($@"{ArticlesLocation}\{newArticlePath}");
            blobClient.Upload(stream);
        }

        private static string GetNewArticle()
        {
            var localArticles = GetLocalArticles();
            Console.WriteLine($"{localArticles.Length} local articles found");
            var cloudArticles = GetCloudArticles();
            Console.WriteLine($"{cloudArticles.Length} cloud articles found");
            var newArticles = localArticles.Except(cloudArticles).ToArray();
            try
            {
                var newArticle = newArticles.SingleOrDefault();
                return newArticle;
            }
            catch (InvalidOperationException e)
            {
                var articles = string.Join(',', newArticles);
                Console.WriteLine($"Multiple new articles found: {articles}\n{e}");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown error: {e}");
                throw;
            }
        }

        private static string[] GetLocalArticles()
        {
            var localFiles = Directory.EnumerateFiles(ArticlesLocation)
                .Select(Path.GetFileName)
                .ToArray();
            return localFiles;
        }

        private static string[] GetCloudArticles()
        {
            var articles = ContainerClient.GetBlobs()
                .Select(blob => blob.Name)
                .ToArray();
            return articles;
        }
    }
}