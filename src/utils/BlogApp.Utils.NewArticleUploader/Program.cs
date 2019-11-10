using System;

namespace BlogApp.Utils.NewArticleUploader
{
    public static class Program
    {
        public static void Main()
        {
            try
            {
                ArticleManager.UploadNewArticleToAzure();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Oops, could not upload: {e}");
            }
        }
    }
}
