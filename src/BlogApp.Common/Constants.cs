using System.IO;

namespace BlogApp.Common
{
    public static class Constants
    {
        public const string InputFile = "input.txt";
        public const string OutputFile = "output.txt";

        public const string Title = "title";
        public const string Content = "content";


        public const string ConnectionString =
            "DefaultEndpointsProtocol=https;AccountName=blazorblog;AccountKey=JD0l2kynOVk1SlpzvUFE1KaYt7mQOUNiXLaXwDAN3GbA2MkE16szkXM+hINjXm266ucGFi4XwJDKnHM0A0jXSw==;EndpointSuffix=core.windows.net";

        public const string Container = "articles";
        public const string Share = Container;
        public const string Directory = Share;

        public const string CopyLeaksEmail = "teodorchirileanu@gmail.com";
        public const string CopyLeaksKey = "59CFD57B-D2C7-4926-B826-0C7E4D8321EF";
        public const string CopyLeaksWebHook = "https://webhook.site/85866b8c-c114-4b86-9032-85c1d0c31546";

        public static readonly string MarkdownContent = $"*{Content}*";
        public static readonly string HtmlContent = $"<p><em>{Content}</em</p>";

        public static readonly string ProdArticlesLocation = // used with dotnet run
            Path.GetFullPath(@"..\..\..\articles");

        public static readonly string TestArticlesLocation = // used with debugging
            Path.GetFullPath(@"..\..\..\..\..\..\articles");
    }
}