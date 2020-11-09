using System;
using Examine;

namespace Our.Umbraco.Extensions.Search
{
    public static class StringExtensions
    {
        /// <summary>
        /// Splits a string removing any Lucene stop words and empty parts
        /// </summary>
        public static string[] SplitSanitize(this string input)
        {
            return input
                .RemoveStopWords()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}