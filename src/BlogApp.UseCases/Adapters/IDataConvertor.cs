using BlogApp.BusinessRules.Data;

namespace BlogApp.UseCases.Adapters
{
    public interface IDataConvertor
    {
        IBlogPostData ConvertMarkdownToHtml(IBlogPostData data);
    }
}