using Markdig;

namespace BlogApp.Infrastructure
{
    public static class MarkdigConverter
    {
        private static readonly MarkdownPipeline Pipeline =
            new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();

        public static string ConvertToHtml(string markdown)
        {
            return Markdown.ToHtml(markdown, Pipeline);
        }

        public static string ConvertToPlainText(string markdown)
        {
            return Markdown.ToPlainText(markdown, Pipeline);
        }
    }
}