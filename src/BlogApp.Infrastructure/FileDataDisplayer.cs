using System;
using System.IO;
using BlogApp.BusinessRules.Data;
using BlogApp.UseCases.Adapters;

namespace BlogApp.Infrastructure
{
    public class FileDataDisplayer : IDataDisplayer
    {
        private readonly string _filePath;

        public FileDataDisplayer(string filePath)
        {
            _filePath = filePath;
        }

        public void DisplayData(BlogPostData data)
        {
            var dataAsString = data.AsString();
            var dataAsLines = dataAsString.Split('\n');
            WriteLines(dataAsLines);
        }

        private void WriteLines(string[] dataAsLines)
        {
            try
            {
                File.WriteAllLines(_filePath, dataAsLines);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}