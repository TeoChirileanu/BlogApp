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

        public static readonly string MarkdownContent = $"*{Content}*";
        public static readonly string HtmlContent = $"<p><em>{Content}</em</p>";
    }
}