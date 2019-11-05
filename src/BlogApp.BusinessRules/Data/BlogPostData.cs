namespace BlogApp.BusinessRules.Data
{
    public class BlogPostData : IBlogPostData
    {
        public BlogPostData(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; }
        public string Content { get; }


        private bool Equals(IBlogPostData other)
        {
            return Title == other.Title && Content == other.Content;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((IBlogPostData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Title != null ? Title.GetHashCode() : 0) * 397) ^
                       (Content != null ? Content.GetHashCode() : 0);
            }
        }
    }
}