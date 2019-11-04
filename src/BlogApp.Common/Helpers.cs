using System;
using System.IO;

namespace BlogApp.Common
{
    public static class Helpers
    {
        public static string[] ReadLines(string filePath)
        {
            var lines = new string[] { };
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return lines;
        }

        public static void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath)) return;
            try
            {
                File.Delete(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}