using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Copyleaks.SDK.V3.API;
using Copyleaks.SDK.V3.API.Models.Requests;
using Copyleaks.SDK.V3.API.Models.Requests.Properties;
using Copyleaks.SDK.V3.API.Models.Types;

namespace BlogApp.Utils.PlagiarismChecker
{
    public static class Program
    {
        public static async Task Main()
        {
            try
            {
                // todo: take it from arguments
                const string file = @"C:\Users\teodo\source\repos\BlogApp\articles\lorem.txt";
                var score = await GetScore(file);
                Console.WriteLine($"Plagiarism score: {score}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static async Task<double> GetScore(string file)
        {
            var token = await GetToken();

            using var scansApi = new CopyleaksScansApi(eProduct.Businesses, token);

            var credits = await scansApi.CreditBalanceAsync();
            Console.WriteLine($"{credits} credits left.");

            var fileDocument = await GetFileDocument(file);
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

        private static async Task<FileDocument> GetFileDocument(string file)
        {
            var bytes = await File.ReadAllBytesAsync(file);
            var base64 = Convert.ToBase64String(bytes);

            var fileDocument = new FileDocument
            {
                Base64 = base64,
                Filename = Path.GetFileName(file),
                PropertiesSection = new BusinessesScanProperties
                {
                    Webhooks = new Webhooks
                    {
                        Status = new Uri("https://webhook.site/f61daef1-faa0-48b9-8feb-29bcd2cd3b0d"),
                        NewResult = new Uri("https://webhook.site/f61daef1-faa0-48b9-8feb-29bcd2cd3b0d")
                    },
                }
            };
            return fileDocument;
        }

        private static async Task<string> GetToken()
        {
            const string email = "teodorchirileanu@gmail.com";
            const string key = "59CFD57B-D2C7-4926-B826-0C7E4D8321EF";
            using var identityApi = new CopyleaksIdentityApi();
            var loginResponse = await identityApi.LoginAsync(email, key);
            var token = loginResponse.Token;
            return token;
        }
    }
}
