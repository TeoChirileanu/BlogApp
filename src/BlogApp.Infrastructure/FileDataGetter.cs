using System;
using System.IO;
using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.Infrastructure
{
    public class FileDataGetter : IDataGetter
    {
        private readonly string _filePath;

        public FileDataGetter(string filePath)
        {
            _filePath = filePath;
        }

        public BlogPostData GetData()
        {
            var lines = File.ReadAllLines(_filePath);
            if (lines.Length < 2) return null;
            var title = lines[0];
            var content = lines[1];
            var data = new BlogPostData
            {
                Title = title,
                Content = content
            };
            return data;
        }
    }
}
