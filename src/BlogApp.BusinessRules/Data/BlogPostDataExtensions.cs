namespace BlogApp.BusinessRules.Data
{
    public static class BlogPostDataExtensions
    {
        public static string AsString(this IBlogPostData data)
        {
            return $"{data?.Title}\n{data?.Content}";
        }
    }
}