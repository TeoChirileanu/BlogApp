namespace BlogApp.Common
{
    public static class Constants
    {
        public const string InputFile = "input.txt";
        public const string OutputFile = "output.txt";

        public const string Title = "title";
        public const string Content = "content";

        public static readonly string MarkdownContent = $"*{Content}*";
        public static readonly string HtmlContent = $"<p><em>{Content}</em</p>";
    }
}