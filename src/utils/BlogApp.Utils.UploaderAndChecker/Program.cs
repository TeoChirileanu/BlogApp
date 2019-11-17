using System;
using System.Threading.Tasks;
using BlogApp.Common;

namespace BlogApp.Utils.UploaderAndChecker
{
    public static class Program
    {
        public static async Task Main()
        {
            try
            {
                var articlesLocation = Constants.ProdArticlesLocation; // todo: do it properly
#if DEBUG
                articlesLocation = Constants.TestArticlesLocation; // for testing only
#endif
                await NewArticleUploaderAndPlagiarismChecker.UploadNewArticlesAndShowPlagiarismScore(articlesLocation);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey(false);
            }
        }
    }
}