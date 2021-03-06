﻿using BlogApp.BusinessRules.Data;
using BlogApp.Common;
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

        public IBlogPostData GetData()
        {
            var lines = Helpers.ReadLines(_filePath);
            if (lines.Length != 2) return null;
            var title = lines[0];
            var content = lines[1];
            var data = new BlogPostData(title, content);
            return data;
        }
    }
}