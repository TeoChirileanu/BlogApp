namespace BlogApp.BusinessRules.Data
{
    public class BlogPostData
    {
        public BlogPostData(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}