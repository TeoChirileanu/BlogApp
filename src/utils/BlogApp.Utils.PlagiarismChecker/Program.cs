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
        private static readonly string ArticlesLocation =
            Path.GetFullPath(@"..\..\..\articles");

        public static async Task Main()
        {
            try
            {
                // todo: take it from arguments
                var file = $@"{ArticlesLocation}\lorem.txt";
                var score = await PlagiarismScoreGetter.GetScore(file);
                Console.WriteLine($"Plagiarism score: {score}%");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
