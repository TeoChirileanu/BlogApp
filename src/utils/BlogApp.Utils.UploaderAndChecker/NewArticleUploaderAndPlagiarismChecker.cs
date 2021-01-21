using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Common;
using BlogApp.Infrastructure;
using Copyleaks.SDK.V3.API;
using Copyleaks.SDK.V3.API.Models.Requests;
using Copyleaks.SDK.V3.API.Models.Requests.Properties;
using Copyleaks.SDK.V3.API.Models.Types;

namespace BlogApp.Utils.UploaderAndChecker
{
    public static class NewArticleUploaderAndPlagiarismChecker
    {
        private const bool SandboxMode = true; // for testing purposes, doesn't use credits
        private static string _articlesLocation;

        public static async Task UploadNewArticlesAndShowPlagiarismScore(string articlesLocation)
        {
            _articlesLocation = articlesLocation;
            var newArticles = await GetNewLocalArticles();
            foreach (var newArticle in newArticles)
            {
                var markdownArticle = Path.Join(_articlesLocation, newArticle);
                await UploadNewArticle(markdownArticle);
                await ShowPlagiarismScore(markdownArticle);
            }
        }

        private static async Task<string[]> GetNewLocalArticles()
        {
            var localArticles = GetLocalArticles();
            Console.WriteLine($"{localArticles.Length} local articles found");
            var cloudArticles = await GetCloudArticles();
            Console.WriteLine($"{cloudArticles.Length} cloud articles found");
            var newLocalArticles = localArticles.Except(cloudArticles).ToArray();
            var articlesAsString = string.Join(',', newLocalArticles);
            Console.WriteLine($"{newLocalArticles.Length} new local articles found: {articlesAsString}");
            return newLocalArticles;
        }

        private static string[] GetLocalArticles()
        {
            var localFiles = Directory.EnumerateFiles(_articlesLocation)
                .Select(Path.GetFileNameWithoutExtension)
                .ToArray();
            return localFiles;
        }

        private static async Task<string[]> GetCloudArticles()
        {
            var cloudArticles = await BlobService.GetAllBlobs();
            var cloudArticlesNames = cloudArticles.Select(blob => blob.blobName);
            return cloudArticlesNames.ToArray();
        }

        private static async Task UploadNewArticle(string newArticle)
        {
            var name = Path.GetFileNameWithoutExtension(newArticle);
            var content = await File.ReadAllTextAsync(newArticle);
            Console.WriteLine($"Uploading article {name}...");
            await BlobService.AddBlob(name, content);
        }

        private static async Task ShowPlagiarismScore(string markdownArticle)
        {
            var plainTextArticle = await GetPlainTextArticle(markdownArticle);
            var score = await GetPlagiarismScore(plainTextArticle);
            var articleName = Path.GetFileNameWithoutExtension(plainTextArticle);
            Console.WriteLine($"Article {articleName} has a plagiarism score of {score}.");
            File.Delete(plainTextArticle);
        }

        private static async Task<string> GetPlainTextArticle(string articlePath)
        {
            var markdownContent = await File.ReadAllTextAsync(articlePath);
            var plainTextContent = MarkdigConverter.ConvertToPlainText(markdownContent);
            var plainTextArticlePath = Path.ChangeExtension(articlePath, "txt");
            await File.WriteAllTextAsync(plainTextArticlePath, plainTextContent);
            return plainTextArticlePath;
        }

        private static async Task<double> GetPlagiarismScore(string article)
        {
            var token = await GetToken();
            using var scansApi = new CopyleaksScansApi(eProduct.Businesses, token);
            var credits = await scansApi.CreditBalanceAsync();
            Console.WriteLine($"{credits} credits left.");

            var fileDocument = await GetFileDocument(article);
            var scanId = $"{Guid.NewGuid()}";
            await scansApi.SubmitFileAsync(scanId, fileDocument);

            uint progress;
            do
            {
                progress = await scansApi.ProgressAsync(scanId);
                Console.WriteLine($"Progress: {progress}%");
                Thread.Sleep(1000);
            } while (progress != 100);

            var result = await scansApi.ResultAsync(scanId);
            var score = result.Results.Score.AggregatedScore;
            return score;
        }

        private static async Task<string> GetToken()
        {
            using var identityApi = new CopyleaksIdentityApi();
            var response = await identityApi.LoginAsync(Constants.CopyLeaksEmail, Constants.CopyLeaksKey);
            var token = response.Token;
            return token;
        }

        private static async Task<FileDocument> GetFileDocument(string filePath)
        {
            var bytes = await File.ReadAllBytesAsync(filePath);
            var base64 = Convert.ToBase64String(bytes);
            var fileDocument = new FileDocument
            {
                Base64 = base64,
                Filename = Path.GetFileName(filePath),
                PropertiesSection = new BusinessesScanProperties
                {
                    Webhooks = new Webhooks
                    {
                        Status = new Uri(Constants.CopyLeaksWebHook),
                        NewResult = new Uri(Constants.CopyLeaksWebHook)
                    },
                    Sandbox = SandboxMode // true means there are no credits used
                }
            };
            return fileDocument;
        }

        //private static string[] GetNewCloudArticles() // todo
        //{
        //    var localArticles = GetLocalArticles();
        //    Console.WriteLine($"{localArticles.Length} local articles found");
        //    var cloudArticles = GetCloudArticles();
        //    Console.WriteLine($"{cloudArticles.Length} cloud articles found");
        //    var newCloudArticles = cloudArticles.Except(localArticles).ToArray();
        //    Console.WriteLine($"{newCloudArticles.Length} new cloud articles found");
        //    return newCloudArticles;
        //}
    }
}