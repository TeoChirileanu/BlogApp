﻿using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;
using Markdig;

namespace BlogApp.Infrastructure
{
    public class MarkdownDataProcessor : IDataProcessor
    {
        private readonly MarkdownPipeline _pipeline =
            new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

        public BlogPostData ProcessData(IBlogPostData data)
        {
            var title = data.Title;
            var content = data.Content;

            var contentAsHtml = Markdown.ToHtml(content, _pipeline);

            var result = new BlogPostData(title, contentAsHtml);
            return result;
        }
    }
}