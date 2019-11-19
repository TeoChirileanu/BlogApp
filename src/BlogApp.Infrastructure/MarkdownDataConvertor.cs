using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;
using Ganss.XSS;

namespace BlogApp.Infrastructure
{
    public class MarkdownDataConvertor : IDataConvertor
    {
        private readonly HtmlSanitizer _sanitizer = new HtmlSanitizer();

        public IBlogPostData ConvertMarkdownToHtml(IBlogPostData data)
        {
            var title = data.Title;
            var content = data.Content;
            var contentAsHtml = MarkdigConverter.ConvertToHtml(content);
            var sanitizedHtml = _sanitizer.Sanitize(contentAsHtml);
            var result = new BlogPostData(title, sanitizedHtml);
            return result;
        }
    }
}