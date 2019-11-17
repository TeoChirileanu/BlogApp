using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.Infrastructure
{
    public class MarkdownDataConvertor : IDataConvertor
    {
        public IBlogPostData ConvertMarkdownToHtml(IBlogPostData data)
        {
            var title = data.Title;
            var content = data.Content;
            var contentAsHtml = MarkdigConverter.ConvertToHtml(content);
            var result = new BlogPostData(title, contentAsHtml);
            return result;
        }
    }
}